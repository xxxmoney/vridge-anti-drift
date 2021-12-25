using MobileScanner.ViewModels;
using MobileScanner.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace MobileScanner
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        }

    }
}
