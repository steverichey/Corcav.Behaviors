// <copyright file="Interaction.cs" company="Float">
// Copyright (c) 2020 Float, All rights reserved.
// Shared under an MIT license. See license.md for details.
// </copyright>

using System.Collections.Specialized;
using System.Diagnostics.Contracts;
using Xamarin.Forms;

namespace Corcav.Behaviors
{
    /// <summary>
    /// Manages behavior infrastructure.
    /// </summary>
    public class Interaction
    {
        /// <summary>
        /// Bind to the behaviors.
        /// </summary>
        public static readonly BindableProperty BehaviorsProperty = BindableProperty.CreateAttached(
            "Behaviors",
            typeof(BehaviorCollection),
            typeof(Interaction),
            null,
            BindingMode.OneWay,
            null,
            OnBehaviorsChanged);

        /// <summary>
        /// Returns the behavior collection from the target's behaviors property.
        /// </summary>
        /// <param name="target">The target from which to get behaviors.</param>
        /// <returns>The behaviors in the given object.</returns>
        public static BehaviorCollection GetBehaviors(BindableObject target)
        {
            Contract.Requires(target != null);
            return (BehaviorCollection)target.GetValue(BehaviorsProperty);
        }

        /// <summary>
        /// Set the behaviors property on the given target.
        /// </summary>
        /// <param name="target">The target object whose behaviors should be set.</param>
        /// <param name="value">The value to set on the behaviors property.</param>
        public static void SetBehaviors(BindableObject target, BehaviorCollection value)
        {
            Contract.Requires(target != null);
            target.SetValue(BehaviorsProperty, value);
        }

        static void OnBehaviorsChanged(BindableObject target, object oldvalue, object newvalue)
        {
            if (newvalue is BehaviorCollection value)
            {
                if (target is Element t)
                {
                    t.BindingContextChanged += (s, e) =>
                    {
                        foreach (var behavior in GetBehaviors(target))
                        {
                            if (behavior is BindableObject el)
                            {
                                el.BindingContext = target.BindingContext;
                            }
                        }
                    };
                }

                value.CollectionChanged += (sender, args) =>
                {
                    if (args.Action == NotifyCollectionChangedAction.Add)
                    {
                        foreach (IBehavior behavior in args.NewItems)
                        {
                            behavior.Attach(target);
                        }
                    }

                    if (args.Action == NotifyCollectionChangedAction.Remove)
                    {
                        foreach (IBehavior behavior in args.OldItems)
                        {
                            behavior.Detach();
                        }
                    }
                };

                foreach (var behavior in value)
                {
                    behavior.Attach(target);
                }

                SetBehaviors(target, value);
            }
        }
    }
}
