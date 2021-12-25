using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.ObjectModel;

namespace MobileScanner.ViewModels
{
    public abstract class BaseViewModel : ObservableObject
    {
        private bool isBusy;
        public bool IsBusy
        {
            get => this.isBusy;
            set => SetProperty(ref this.isBusy, value);
        }
    }
}
