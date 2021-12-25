using MobileScanner.BackgroundServices;
using MobileScanner.Services;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileScanner.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand StartServiceCommand { get; }
        public ICommand StopServiceCommand { get; }

        private IBackgroundServicesManagerService backgroundServicesManagerService;

        public MainViewModel()
        {            
            this.StartServiceCommand = new Command(this.StartService);
            this.StopServiceCommand = new Command(this.StopService);

            this.backgroundServicesManagerService = DependencyService.Get<IBackgroundServicesManagerService>();
        }
        
        private void StartService()
        {
            this.backgroundServicesManagerService.StartService(BackgroundServiceList.SCANNER_SERVICE);
        }
        private void StopService()
        {
            this.backgroundServicesManagerService.StopService(BackgroundServiceList.SCANNER_SERVICE);
        }

    }
}