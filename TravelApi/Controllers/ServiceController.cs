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
        #region hotel
        [HttpPost]
        [Authorize]
        [Route("create-hotel")]
        public object CreateHotel([FromBody] JObject frmData)
        {
            message = null;
            var result = _serviceRes.CheckBeforSave(frmData, ref message, Travel.Shared.Ultilities.Enums.TypeService.Hotel, false);
            if (message == null)
            {
                var createObj = JsonSerializer.Deserialize<CreateHotelViewModel>(result);
                res = _serviceRes.CreateHotel(createObj);
            }
            return Ok(res);
        }
        [HttpGet()]
        [Authorize]
        [Route("gets-hotel")]
        public object GetHotel(bool isDelete)
        {
            res = _serviceRes.GetsHotel(isDelete);
            return Ok(res);
        }
        [HttpGet()]
        [Authorize]
        [Route("gets-hotel-waiting")]
        public object GetHotelWaiting(Guid idUser)
        {
            res = _serviceRes.GetsWaitingHotel(idUser);
            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        [Route("update-hotel")]
        public object UpdateHotel([FromBody] JObject frmData)
        {
            message = null;
            var result = _serviceRes.CheckBeforSave(frmData, ref message, Travel.Shared.Ultilities.Enums.TypeService.Hotel, true);
            if (message == null)
            {
                var updateObj = JsonSerializer.Deserialize<UpdateHotelViewModel>(result);
                res = _serviceRes.UpdateHotel(updateObj);
            }
            return Ok(res);
        }
        [HttpGet]
        [Authorize]
        [Route("approve-hotel")]
        public object ApproveHotel(Guid idHotel)
        {
            res = _serviceRes.ApproveHotel(idHotel);
            return Ok(res);
        }

        [HttpGet]
        [Authorize]
        [Route("refuse-hotel")]
        public object RefuseHotel(Guid idHotel)
        {
            res = _serviceRes.RefusedHotel(idHotel);
            return Ok(res);
        }
        [HttpGet]
        [Authorize]
        [Route("delete-hotel")]
        public object DeleteHotel(Guid idHotel, Guid idUser)
        {
            res = _serviceRes.DeleteHotel(idHotel, idUser);
            return Ok(res);
        }
        [HttpGet]
        [Authorize]
        [Route("restore-hotel")]
        public object RestoreHotel(Guid idHotel, Guid idUser)
        {
            res = _serviceRes.RestoreHotel(idHotel, idUser);
            return Ok(res);
        }
        #endregion

        #region restaurant
        [HttpGet()]
        [Authorize]
        [Route("gets-restaurant-waiting")]
        public object GetWaitingRestaurant(Guid idUser)
        {
            res = _serviceRes.GetsWaitingRestaurant(idUser);
            return Ok(res);
        }

        [HttpGet()]
        [Authorize]
        [Route("gets-restaurant")]
        public object GetRestaurant(bool isDelete)
        {
            res = _serviceRes.GetsRestaurant(isDelete);
            return Ok(res);
        }
        [HttpPost]
        [Authorize]
        [Route("create-restaurant")]
        public object CreateRestaurant([FromBody] JObject frmData)
        {
            message = null;
            var result = _serviceRes.CheckBeforSave(frmData, ref message, Travel.Shared.Ultilities.Enums.TypeService.Restaurant, false);
            if (message == null)
            {
                var createObj = JsonSerializer.Deserialize<CreateRestaurantViewModel>(result);
                res = _serviceRes.CreateRestaurant(createObj);
            }
            return Ok(res);
        }

        [HttpGet]
        [Authorize]
        [Route("approve-restaurant")]
        public object ApproveRestaurant(Guid idRestaurant)
        {
            res = _serviceRes.ApproveRestaurant(idRestaurant);
            return Ok(res);
        }

        [HttpGet]
        [Authorize]
        [Route("refuse-restaurant")]
        public object RefuseRestaurant(Guid idRestaurant)
        {
            res = _serviceRes.RefusedRestaurant(idRestaurant);
            return Ok(res);
        }
        [HttpGet]
        [Authorize]
        [Route("delete-restaurant")]
        public object DeleteRestaurant(Guid idRestaurant, Guid idUser)
        {
            res = _serviceRes.DeleteRestaurant(idRestaurant, idUser);
            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        [Route("update-restaurant")]
        public object UpdateRestaurant([FromBody] JObject frmData)
        {

            message = null;
            var result = _serviceRes.CheckBeforSave(frmData, ref message, Travel.Shared.Ultilities.Enums.TypeService.Restaurant, true);
            if (message == null)
            {
                var updateObj = JsonSerializer.Deserialize<UpdateRestaurantViewModel>(result);
                res = _serviceRes.UpdateRestaurant(updateObj);
            }
            return Ok(res);
        }
        [HttpGet]
        [Authorize]
        [Route("restore-restaurant")]
        public object RestoreRestaurant(Guid idRestaurant, Guid idUser)
        {
            res = _serviceRes.RestoreRestaurant(idRestaurant, idUser);
            return Ok(res);
        }
        #endregion

        #region place
        [HttpGet()]
        [Authorize]
        [Route("gets-place-waiting")]
        public object GetPlaceWaiting(Guid idUser)
        {
            res = _serviceRes.GetsWaitingHPlace(idUser);
            return Ok(res);
        }

        [HttpGet()]
        [AllowAnonymous]
        [Route("gets-place")]
        public object GetPlace(bool isDelete)
        {
            res = _serviceRes.GetsPlace(isDelete);
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
        [HttpPost]
        [Authorize]
        [Route("update-place")]
        public object UpdatePlace([FromBody] JObject frmData)
        {
            message = null;
            var result = _serviceRes.CheckBeforSave(frmData, ref message, Travel.Shared.Ultilities.Enums.TypeService.Place, true);
            if (message == null)
            {
                var updateObj = JsonSerializer.Deserialize<UpdatePlaceViewModel>(result);
                res = _serviceRes.UpdatePlace(updateObj);
            }
            return Ok(res);
        }
        [HttpGet]
        [Authorize]
        [Route("approve-place")]
        public object ApprovePlace(Guid idPlace)
        {
            res = _serviceRes.ApprovePlace(idPlace);
            return Ok(res);
        }

        [HttpGet]
        [Authorize]
        [Route("refuse-place")]
        public object RefusePlace(Guid idPlace)
        {
            res = _serviceRes.RefusedPlace(idPlace);
            return Ok(res);
        }
        [HttpGet]
        [Authorize]
        [Route("delete-place")]
        public object DeletePlace(Guid idPlace, Guid idUser)
        {
            res = _serviceRes.DeletePlace(idPlace, idUser);
            return Ok(res); 
        }

        [HttpGet]
        [Authorize]
        [Route("restore-place")]
        public object RestorePlace(Guid idPlace, Guid idUser)
        {
            res = _serviceRes.RestorePlace(idPlace, idUser);
            return Ok(res);
        }
        #endregion

    }
}
