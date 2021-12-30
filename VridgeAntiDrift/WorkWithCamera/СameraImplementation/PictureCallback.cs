using Android.Hardware;
using Java.IO;
using Android.Icu.Text;
using Java.Util;
using System;
using Android.Util;
using Task2.WebClient;

namespace Task2
{    
    public class PictureCallback : Java.Lang.Object, Camera.IPictureCallback
    {
        public int CameraId;
        public event Action OnFinished;

        public PictureCallback()
        {
        }
        
        public void OnPictureTaken(byte[] data, Camera camera)
        {
            try 
            {
                string base64 = Convert.ToBase64String(data);

                RestSharp.IRestResponse result = QrWebRequestor
                    .GetInstance()
                    .RecenterQrBase64(base64);                
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
            finally
            {
                this.OnFinished();
            }
        }
        
        
    }
}