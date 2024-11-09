using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AFF_BE.Api.Controllers.FIle
{
    [Route("api/file")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        [HttpGet("images/{filename}")]
        public IActionResult getImage(string filename)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Public\\Images\\") + filename;
            if (!System.IO.File.Exists(filePath))
                return BadRequest("Not found image");
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "image/jpeg");
        }
        [HttpGet("video/{filename}")]
        public IActionResult getVideo(string filename)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Public\\Videos\\") + filename;
            if (!System.IO.File.Exists(filePath))
                return BadRequest("Not found video");
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

            return File(fileBytes, "video/mp4");
        }
    }


}
