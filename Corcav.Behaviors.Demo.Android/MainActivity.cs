// <copyright file="BehaviorCollection.cs" company="Float">
// Copyright (c) 2020 Float, All rights reserved.
// Shared under an MIT license. See license.md for details.
// </copyright>

using Android.App;
using Android.Content.PM;
using Android.OS;
using Xamarin.Forms;

namespace Corcav.Behaviors.Demo.Droid
{
	[Activity(Label = "Corcav.Behaviors.Demo", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);
			Forms.Init(this, bundle);
			LoadApplication(new App());
		}
	}
}
