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
using Travel.Shared.ViewModels.Travel.PromotionVM;
using TravelApi.Hubs;

namespace TravelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotions _promotion;
        private Notification message;
        private Response res;

        public PromotionController(IPromotions promotion)
        {
            _promotion = promotion;
            res = new Response();
        }

        [HttpGet]
        [Authorize]
        [Route("gets-promotion")]
        public object GetsPromotion(bool isDelete)
        {
            res = _promotion.GetsPromotion(isDelete);
            return Ok(res);
        }

        [HttpGet]
        [Authorize]
        [Route("gets-promotion-waiting")]
        public object GetsWaitingPromotion(Guid idUser)
        {
            res = _promotion.GetsWaitingPromotion(idUser);
            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        [Route("create-promotion")]
        public object Create([FromBody] JObject frmData)
        {
            message = null;
            var result = _promotion .CheckBeforSave(frmData, ref message, Travel.Shared.Ultilities.Enums.TypeService.Hotel, false);
            if (message == null)
            {
                var createObj = JsonSerializer.Deserialize<CreatePromotionViewModel>(result);
                res = _promotion.CreatePromotion(createObj);
            }
            return Ok(res);
        }


        [HttpPost]
        [Authorize]
        [Route("update-promotion")]
        public object UpdatePromotion(int id,[FromBody] JObject frmData)
        {
            message = null;
            var result = _promotion.CheckBeforSave(frmData, ref message, Travel.Shared.Ultilities.Enums.TypeService.Hotel, true);
            if (message == null)
            {
                var updateObj = JsonSerializer.Deserialize<UpdatePromotionViewModel>(result);
                res = _promotion.UpdatePromotion(updateObj);
            }
            return Ok(res);
        }

    }
}
