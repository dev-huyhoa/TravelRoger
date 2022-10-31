using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
using TravelApi.Hubs;

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
        private IHubContext<TravelHub, ITravelHub> _messageHub;
        public TourBookingController(ITourBooking tourbooking, IHubContext<TravelHub, ITravelHub> messageHub)
        {
            _tourbooking = tourbooking;
            res = new Response();
            _messageHub = messageHub;
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
        public async Task<object> Create([FromBody] JObject frmData)
        {
            message = null;
            var result = _tourbooking.CheckBeforSave(frmData, ref message, false);
            if (message == null)
            {
                var createObj = JsonSerializer.Deserialize<CreateTourBookingViewModel>(result);
                res =await   _tourbooking.Create(createObj);
                await _messageHub.Clients.All.Init();
            }
            else
            {
                res.Notification = message;
            }
            return Ok(res);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("tour-booking-by-id")]
        public async Task<object> TourBookingById(string idTourBooking)
        {
            res =await _tourbooking.TourBookingById(idTourBooking);
            return Ok(res);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("get-tourbooking-by-date")]
        public object GetTourBookingFromDateToDate(DateTime? fromDateInput, DateTime? toDateInput)
        {
            res = _tourbooking.GetTourBookingFromDateToDate(fromDateInput, toDateInput);
            return Ok(res);
        }
    }
}
