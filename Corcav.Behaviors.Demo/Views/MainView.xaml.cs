// <copyright file="MainView.xaml.cs" company="Float">
// Copyright (c) 2020 Float, All rights reserved.
// Shared under an MIT license. See license.md for details.
// </copyright>

using Corcav.Behaviors.Demo.ViewModels;
using Xamarin.Forms;

namespace Corcav.Behaviors.Demo.Views
{
    /// <summary>
    /// The main view.
    /// </summary>
    public partial class MainView : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainView"/> class.
        /// </summary>
        public MainView()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
    }
}
