// <copyright file="App.cs" company="Float">
// Copyright (c) 2020 Float, All rights reserved.
// Shared under an MIT license. See license.md for details.
// </copyright>

using Corcav.Behaviors.Demo.Views;
using Xamarin.Forms;

namespace Corcav.Behaviors.Demo
{
    /// <summary>
    ///  The base class for this application.
    /// </summary>
    public class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            MainPage = new MainView();
        }
    }
}
