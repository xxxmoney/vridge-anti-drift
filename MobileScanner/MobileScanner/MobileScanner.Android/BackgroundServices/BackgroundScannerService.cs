using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MobileScanner.BackgroundServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace MobileScanner.Droid.BackgroundServices
{
    [Service(Label = BackgroundServiceList.SCANNER_SERVICE)]
    public class BackgroundScannerService : Service, IBackgroundService
    {
        private int counter = 0;
        private bool isRunning = true;

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), this.TimerHandler);

            return StartCommandResult.Sticky;
        }
        private bool TimerHandler()
        {
            //DO STUFF

            return this.isRunning;
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override void OnDestroy()
        {
            this.StopSelf();
            this.isRunning = false;
            base.OnDestroy();
        }
    }
}