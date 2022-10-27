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
using TravelApi.Hubs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly ISchedule _schedule;
        private Notification message;
        private Response res;
        private IHubContext<TravelHub, ITravelHub> _messageHub;

        public ScheduleController(ISchedule schedule, IHubContext<TravelHub, ITravelHub> messageHub)
        {
            _schedule = schedule;
            res = new Response();
            _messageHub = messageHub;

        }

        [HttpPost]
        [Authorize]
        [Route("create-schedule")]
        public object Create([FromBody] JObject frmData)
        {
            message = null;
            var result = _schedule.CheckBeforSave(frmData, ref message, false);
            if (message == null)
            {
                var createObj = JsonSerializer.Deserialize<CreateScheduleViewModel>(result);
                res = _schedule.Create(createObj);
            }
            return Ok(res);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("gets-schedule")]
        public object Gets()
        {
            res = _schedule.Gets();
            return Ok(res);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("get-schedule")]
        public async Task<object> Get(string idSchedule)
        {
            res = await _schedule.Get(idSchedule);
            return Ok(res);
        }
        [HttpGet]
        [Route("update-promotion")]
        public object UpdatePromotion(string idSchedule, int idPromotion)
        {
            res = _schedule.UpdatePromotion(idSchedule, idPromotion);
            return Ok(res);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("gets-schedule-idtour")]
        public object GetsSchedulebyIdTour(string idtour)
        {
            res = _schedule.GetsSchedulebyIdTour(idtour);
            return Ok(res);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("gets-schedule-idtour-waiting")]
        public object GetsSchedulebyIdTourWaiting(string idtour)
        {
            res = _schedule.GetSchedulebyIdTourWaiting(idtour);
            return Ok(res);
        }


        [HttpGet]
        [Authorize]
        [Route("delete-schedule")]
        public object DeleteTour(string idSchedule)
        {
            res = _schedule.Delete(idSchedule);
            _messageHub.Clients.All.Init();
            return Ok(res);
        }
        [HttpGet]
        [Authorize]
        [Route("restore-schedule")]
        public object RestoreSchedule(string idSchedule)
        {
            res = _schedule.RestoreShedule(idSchedule);
            _messageHub.Clients.All.Init();
            return Ok(res);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("cus-search-schedule")]
        public async Task<object> SearchSchedule(string from = null, string to = null,DateTime? departureDate = null, DateTime? returnDate = null)
        {
            res = await _schedule.SearchTour(from,to,departureDate,returnDate);
            await _messageHub.Clients.All.Init();
            return Ok(res);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("cus-gets-schedule-idtour")]
        public object CusGetsSchedulebyIdTour(string idtour)
        {
            res = _schedule.GetsSchedulebyIdTour(idtour);
            return Ok(res);
        }
    }
}
