// <copyright file="MainView.cs" company="Float">
// Copyright (c) 2020 Float, All rights reserved.
// Shared under an MIT license. See license.md for details.
// </copyright>

using Corcav.Behaviors.Demo.ViewModels;
using Xamarin.Forms;

namespace Corcav.Behaviors.Demo.Views
{
    public partial class MainView: ContentPage
    {
        public MainView()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }
    }
}
