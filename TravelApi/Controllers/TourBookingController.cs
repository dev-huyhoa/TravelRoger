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
using Travel.Shared.ViewModels.Travel;
using Travel.Shared.ViewModels.Travel.TourBookingVM;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace TravelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourBookingController : Controller
    {
        private ITourBooking _tourbooking;
        private Notification message;
        private Response res;
        public TourBookingController(ITourBooking tourbooking)
        {
            _tourbooking = tourbooking;
            res = new Response();
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("get-tourbooking")]
        public object getTourBooking()
        {
            res = _tourbooking.Gets();
            return Ok(res);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("create-tourbooking")]
        public object Create([FromBody] JObject frmData)
        {

            message = null;
            var result = _tourbooking.CheckBeforSave(frmData, ref message, false);
            if (message == null)
            {
                var createObj = JsonSerializer.Deserialize<CreateTourBookingViewModel>(result);
                res = _tourbooking.Create(createObj);
            }
            return Ok(res);
        }
    }
}
