using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travel.Data.Interfaces;
using Travel.Shared.ViewModels;

namespace TravelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : Controller
    {
        private INews news;
        private Response res;
        private Notification message;

        public NewsController(INews _news)
        {
            news = _news;
            res = new Response();
        }

        [HttpPost]
        [Authorize]
        [Route("UploadBanner")]
        public object UploadBanner(IFormCollection frmdata, ICollection<IFormFile> files)
        {
            try
            {
                res.Notification = news.UploadBanner(frmdata, files);

                return Ok(res);
            }
            catch (Exception)
            {
                return Ok(res);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("UploadBanner")]
        public object GetBanners()
        {
            try
            {

                return Ok(res);
            }
            catch (Exception)
            {
                return Ok(res);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("list-banner")]
        public object GetBanner()
        {
            res = news.GetBanner();
            return Ok(res);
        }


        //[HttpPost]
        //[Authorize]
        //[Route("create-news")]
        //public object CreateNews(IFormCollection frmdata, IFormFile file)
        //{
        //    message = null; IFormCollection frmdata, IFormFile file, ref Notification _message
        //    var result = news.CheckBeforeSave(frmdata, file, ref message, false);
        //    if (message == null)
        //    {
        //        var createObj = JsonSerializer.Deserialize<CreateEmployeeViewModel>(result);
        //        res = employee.CreateEmployee(createObj);
        //        _messageHub.Clients.All.Init();
        //    }
        //    else
        //    {
        //        res.Notification = message;
        //    }
        //    return Ok(res);
        //}

        //[HttpPost]
        //[Authorize]
        //[Route("delete-banner")]
        //public object Delete([FromBody] JObject frmData)
        //{

        //    //var result = news. .CheckBeforeSave(frmData, ref message);
        //    //if (message == null)
        //    //{
        //    //    res = employee.Update(result);
        //    //}
        //    //return Ok(res);
        //    return res;
        //}
    }
}
