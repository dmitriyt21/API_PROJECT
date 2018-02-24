using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Http;
using System.Net;

namespace ErezAPI
{
    [Route("[controller]")]
    public class MultipartController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;

        public MultipartController(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            return new MultipartResult()
        {
            new MultipartContent()
            {
                ContentType = "text/plain",
                FileName = "File.txt",
                Stream = this.OpenFile("File.txt")
            },
            new MultipartContent()
            {
                ContentType = "application/json",
                FileName = "File.json",
                Stream = this.OpenFile("File.json")
            }
        };
        }

        private Stream OpenFile(string relativePath)
        {
            return System.IO.File.Open(
                Path.Combine(this.hostingEnvironment.WebRootPath, relativePath),
                FileMode.Open,
                FileAccess.Read);
        }

        public async void WriteContentToStream(Stream outputStream, HttpContent content, TransportContext transportContext)
        {
            //path of file which we have to read//
            string webRootPath = hostingEnvironment.WebRootPath;
            string filePath = Path.Combine(webRootPath, "~/bvg.mp4");
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