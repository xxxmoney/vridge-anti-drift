﻿using MobileScanner.ViewModels;
using MobileScanner.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileScanner
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            this.SetupServices();

            MainPage = new AppShell();
        }

        private void SetupServices()
        {
            DependencyService.Register<MainViewModel>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
