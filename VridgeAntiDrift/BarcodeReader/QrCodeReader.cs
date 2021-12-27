using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Icu.Text;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace Task2.BarcodeReader
{
    public class QrCodeReader
    {
        private readonly QRCodeReader reader;
        private static QrCodeReader instance;
        public QrCodeReader()
        {
            this.reader = new QRCodeReader();
        }

        public static QrCodeReader GetInstance()
        {
            if (instance == null)
                instance = new QrCodeReader();

            return instance;
        }
        
        public ZXing.Result ScanImage(byte[] buffer)
        {
            Bitmap bitmap = BitmapFactory.DecodeByteArray(buffer, 0, buffer.Length);

            int width = (int)bitmap.Width;
            int height = (int)bitmap.Height;

            bitmap.Dispose();

            LuminanceSource source = new PlanarYUVLuminanceSource(buffer, width, height,
                0, 0, width, height, false);
            BinaryBitmap binaryBitmap = new BinaryBitmap(new HybridBinarizer(source));

            return this.reader.decode(binaryBitmap);
        }

        
    }
}