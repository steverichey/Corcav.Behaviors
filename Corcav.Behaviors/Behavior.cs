// <copyright file="Behavior.cs" company="Float">
// Copyright (c) 2020 Float, All rights reserved.
// Shared under an MIT license. See license.md for details.
// </copyright>

using Xamarin.Forms;

namespace Corcav.Behaviors
{
    /// <summary>
    /// The behavior object.
    /// </summary>
    public abstract class Behavior : BindableObject, IBehavior
    {
        /// <summary>
        /// Gets the bindable object associated with this behavior.
        /// </summary>
        /// <value>The associated bindable object.</value>
        public virtual BindableObject AssociatedObject { get; private set; }

        /// <inheritdoc />
        public virtual void Detach()
        {
            OnDetach();
            AssociatedObject = null;
        }

        /// <inheritdoc />
        public virtual void Attach(BindableObject dependencyObject)
        {
            AssociatedObject = dependencyObject;
            OnAttach();
        }

        /// <summary>
        /// Called on attach.
        /// </summary>
        protected abstract void OnAttach();

        /// <summary>
        /// Called on detach.
        /// </summary>
        protected abstract void OnDetach();
    }
}
