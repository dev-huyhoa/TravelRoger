using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using Travel.Data.Interfaces;
using Travel.Data.Repositories;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel;

namespace TravelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimelineController : ControllerBase
    {
        private readonly ITimeLine _timelineRes;
        private Notification message;
        private Response res;
        
        public TimelineController(ITimeLine timelineRes)
        {
            _timelineRes = timelineRes;
            res = new Response();
        }

        [HttpPost]
        [Authorize]
        [Route("create-timeline")]
        public object Create([FromBody] JObject frmData)
        {
            message = null;
            var result = _timelineRes.CheckBeforSave(frmData, ref message, false);
            if (message == null)
            {
                var createObj = JsonSerializer.Deserialize<CreateTimeLineViewModel>(result);
                res = _timelineRes.Create(createObj);
            }
            return Ok(res);
        }

        [HttpGet]
        [Authorize]
        [Route("get-timeline")]
        public object GetTimeline()
        {
            res = _timelineRes.Get();
            return Ok(res);
        }

        [HttpGet]
        [Authorize]
        [Route("get-timeline-idSchedule")]
        public object GetGetCostByIdTourDetail(string idSchedule)
        {
            res = _timelineRes.GetTimelineByIdSchedule(idSchedule);
            return Ok(res);
        }
    }
}
