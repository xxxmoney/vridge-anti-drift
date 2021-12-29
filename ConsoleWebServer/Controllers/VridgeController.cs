using ConsoleWebServer.DTO;
using ConsoleWebServer.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleWebServer.Controllers
{
    [Route("Vridge")]
    public class VridgeController : Controller
    {
        private readonly IQrCodeReaderService qrCodeReaderService;
        private readonly IVridgeService vridgeService;
        public VridgeController(
            IQrCodeReaderService qrCodeReaderService,
            IVridgeService vridgeService)
        {
            this.qrCodeReaderService = qrCodeReaderService;
            this.vridgeService = vridgeService;
        }

        [DisableRequestSizeLimit]
        [Route(nameof(RecenterQrBase64))]
        [HttpPost]
        public IActionResult RecenterQrBase64([FromBody] Base64Dto base64)
        {
            var result = this.qrCodeReaderService.DecodeQrBase64(base64.Base64);

            if (result != null) 
                this.vridgeService.Recenter();            

            return Ok();
        }

    }
}
