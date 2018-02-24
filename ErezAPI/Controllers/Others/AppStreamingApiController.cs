using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace ErezAPI
{
    public class AppStreamingApiController : Controller
    {
        private IHostingEnvironment _hostingEnvironment;

        public AppStreamingApiController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpGet]
        [Route("api/stream")]
        public Task Stream()
        {
            //try
            //{
                //if (!ModelState.IsValid)
                //    return Request.CreateResponse(HttpStatusCode.BadRequest, 
                //        ErrorHelper.CreateAppErrorResponseModel(model.requestID, ModelState));

                //AssetsLogic logic = new AssetsLogic();

                //string fileName = logic.GetResourceName(model.assetID);

                //if(string.IsNullOrWhiteSpace(fileName))
                //    return Request.CreateResponse(HttpStatusCode.NotFound, 
                //        ErrorHelper.CreateAppErrorResponseModel(model.requestID, HttpStatusCode.NotFound, "Asset not found."));

                //string fullPathAndFileName = Paths.VideosFolder + "\\" + fileName;

                //if (!File.Exists(fullPathAndFileName))
                //    return Request.CreateResponse(HttpStatusCode.BadRequest,
                //        ErrorHelper.CreateAppErrorResponseModel(model.requestID,HttpStatusCode.BadRequest, "This is not a local video you're trying to stream."));

                //string extension = Path.GetExtension(fileName);
                //extension = extension.Substring(1); // ".mp4" => "mp4"
                Response.ContentType = "video/mp4";
                string webRootPath = _hostingEnvironment.WebRootPath;
                string filePath = Path.Combine(webRootPath, "bg2.mp4");

                StreamHelper streamHelper = new StreamHelper(filePath);
                var sourceStream = new PushStreamContent((Action<Stream, HttpContent, TransportContext>)streamHelper.WriteToStream);
                return sourceStream.CopyToAsync(HttpContext.Response.Body);
                //return Ok(aaa);
                //HttpResponseMessage response = Request.CreateResponse();
                //response.Content = new PushStreamContent((Action<Stream, HttpContent, TransportContext>)streamHelper.WriteToStream, 
                    //new MediaTypeHeaderValue("video/" + extension));
                //return response;
            //}
            //catch (Exception ex)
            //{
            //    return Task;
            //}
        }
    }
}