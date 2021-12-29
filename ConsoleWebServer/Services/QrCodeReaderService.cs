using System;
using System.Collections.Generic;
using System.DrawingCore;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace ConsoleWebServer.Services
{
    public interface IQrCodeReaderService
    {
        Result DecodeQrBuffer(byte[] buffer);
        Result DecodeQrBase64(string base64);
    }
    public class QrCodeReaderService : IQrCodeReaderService
    {
        private readonly QRCodeReader reader;
        public QrCodeReaderService()
        {
            this.reader = new QRCodeReader();
        }

        private BinaryBitmap StreamToBinaryBitmap(Stream stream)
        {
            var bitMap = (Bitmap)Bitmap.FromStream(stream);
            var source = new ZXing.ZKWeb.BitmapLuminanceSource(bitMap);
            return new BinaryBitmap(new HybridBinarizer(source));
        }
        private BinaryBitmap BufferToBinaryBitmap(byte[] buffer)
        {
            var stream = new MemoryStream(buffer);
            var binaryBitmap = this.StreamToBinaryBitmap(stream);
            stream.Dispose();

            return binaryBitmap;
        }

        public Result DecodeQrBuffer(byte[] buffer)
        {
            var bitmap = this.BufferToBinaryBitmap(buffer);

            return this.reader.decode(bitmap);            
        }

        public Result DecodeQrBase64(string base64)
        {
            var bytes = Convert.FromBase64String(base64);
            var stream = new MemoryStream(bytes);

            var bitmap = this.StreamToBinaryBitmap(stream);

            stream.Dispose();

            return this.reader.decode(bitmap);
        }
    }
}
