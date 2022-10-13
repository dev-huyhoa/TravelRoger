using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travel.Data.Interfaces;
using Travel.Shared.ViewModels;

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
        public ScheduleController(ISchedule schedule)
        {
            _schedule = schedule;
            res = new Response();
        }

        [HttpPost]
        [Authorize]
        [Route("create-tour")]
        public object Create([FromBody] JObject frmData)
        {
            message = null;
            var result = _schedule.CheckBeforSave(frmData, ref message);
            if (message == null)
            {
                res = _schedule.Create(result);
            }
            return Ok(res);
        }
        // GET api/<ScheduleController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
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

        // DELETE api/<ScheduleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
