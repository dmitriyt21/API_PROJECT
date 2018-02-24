using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
namespace ErezAPI

{
    [Route("api/videostream")]
    public class VideoStreamController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;
        string FileLength;

        public VideoStreamController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        [Route("bgv2")]
        public Task Get2()
        {
            Response.ContentType = "video/mp4";
            Response.Headers["Content-Accept"] = Response.ContentType;
            var sourceStream = new PushStreamContent((Action<Stream, HttpContent, TransportContext>)WriteContentToStream);// get the source stream
            return sourceStream.CopyToAsync(HttpContext.Response.Body);
        }

        [HttpGet]
        [Route("bgv")]
        public async Task<IActionResult> Get()
        {
            try
            {

                Response.StatusCode = 200;

                Response.ContentType = "video/mp4";
                Response.Headers["Content-Accept"] = Response.ContentType;
                Response.Headers["Content-Type"] = "application/octet-stream";
                Response.Headers["Content-Disposition"] = "attachment";

                return Ok(new PushStreamContent((Action<Stream, HttpContent, TransportContext>)WriteContentToStream));

            }
            catch (Exception ex)
            {
                return Ok(new PushStreamContent((Action<Stream, HttpContent, TransportContext>)WriteContentToStream));
            }
        }

        public async void WriteContentToStream(Stream outputStream, HttpContent content, TransportContext transportContext)
        {
            //path of file which we have to read//
            string webRootPath = _hostingEnvironment.WebRootPath;
            string filePath = Path.Combine(webRootPath, "bgv2.mp4");
            //here set the size of buffer, you can set any size  
            int bufferSize = 1000;
            byte[] buffer = new byte[bufferSize];
            //here we re using FileStream to read file from server//  
            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                int totalSize = (int)fileStream.Length;
                /*here we are saying read bytes from file as long as total size of file 
 
                is greater then 0*/
                while (totalSize > 0)
                {
                    int count = totalSize > bufferSize ? bufferSize : totalSize;
                    //int count = totalSize > bufferSize ? bufferSize : bufferSize;
                    //here we are reading the buffer from orginal file  
                    int sizeOfReadedBuffer = fileStream.Read(buffer, 0, count);
                    //here we are writing the readed buffer to output//  
                    await outputStream.WriteAsync(buffer, 0, sizeOfReadedBuffer);
                    //and finally after writing to output stream decrementing it to total size of file.  
                    totalSize -= sizeOfReadedBuffer;
                }
            }
        }
    }
}