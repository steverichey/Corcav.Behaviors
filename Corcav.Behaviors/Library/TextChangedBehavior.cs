// <copyright file="TextChangedBehavior.cs" company="Float">
// Copyright (c) 2020 Float, All rights reserved.
// Shared under an MIT license. See license.md for details.
// </copyright>

using Xamarin.Forms;

namespace Corcav.Behaviors
{
    /// <summary>
    /// Updates text while entry text changes.
    /// </summary>
    public class TextChangedBehavior : Behavior<Entry>
    {
        /// <summary>
        /// Binding to the text property.
        /// </summary>
        public static readonly BindableProperty TextProperty = BindableProperty.Create<TextChangedBehavior, string>(p => p.Text, null, propertyChanged: OnTextChanged);

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        /// <inheritdoc />
        protected override void OnAttach()
        {
            AssociatedObject.TextChanged += OnTextChanged;
        }

        /// <inheritdoc />
        protected override void OnDetach()
        {
            AssociatedObject.TextChanged -= OnTextChanged;
        }

        static void OnTextChanged(BindableObject bindable, string oldvalue, string newvalue)
        {
            (bindable as TextChangedBehavior).AssociatedObject.Text = newvalue;
        }

        void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            Text = e.NewTextValue;
        }
    }
}
