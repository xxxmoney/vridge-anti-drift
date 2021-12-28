using ConsoleWebServer.DTO;
using ConsoleWebServer.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.Common;

namespace ConsoleWebServer.Controllers
{
    [Route("Qr")]
    public class QrController : Controller
    {
        private readonly IQrCodeReader qrCodeReader;
        public QrController(IQrCodeReader qrCodeReader)
        {
            this.qrCodeReader = qrCodeReader;
        }

        [DisableRequestSizeLimit]
        [Route(nameof(DecodeBuffer))]
        [HttpPost]
        public IActionResult DecodeBuffer([FromBody] BufferDto buffer)
        {
            var result = this.qrCodeReader.DecodeQrBuffer(buffer.Buffer);

            return Ok(result);
        }

        [DisableRequestSizeLimit]
        [Route(nameof(DecodeBase64))]
        [HttpPost]
        public IActionResult DecodeBase64([FromBody] string base64)
        {
            var result = this.qrCodeReader.DecodeQrBase64(base64);

            return Ok(result);
        }
    }
}
