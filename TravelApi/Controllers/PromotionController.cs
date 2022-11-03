using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Travel.Data.Interfaces;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel;
using TravelApi.Hubs;

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

        [HttpGet]
        [Authorize]
        [Route("gets-promotion")]
        public object Gets()
        {
            res = promotion.Gets();
            return Ok(res);
        }

        [HttpGet]
        [Authorize]
        [Route("gets-promotion-waiting")]
        public object GetsWaitingPromotion()
        {
            res = promotion.GetWaitingPromotion();
            return Ok(res);
        }

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


        [HttpPost]
        [Authorize]
        [Route("update-promotion")]
        public object UpdatePromotion(int id,[FromBody] JObject frmData)
        { 
            message = null;
            var result = promotion.CheckBeforSave(frmData, ref message, true);
            if (message == null)
            {
                var updateObj = JsonSerializer.Deserialize<UpdatePromotionViewModel>(result);
                res = promotion.UpdatePromotion(id,updateObj);
            }
            else
            {
                res.Notification = message;
            }
            return Ok(res);
        }

    }
}
