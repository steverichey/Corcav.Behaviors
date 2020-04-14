// <copyright file="RelativeContextExtension.cs" company="Float">
// Copyright (c) 2020 Float, All rights reserved.
// Shared under an MIT license. See license.md for details.
// </copyright>

using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Corcav.Behaviors
{
    /// <summary>
    /// Custom markup extension that gets the BindingContext of a UI element.
    /// </summary>
    [ContentProperty("Name")]
    public class RelativeContextExtension : IMarkupExtension
    {
        BindableObject attachedObject;
        Element rootElement;

        /// <summary>
        /// Gets or sets the element name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Provides a value from the given service provider.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        /// <returns>The provided value.</returns>
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException("serviceProvider");
            }

            if (!(serviceProvider.GetService(typeof(IRootObjectProvider)) is IRootObjectProvider rootObjectProvider))
            {
                throw new ArgumentException("serviceProvider does not provide an IRootObjectProvider");
            }

            if (string.IsNullOrEmpty(this.Name))
            {
                throw new ArgumentNullException("Name");
            }

            var nameScope = rootObjectProvider.RootObject as Element;
            var element = nameScope.FindByName<Element>(this.Name);

            if (element == null)
            {
                throw new ArgumentNullException($"Can't find element named '{Name}'");
            }

            var context = element.BindingContext;
            rootElement = element;

            if (!(serviceProvider.GetService(typeof(IProvideValueTarget)) is IProvideValueTarget ipvt))
            {
                throw new ArgumentException("serviceProvider does not provide an IProvideValueTarget");
            }

            attachedObject = ipvt.TargetObject as BindableObject;
            attachedObject.BindingContextChanged += this.OnContextChanged;

            return context ?? new object();
        }

        void OnContextChanged(object sender, EventArgs e)
        {
            // if used with EventToCommand, markup extension automatically acts on CommandNameContext
            if (attachedObject is EventToCommand command)
            {
                command.CommandNameContext = rootElement.BindingContext;
            }
            else
            {
                attachedObject.BindingContext = rootElement.BindingContext;
            }
        }
    }
}
