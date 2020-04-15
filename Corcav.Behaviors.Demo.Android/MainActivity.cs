// <copyright file="MainActivity.cs" company="Float">
// Copyright (c) 2020 Float, All rights reserved.
// Shared under an MIT license. See license.md for details.
// </copyright>

using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Corcav.Behaviors.Demo.Droid
{
    /// <summary>
    /// The starting point for the application.
    /// </summary>
    [Activity(Label = "Corcav.Behaviors.Demo", MainLauncher = true)]
    public class MainActivity : FormsApplicationActivity
    {
        /// <inheritdoc />
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Forms.Init(this, bundle);
            LoadApplication(new App());
        }
    }
}
