// <copyright file="AppDelegate.cs" company="Float">
// Copyright (c) 2020 Float, All rights reserved.
// Shared under an MIT license. See license.md for details.
// </copyright>

using Foundation;
using UIKit;
using Xamarin.Forms;

namespace Corcav.Behaviors.Demo.iOS
{
	[Register(nameof(AppDelegate))]
	public partial class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			Forms.Init();
			LoadApplication(new App());
			return base.FinishedLaunching(app, options);
		}
	}
}
