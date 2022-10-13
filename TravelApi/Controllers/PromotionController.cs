using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Travel.Data.Interfaces;
using Travel.Shared.ViewModels;

namespace TravelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private IPromotions promotion;
        private Notification message;
        private Response res;
        public PromotionController(IPromotions _promotion)
        {
            promotion = _promotion;
            res = new Response();
        }

        //[HttpGet]
        //[Authorize]
        //[Route("gets-promotion")]
        //public object Gets()
        //{
        //    res = promotion.Gets();
        //    return Ok(res);
        //}

        [HttpPost]
        [Authorize]
        [Route("create-promotion")]
        public object Create([FromBody] JObject frmData)
        {
            message = null;
            var result = promotion.CheckBeforSave(frmData, ref message, false);
            if (message == null)
            {
                var createObj = JsonSerializer.Deserialize<CreatePromotionViewModel>(result);
                res = promotion.Create(createObj);
            }
            return Ok(res);
        }
    }
}
