// <copyright file="Infrastructure.cs" company="Float">
// Copyright (c) 2020 Float, All rights reserved.
// Shared under an MIT license. See license.md for details.
// </copyright>

using System;

namespace Corcav.Behaviors
{
    /// <summary>
    /// Forces iOS linker to include Xamarin Behaviors assembly in deployed package.
    /// </summary>
    public static class Infrastructure
    {
        static DateTime initDate;

        /// <summary>
        /// Called on iOS to prevent the compiler optimizing code away.
        /// </summary>
        public static void Init()
        {
            initDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Used to avoid unused value warnings.
        /// </summary>
        /// <returns>The initial date.</returns>
        public static DateTime ReadTime()
        {
            return initDate;
        }
    }
}
