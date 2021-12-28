using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task2.WebClient
{
    public class QrWebRequestor
    {
        private static QrWebRequestor instance;

        private const string BASE_URL = "http://192.168.163.235:666";
        private readonly RestClient client;
        public QrWebRequestor()
        {
            this.client = new RestClient(BASE_URL);            
        }
        public static QrWebRequestor GetInstance()
        {
            if (instance == null)
                instance = new QrWebRequestor();

            return instance;
        }

        public IRestResponse PostQrBuffer(byte[] buffer)
        {            
            IRestRequest request = new RestRequest("Qr/DecodeQrBuffer")
                .AddJsonBody(new { Buffer = buffer });

            return this.client.Post(request);
        }
        public IRestResponse PostQrBase64(string base64)
        {
            IRestRequest request = new RestRequest("Qr/DecodeBase64")
                .AddBody(base64, "text/plain");

            var result = this.client.Post(request);
            return result;
        }

        public IRestResponse PostImageBuffer(byte[] buffer)
        {
            IRestRequest request = new RestRequest("Image/DecodeBufferToBase64")
                .AddJsonBody(new { Buffer = buffer });

            return this.client.Post(request);
        }

    }

}