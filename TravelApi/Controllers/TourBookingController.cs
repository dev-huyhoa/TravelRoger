﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly ISchedule _schedule;

        private Notification message;
        private Response res;
        private IHubContext<TravelHub, ITravelHub> _messageHub;
        public TourBookingController(ITourBooking tourbooking,
            ISchedule schedule,
            IHubContext<TravelHub, ITravelHub> messageHub)
        {
            _tourbooking = tourbooking;
            _schedule = schedule;
            res = new Response();
            _messageHub = messageHub;
        }
        [HttpGet]
        [Authorize]
        [Route("do-payment")]
        public object DoPayment(string idTourBooking)
        {
            res = _tourbooking.DoPayment(idTourBooking);
            return Ok(res);
        }
        [HttpGet]
        [Authorize]
        [Route("get-tourbooking-by-date")]
        public object GetTourBookingFromDateToDate(DateTime? fromDateInput, DateTime? toDateInput)
        {
            res = _tourbooking.GetTourBookingFromDateToDate(fromDateInput, toDateInput);
            return Ok(res);
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
                int adult = createObj.BookingDetails.Adult;
                int child = createObj.BookingDetails.Child;
                int baby = createObj.BookingDetails.Baby;
                await _schedule.UpdateCapacity(createObj.ScheduleId, adult, child, baby);
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
        [Route("cancel-booking")]
        public async Task<object> CancelBooking(string idTourBooking)
        {
            res =await _tourbooking.CancelBooking(idTourBooking);
            return Ok(res);
        }
    }
}
