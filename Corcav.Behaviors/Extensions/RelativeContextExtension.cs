// <copyright file="RelativeContextExtension.cs" company="Float">
// Copyright (c) 2020 Float, All rights reserved.
// Shared under an MIT license. See license.md for details.
// </copyright>

using System;
using System.Diagnostics.Contracts;
using System.Globalization;
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
            Contract.Requires(serviceProvider != null);

            if (!(serviceProvider.GetService<IRootObjectProvider>() is IRootObjectProvider rootObjectProvider))
            {
                throw new ArgumentException(Resources.ServiceProviderDoesNot);
            }

            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, Resources.PropertyRequired, nameof(Name)));
            }

            if (!(rootObjectProvider.RootObject is Element nameScope))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, Resources.DoesNotProvideRoot, nameof(rootObjectProvider)));
            }

            if (!(nameScope.FindByName<Element>(Name) is Element element))
            {
                throw new ArgumentNullException($"Can't find element named '{Name}'");
            }

            var context = element.BindingContext;
            rootElement = element;

            if (!(serviceProvider.GetService<IProvideValueTarget>() is IProvideValueTarget ipvt))
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, Resources.DoesNotProvide, nameof(serviceProvider), nameof(IProvideValueTarget)));
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
