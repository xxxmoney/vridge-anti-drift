using MobileScanner.ForegroundServices;
using MobileScanner.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZXing;

namespace MobileScanner.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private Result result;
        public Result Result 
        {
            get => this.result;
            set => SetProperty(ref this.result, value);
        }

        public ICommand StartServiceCommand { get; }
        public ICommand StopServiceCommand { get; }
        public ICommand OnAppearCommand { get; }
        public ICommand OnScanResultCommand { get; }

        private readonly IForegroundServicesManagerService backgroundServicesManagerService;
        private readonly IPermissionsService permissionsService;

        public MainViewModel()
        {            
            this.StartServiceCommand = new AsyncCommand(this.StartService);
            this.StopServiceCommand = new Command(this.StopService);
            this.OnAppearCommand = new AsyncCommand(this.CheckPermissions);
            this.OnScanResultCommand = new Command(this.OnScanResult);

            this.backgroundServicesManagerService = DependencyService.Get<IForegroundServicesManagerService>();
            this.permissionsService = DependencyService.Get<IPermissionsService>();
        }
        
        private async Task<bool> CheckPermissions()
        {
            var passess = await this.permissionsService.CheckPermissions();

            if (!passess)
            {
                await Shell.Current.DisplayAlert("PERMISSION", "Please allow all permissions.", "OK");
            }
            else if (!MediaPicker.IsCaptureSupported)
            {
                await Shell.Current.DisplayAlert("CAMERA", "Capture is not supported.", "OK");
            }

            return passess;
        }

        private async Task StartService()
        {
#if __ANDROID__
	// Initialize the scanner first so it can track the current context
	MobileBarcodeScanner.Initialize (Application);
#endif

            var scanner = new ZXing.Mobile.MobileBarcodeScanner();

            var result = await scanner.Scan();

            return;

            if (await this.CheckPermissions())
            {
                this.backgroundServicesManagerService.StartService(ForegroundServiceList.SCANNER_SERVICE);
            }
        }
        private void StopService()
        {
            this.backgroundServicesManagerService.StopService(ForegroundServiceList.SCANNER_SERVICE);
        }

        private void OnScanResult()
        {

        }

    }
}