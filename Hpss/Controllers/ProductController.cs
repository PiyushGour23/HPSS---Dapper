using Hpss.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Hpss.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IWebHostEnvironment environment)
        {
            _webHostEnvironment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        [HttpPut("UploadSingleImage")]
        public async Task<IActionResult> UploadImage (IFormFile formFile, string productcode)
        {
            APIResponse response = new APIResponse();
            try
            {
                string Filepath = GetFilePath(productcode);
                if (!System.IO.Directory.Exists(Filepath))
                {
                    System.IO.Directory.CreateDirectory(Filepath);
                }
                string imagepath = Filepath + "\\" + productcode + ".jpg";
                if (System.IO.File.Exists(imagepath))
                {
                    System.IO.File.Delete(imagepath);
                }
                using (FileStream stream = System.IO.File.Create(imagepath))
                {
                    await formFile.CopyToAsync(stream);
                    response.ResponseCode = 200;
                    response.Result = "Pass";
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "Fail";
            }
            return Ok(response);
        }

        [HttpPut("UploadMultiImage")]
        public async Task<IActionResult> UploadMultiImage(IFormFileCollection formFile, string productcode)
        {
            APIResponse response = new APIResponse();
            int passcount = 0;
            int errorcount = 0;
            try
            {
                string Filepath = GetFilePath(productcode);
                if (!System.IO.Directory.Exists(Filepath))
                {
                    System.IO.Directory.CreateDirectory(Filepath);
                }

                foreach(var file in formFile) 
                {
                    string imagepath = Filepath + "\\" + file.FileName;
                    if (System.IO.File.Exists(imagepath))
                    {
                        System.IO.File.Delete(imagepath);
                    }
                    using (FileStream stream = System.IO.File.Create(imagepath))
                    {
                        await file.CopyToAsync(stream);
                        passcount++;
                    }
                }

            }
            catch (Exception ex)
            {
                errorcount++;
                response.ErrorMessage = ex.Message;
            }

            response.ResponseCode = 200;
            response.Result = passcount + "Files Uploaded" + errorcount + " Failed to upload";
            return Ok(response);
        }



        [HttpGet("GetSingleImage")]
        public async Task<IActionResult> GetImage(string productcode)
        {
            string ImageURL = string.Empty;
            string hosturl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            try
            {
                string Filepath = GetFilePath(productcode);
                string imagepath = Filepath + "\\" + productcode + ".jpg";
                if(System.IO.File.Exists(imagepath))
                {
                    ImageURL = hosturl + "/Upload/product/" + productcode + "/" + productcode + ".jpg";
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex) 
            {
                throw ex;
            }
            return Ok(ImageURL);
        }

        [HttpGet("GetMultipleImage")]
        public async Task<IActionResult> GetMultipleImage(string productcode)
        {
            List<string> ImageURL = new List<string>();
            string hosturl = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}";
            try
            {
                string Filepath = GetFilePath(productcode);

                if (System.IO.Directory.Exists(Filepath))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(Filepath);
                    FileInfo[] fileInfos = directoryInfo.GetFiles();
                    foreach (FileInfo fileInfo in fileInfos)
                    {
                        string fileName = fileInfo.Name;
                        string imagepath = Filepath + "\\" + fileName;
                        if (System.IO.File.Exists(imagepath))
                        {
                            string _imageURL = hosturl + "/Upload/product/" + productcode + "/" + fileName;
                            ImageURL.Add(_imageURL);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok(ImageURL);
        }


        [HttpGet("DownloadImage")]
        public async Task<IActionResult> DownloadImage(string productcode)
        {
            try
            {
                string Filepath = GetFilePath(productcode);
                string imagepath = Filepath + "\\" + productcode + ".jpg";
                if (System.IO.File.Exists(imagepath))
                {
                    MemoryStream memoryStream = new MemoryStream();
                    using (FileStream fileStream = new FileStream(imagepath, FileMode.Open))
                    {
                        await fileStream.CopyToAsync(memoryStream);
                    }
                    memoryStream.Position = 0;
                    return File(memoryStream, "image/png", productcode + ".jpg");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpDelete("SingleImageRemove")]
        public async Task<IActionResult> RemoveImage(string productcode)
        {
            try
            {
                string Filepath = GetFilePath(productcode);
                string imagepath = Filepath + "\\" + productcode + ".jpg";
                if (System.IO.File.Exists(imagepath))
                {
                    System.IO.File.Delete(imagepath);
                    return Ok("Pass");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetMultipleImageRemove")]
        public async Task<IActionResult> GetMultipleImageRemove(string productcode)
        {
            List<string> ImageURL = new List<string>();
            try
            {
                string Filepath = GetFilePath(productcode);

                if (System.IO.Directory.Exists(Filepath))
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(Filepath);
                    FileInfo[] fileInfos = directoryInfo.GetFiles();
                    foreach (FileInfo fileInfo in fileInfos)
                    {
                        fileInfo.Delete();
                    }
                    return Ok("Pass");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok(ImageURL);
        }

        [NonAction]
        private string GetFilePath(string productcode)
        {
            return _webHostEnvironment.WebRootPath + "\\Upload\\product\\" + productcode;
        }
    }
}