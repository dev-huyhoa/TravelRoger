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
        
        [HttpGet()]
        [AllowAnonymous]
        [Route("gets-schedule")]
        public object Get()
        {
            res = _schedule.Get();
            return Ok(res);
        }

        // POST api/<ScheduleController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ScheduleController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }


        [HttpGet("{id}")]
        [Authorize]
        [Route("delete-schedule")]
        public object DeleteTour(string idSchedule)
        {
            res = _schedule.Delete(idSchedule);
            _messageHub.Clients.All.Init();
            return Ok(res);
        }


        [HttpGet("{id}")]
        [Authorize]
        [Route("restore-schedule")]
        public object RestoreSchedule(string idSchedule)
        {
            res = _schedule.RestoreShedule(idSchedule);
            _messageHub.Clients.All.Init();
            return Ok(res);
        }
    }
}
