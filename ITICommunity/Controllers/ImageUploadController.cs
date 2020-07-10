using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ITICommunity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        public static IWebHostEnvironment _environment;
        public ImageUploadController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }
        public class FileUploadAPI
        {
            public IFormFile file { get; set; }
        }
        [HttpPost]
        public async Task<string> FileUpload([FromForm] FileUploadAPI objFile)
        {
            try
            {  
                if (objFile.file.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\Images/Upload\\"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\Images/Upload\\");
                    }
                    using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\Images/Upload\\" + objFile.file.FileName))
                    {
                        objFile.file.CopyTo(filestream);
                        filestream.Flush();
                        return "/Images/Upload/" + objFile.file.FileName;
                    }
                }
                else
                {
                    return "Failed";
                }
            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
        }
    }
}