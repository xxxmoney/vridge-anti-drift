using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Xamarin.Forms;
using MobileScanner.Droid.ForegroundServices;
using Android.Content;
using MobileScanner.Services;
using MobileScanner.ForegroundServices;

namespace MobileScanner.Droid
{    
    [Activity(Label = "MobileScanner", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            //Registers starting and stopping foreground or background service(s).
            MessagingCenter.Unsubscribe<ForegroundServicesManagerService, string>(this, ForegroundServiceList.SCANNER_SERVICE);
            MessagingCenter.Subscribe<ForegroundServicesManagerService, string>(this, ForegroundServiceList.SCANNER_SERVICE, (sender, value) => 
            {
                if (value == ServiceStateEnum.START.ToString("d"))
                    StartForegroundService(new Intent(this, typeof(ForegroundScannerService)));
                else if(value == ServiceStateEnum.STOP.ToString("d"))
                    StopService(new Intent(this, typeof(ForegroundScannerService)));
            });
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}