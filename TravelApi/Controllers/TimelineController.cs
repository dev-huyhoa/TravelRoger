using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text.Json;
using Travel.Context.Models;
using Travel.Data.Interfaces;
using Travel.Data.Repositories;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel;
using static Travel.Shared.ViewModels.Travel.CreateTimeLineViewModel;

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
        public object Create(ICollection<CreateTimeLineViewModel> timelinee)
        {
            message = null;
            //var result = _timelineRes.CheckBeforSave(frmData, ref message, false);
            if (message == null)
            {
                var createObj = timelinee;
                res = _timelineRes.Create(createObj);
            }
            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        [Route("update-timeline")]
        public object update(ICollection<UpdateTimeLineViewModel> timelinee)
        {
            message = null;
            //var result = _timelineRes.CheckBeforSave(frmData, ref message, false);
            if (message == null)
            {
                var createObj = timelinee;
                res = _timelineRes.Update(createObj);
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
