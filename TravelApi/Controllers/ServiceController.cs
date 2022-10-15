using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Travel.Data.Interfaces;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel.ContractVM;

namespace TravelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IService _serviceRes;
        private Notification message;
        private Response res;

        public ServiceController(IService service)
        {
            _serviceRes = service;
            res = new Response();
        }

        [HttpGet()]
        [Authorize]
        [Route("gets-hotel")]
        public object GetHotel()
        {
            res = _serviceRes.GetHotel();
            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        [Route("create-hotel")]
        public object CreateHotel([FromBody] JObject frmData)
        {
            message = null;
            var result = _serviceRes.CheckBeforSave(frmData, ref message,Travel.Shared.Ultilities.Enums.TypeService.Hotel, false);
            if (message == null)
            {
                var createObj = JsonSerializer.Deserialize<CreateHotelViewModel>(result);
                res = _serviceRes.CreateHotel(createObj);
            }
            return Ok(res);
        }

        [HttpGet()]
        [Authorize]
        [Route("gets-restaurant")]
        public object GetRestaurant()
        {
            res = _serviceRes.GetRestaurant();
            return Ok(res);
        }
        [HttpPost]
        [Authorize]
        [Route("create-restaurant")]
        public object CreateRestaurant([FromBody] JObject frmData)
        {

            message = null;
            var result = _serviceRes.CheckBeforSave(frmData, ref message, Travel.Shared.Ultilities.Enums.TypeService.Restaurant, false) ;
            if (message == null)
            {
                var createObj = JsonSerializer.Deserialize<CreateRestaurantViewModel>(result);
                res = _serviceRes.CreateRestaurant(createObj);
            }
            return Ok(res);
        }
        [HttpPost]
        [Authorize]
        [Route("create-place")]
        public object CreatePlace([FromBody] JObject frmData)
        {

            message = null;
            var result = _serviceRes.CheckBeforSave(frmData, ref message, Travel.Shared.Ultilities.Enums.TypeService.Place, false);
            if (message == null)
            {
                var createObj = JsonSerializer.Deserialize<CreatePlaceViewModel>(result);
                res = _serviceRes.CreatePlace(createObj);
            }
            return Ok(res);
        }
    }
}
