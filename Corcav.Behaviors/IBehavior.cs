// <copyright file="IBehavior.cs" company="Float">
// Copyright (c) 2020 Float, All rights reserved.
// Shared under an MIT license. See license.md for details.
// </copyright>

using Xamarin.Forms;

namespace Corcav.Behaviors
{
    /// <summary>
    /// Abstracts a behavior implementation.
    /// </summary>
    public interface IBehavior
    {
        /// <summary>
        /// Gets the associated object.
        /// </summary>
        /// <value>The associated object.</value>
        BindableObject AssociatedObject { get; }

        /// <summary>
        /// Attaches the specified dependency object to this behavior.
        /// </summary>
        /// <param name="associatedObject">The associated object.</param>
        /// <exception cref="InvalidOperationException">Raised when attached element doesn't match declared type.</exception>
        void Attach(BindableObject associatedObject);

        /// <summary>
        /// Detaches this object.
        /// </summary>
        void Detach();
    }
}
