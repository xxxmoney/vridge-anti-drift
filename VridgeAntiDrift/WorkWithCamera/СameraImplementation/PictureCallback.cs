using Android.Hardware;
using Java.IO;
using Android.Icu.Text;
using Java.Util;
using System;
using Android.Util;
using Task2.BarcodeReader;

namespace Task2
{    
    class PictureCallback : Java.Lang.Object, Camera.IPictureCallback
    {
        private int _cameraID;

        public PictureCallback(int cameraID)
        {
            _cameraID = cameraID;
        }
        
        public void OnPictureTaken(byte[] data, Camera camera)
        {
            try 
            {
                ZXing.Result result = QrCodeReader.GetInstance().ScanImage(data);
                if (result != null)
                {
                    
                }
            }
            catch (Exception e)
            {
                _ = e.StackTrace;
            }
        }
        
        
    }
}