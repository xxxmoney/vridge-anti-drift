using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using MobileScanner.ForegroundServices;
using MobileScanner.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileScanner.Droid.ForegroundServices
{
    [Service(Label = ForegroundServiceList.SCANNER_SERVICE)]
    public class ForegroundScannerService : Service, IForeroundService
    {
        private bool isRunning = true;
        private const int SERVICE_RUNNING_NOTIFICATION_ID = 666;

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {           
            StartForeground(SERVICE_RUNNING_NOTIFICATION_ID, this.CreateNotification());

            Device.StartTimer(TimeSpan.FromSeconds(1), this.TimerHandler);

            return StartCommandResult.Sticky;
        }
        private Notification CreateNotification()
        {
            string NOTIFICATION_CHANNEL_ID = "com.app.vridge_anti_drift";
            string channelName = "Vridge Anti-Drift Service";
            NotificationChannel chan = new NotificationChannel(NOTIFICATION_CHANNEL_ID, channelName, NotificationImportance.None);
            chan.LockscreenVisibility = NotificationVisibility.Private;
            NotificationManager manager = (NotificationManager)GetSystemService(NotificationService);
            manager.CreateNotificationChannel(chan);

            NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(this, NOTIFICATION_CHANNEL_ID);
            Notification notification = notificationBuilder.SetOngoing(true)
                    .SetSmallIcon(Resource.Drawable.icon_about)
                    .SetContentTitle("App is running in background")
                    .SetPriority(1)
                    .SetCategory(Notification.CategoryService)
                    .Build();

            return notification;
        }

        private async Task ScanAsync()
        {
            
            

            //byte[] bytes = new byte[stream.Length];
            //stream.Read(bytes, 0, bytes.Length);
            //stream.Seek(0, SeekOrigin.Begin);

            //var result = await GoogleVisionBarCodeScanner.Methods.ScanFromImage(bytes);

            //stream.Dispose();
        }

        
        private bool TimerHandler()
        {
            if (!Android.Provider.Settings.CanDrawOverlays(this))
            {
                Shell.Current.DisplayAlert("PERMISSION", "Please grant permission for DrawOver.", "OK");
                this.OnDestroy();
            }           
            else
            {
                var task = TaskEx.Run(ScanAsync);
                task.Wait();
            }
            
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
            StopForeground(true);
            base.OnDestroy();
        }

    }
}