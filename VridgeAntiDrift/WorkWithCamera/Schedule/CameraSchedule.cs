using Android.Hardware.Camera2;
using System.Timers;

namespace Task2
{
    public class CameraSchedule
    {
        const int SECONDS_TO_MILLISECONDS = 1000;
        const int MINUTES_TO_MILLISECONDS = 60000;
        const int HOURS_TO_MILLISECONDS = 3600000;
        const int EVERY_MINUTE = 60000;

        private Timer timer;
        HiddenCamera _hiddenCamera;
        long _periodInMilliseconds;

        public CameraSchedule(CameraManager cameraManager, int period, Period photographingPeriod)
        {
            _hiddenCamera = new HiddenCamera(cameraManager);
            this._hiddenCamera.PictureCallback.OnFinished += () => this.timer.Start();
            _periodInMilliseconds = ConvertToMilliseconds(period, photographingPeriod);
        }

        private long ConvertToMilliseconds(int period, Period photographingPeriod)
        {
            return photographingPeriod switch
            {
                Period.InMiliseconds => period,
                Period.InSeconds => period * SECONDS_TO_MILLISECONDS,
                Period.InMinutes => period * MINUTES_TO_MILLISECONDS,
                Period.InHours => period * HOURS_TO_MILLISECONDS,
                _ => EVERY_MINUTE,
            };
        }

        public void StartTimerPhotography()
        {
            this.timer = new Timer(this._periodInMilliseconds);
            this.timer.Elapsed += Timer_Elapsed;
            this.timer.AutoReset = false;
            this.timer.Start();            
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this.timer.Stop();
            
            this._hiddenCamera.TakePhoto();
        }

        public void Stop()
        {
            this.timer.Close();
            _hiddenCamera.StopPreviewAndFreeCamera();
        }
    }
}