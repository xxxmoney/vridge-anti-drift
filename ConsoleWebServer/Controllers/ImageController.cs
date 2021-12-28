using ConsoleWebServer.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleWebServer.Controllers
{
    [Route("Image")]
    public class ImageController : Controller
    {
        [DisableRequestSizeLimit]
        [Route(nameof(DecodeBufferToBase64))]
        [HttpPost]
        public IActionResult DecodeBufferToBase64([FromBody] BufferDto buffer)
        {
            var result = Convert.ToBase64String(buffer.Buffer);

            return Ok(result);            
        }
    }
}
