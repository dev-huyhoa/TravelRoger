﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public NewsController(INews _news)
        {
            news = _news;
            res = new Response();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("UploadBanner")]
        public object UploadBanner(IFormCollection frmdata, ICollection<IFormFile> files, string name)
        {
            try
            {
                res.Notification = news.UploadBanner(frmdata, files, name);

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
        [Route("gets-banner")]
        public object GetBanner()
        {
            res = news.GetBanner();
            return Ok(res);
        }
    }
}
