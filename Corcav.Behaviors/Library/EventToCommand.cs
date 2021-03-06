// <copyright file="EventToCommand.cs" company="Float">
// Copyright (c) 2020 Float, All rights reserved.
// Shared under an MIT license. See license.md for details.
// </copyright>

using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;

namespace Corcav.Behaviors
{
    /// <summary>
    /// Invoked a command when an event raises.
    /// </summary>
    public class EventToCommand : Behavior
    {
        /// <summary>
        /// Binding to the event name property.
        /// </summary>
        public static readonly BindableProperty EventNameProperty = BindableProperty.Create(nameof(EventName), typeof(string), typeof(EventToCommand));

        /// <summary>
        /// Binding to the command property.
        /// </summary>
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(EventToCommand));

        /// <summary>
        /// Binding to the command parameter property.
        /// </summary>
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(EventToCommand));

        /// <summary>
        /// Binding to the command name property.
        /// </summary>
        public static readonly BindableProperty CommandNameProperty = BindableProperty.Create(nameof(CommandName), typeof(string), typeof(EventToCommand));

        /// <summary>
        /// Binding to the command name context property.
        /// </summary>
        public static readonly BindableProperty CommandNameContextProperty = BindableProperty.Create(nameof(CommandNameContext), typeof(object), typeof(EventToCommand));

        Delegate handler;
        EventInfo eventInfo;

        /// <summary>
        /// Gets or sets a value indicating whether event argument will be passed to bound command.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [pass event argument]; otherwise, <c>false</c>.
        /// </value>
        public bool PassEventArgument { get; set; }

        /// <summary>
        /// Gets or sets the name of the event to subscribe.
        /// </summary>
        /// <value>The name of the event.</value>
        public string EventName
        {
            get { return (string)GetValue(EventNameProperty); }
            set { SetValue(EventNameProperty, value); }
        }

        /// <summary>
        /// Gets or sets the command to invoke when event raised.
        /// </summary>
        /// <value>The command.</value>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the optional command parameter.
        /// </summary>
        /// <value>The command parameter.</value>
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        /// <summary>
        /// Gets or sets the name of the relative command.
        /// </summary>
        /// <value>The name of the command.</value>
        public string CommandName
        {
            get { return (string)GetValue(CommandNameProperty); }
            set { SetValue(CommandNameProperty, value); }
        }

        /// <summary>
        /// Gets or sets the relative context used with command name.
        /// </summary>
        /// <value>The command name context.</value>
        public object CommandNameContext
        {
            get { return GetValue(CommandNameContextProperty); }
            set { SetValue(CommandNameContextProperty, value); }
        }

        /// <inheritdoc />
        protected override void OnAttach()
        {
            var events = this.AssociatedObject.GetType().GetRuntimeEvents();

            if (events.Any())
            {
                this.eventInfo = events.FirstOrDefault(e => e.Name == this.EventName);

                if (this.eventInfo == null)
                {
                    throw new ArgumentException($"EventToCommand: Can't find any event named '{EventName}' on attached type");
                }

                this.AddEventHandler(eventInfo, this.AssociatedObject, this.OnFired);
            }
        }

        /// <inheritdoc />
        protected override void OnDetach()
        {
            if (handler == null)
            {
                return;
            }

            eventInfo.RemoveEventHandler(AssociatedObject, handler);
        }

        void AddEventHandler(EventInfo eventInfo, object item, Action<EventArgs> action)
        {
            // Got inspiration from here: http://stackoverflow.com/questions/9753366/subscribing-an-action-to-any-event-type-via-reflection
            // Maybe it is possible to pass Event arguments as CommandParameter
            var mi = eventInfo.EventHandlerType.GetRuntimeMethods().First(rtm => rtm.Name == "Invoke");
            var parameters = mi.GetParameters().Select(p => Expression.Parameter(p.ParameterType)).ToList();
            var actionMethodInfo = action.GetMethodInfo();
            var exp = Expression.Call(Expression.Constant(this), actionMethodInfo, parameters.Last());
            this.handler = Expression.Lambda(eventInfo.EventHandlerType, exp, parameters).Compile();
            eventInfo.AddEventHandler(item, handler);
        }

        /// <summary>
        /// Called when subscribed event fires
        ///
        /// If a CommandParameter isn't assigned, the EventArgs parameter to the Event you're attaching to will be sent instead.
        /// You will want to have your Command to accept a parameter type of EventArgs for this to work correctly.
        ///
        /// </summary>
        /// <example>This is an example of using a Command and accepting an object of the ItemVisibilityEventArgs Type.
        /// <code>
        /// ICommand ItemAppearingCommand
        /// {
        ///     get
        ///     {
        ///         return new Command&lt;ItemVisibilityEventArgs&gt;(async args =>
        ///         {
        ///             if(viewModel.Items != null &amp;&amp; e.Item == viewModel.Items[viewModel.Items.Count -1])
        ///             {
        ///                 await viewModel.RetrieveNextItemSet(viewModel.Items.Count).ConfigureAwait(false);
        ///             }
        ///         }
        ///     }
        /// }
        /// </code>
        /// </example>
        /// <param name="e">The EventArgs value accompanying the Event.</param>
        private void OnFired(EventArgs e)
        {
            var param = PassEventArgument ? e : CommandParameter;

            if (!string.IsNullOrEmpty(CommandName))
            {
                if (Command == null)
                {
                    CreateRelativeBinding();
                }
            }

            Contract.Requires(Command != null);
            Contract.Requires(e != null || CommandParameter != null);

            if (Command != null && Command.CanExecute(param))
            {
                Command.Execute(param);
            }
        }

        /// <summary>
        /// Cretes a binding between relative context and provided Command name.
        /// </summary>
        void CreateRelativeBinding()
        {
            Contract.Requires(CommandNameContext != null);
            Contract.Requires(Command != null);

            var pi = CommandNameContext.GetType().GetRuntimeProperty(CommandName);

            if (pi == null)
            {
                throw new ArgumentNullException($"Can't find a command named '{CommandName}'");
            }

            Command = pi.GetValue(this.CommandNameContext) as ICommand;

            if (Command == null)
            {
                throw new ArgumentNullException($"Can't create binding with CommandName '{CommandName}'");
            }
        }
    }
}
