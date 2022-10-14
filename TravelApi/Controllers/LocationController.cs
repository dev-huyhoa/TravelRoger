using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using PrUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Travel.Data.Interfaces;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel;
using Travel.Shared.ViewModels.Travel.DistrictVM;
using Travel.Shared.ViewModels.Travel.WardVM;
using TravelApi.Hubs;

namespace TravelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private ILocation location;
        private Notification message;
        private Response res;
        private IHubContext<TravelHub, ITravelHub> _messageHub;

        public LocationController(ILocation _location
             , IHubContext<TravelHub, ITravelHub> messageHub)
        {
            location = _location;
            res = new Response();
            _messageHub = messageHub;

        }

        [HttpGet]
        [AllowAnonymous]
        [Route("gets-province")]
        public  object GetsProvince()
        {
       
            res = location.GetsProvince();
            return Ok(res);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("gets-district")]
        public object GetsDistrict()
        {
            res = location.GetsDistrict();
            return Ok(res);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("gets-ward")]
        public object GetsWard()
        {
            res = location.GetsWard();
            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        [Route("search-province")]
        public object SearchProvince([FromBody] JObject frmData)
        {
            res = location.SearchProvince(frmData);
            return Ok(res);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("search-district")]
        public object SearchDistrict([FromBody] JObject frmData)
        {
            res = location.SearchDistrict(frmData);
            return Ok(res);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("search-ward")]
        public object SearchWard([FromBody] JObject frmData)
        {
            res = location.SearchWard(frmData);
            return Ok(res);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("create-province")]
        public object CreateProvince([FromBody] JObject frmData)
        {
            message = null;
            var result = location.CheckBeforeSaveProvince(frmData, ref message, false);
            if (message == null)
            {
                var createObj = JsonSerializer.Deserialize<CreateProvinceViewModel>(result);
                res = location.CreateProvince(createObj);
                _messageHub.Clients.All.Insert();
            }
            else
            {
                res.Notification = message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        [Route("create-district")]
        public object CreateDistrict([FromBody] JObject frmData)
        {
            message = null;
            var result = location.CheckBeforeSaveDistrict(frmData, ref message, false);
            if (message == null)
            {
                var createObj = JsonSerializer.Deserialize<CreateDistrictViewModel>(result);
                res = location.CreateDistrict(createObj);
                _messageHub.Clients.All.Insert();
            }
            else
            {
                res.Notification = message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        [Route("create-ward")]
        public object CreateWard([FromBody] JObject frmData)
        {
            message = null;
            var result = location.CheckBeforeSaveWard(frmData, ref message, false);
            if (message == null)
            {
                var createObj = JsonSerializer.Deserialize<CreateWardViewModel>(result);
                res = location.CreateWard(createObj);
                _messageHub.Clients.All.Insert();
            }
            else
            {
                res.Notification = message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        [Route("update-province")]
        public object UpdateProvince([FromBody] JObject frmData)
        {
            message = null;
            var result = location.CheckBeforeSaveProvince(frmData, ref message, true);
            if (message == null)
            {
                var updateObj = JsonSerializer.Deserialize<UpdateProvinceViewModel>(result);
                res = location.UpdateProvince(updateObj);
            }
            else
            {
                res.Notification = message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        [Route("update-district")]
        public object UpdateDistrict([FromBody] JObject frmData)
        {
            message = null;
            var result = location.CheckBeforeSaveDistrict(frmData, ref message, true);
            if (message == null)
            {
                var updateObj = JsonSerializer.Deserialize<UpdateDistrictViewModel>(result);
                res = location.UpdateDistrict(updateObj);
            }
            else
            {
                res.Notification = message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        [Route("update-ward")]
        public object UpdateWard([FromBody] JObject frmData)
        {
            message = null;
            var result = location.CheckBeforeSaveWard(frmData, ref message, true);
            if (message == null)
            {
                var updateObj = JsonSerializer.Deserialize<UpdateWardViewModel>(result);
                res = location.UpdateWard(updateObj);
            }
            else
            {
                res.Notification = message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        [Route("delete-province")]
        public object DeleteProvince([FromBody] JObject frmData)
        {
            res = location.DeleteProvince(frmData);
            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        [Route("delete-district")]
        public object DeleteDistrict([FromBody] JObject frmData)
        {
            res = location.DeleteDistrict(frmData);
            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        [Route("delete-ward")]
        public object DeleteWard([FromBody] JObject frmData)
        {
            res = location.DeleteWard(frmData);
            return Ok(res);
        }
    }
}
