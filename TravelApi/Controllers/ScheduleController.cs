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
using Travel.Data.Repositories;
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

        public ScheduleController(ISchedule schedule)
        {
            _schedule = schedule;
            res = new Response();

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

        [HttpPost]
        [Authorize]
        [Route("update-schedule")]
        public object Update([FromBody] JObject frmData)
        {
            message = null;
            var result = _schedule.CheckBeforSave(frmData, ref message, true);
            if (message == null)
            {
                var updateObj = JsonSerializer.Deserialize<UpdateScheduleViewModel>(result);
                res = _schedule.Update(updateObj);
            }
            return Ok(res);
        }


        [HttpGet]
        [Authorize]
        [Route("delete-schedule")]
        public object DeleteTour(string idSchedule, Guid idUser)
        {
            res = _schedule.Delete(idSchedule, idUser);
            return Ok(res);
        }
        [HttpGet]
        [Authorize]
        [Route("restore-schedule")]
        public object RestoreSchedule(string idSchedule, Guid idUser)
        {
            res = _schedule.RestoreShedule(idSchedule, idUser);
            return Ok(res);
        }

        [HttpGet]
        [Authorize]
        [Route("approve-schedule")]
        public object ApproveSchedule(string idSchedule)
        {
            res = _schedule.Approve(idSchedule);
            return Ok(res);
        }
        [HttpGet]
        [Authorize]
        [Route("refused-schedule")]
        public object RefusedSchedule(string idSchedule)
        {
            res = _schedule.Refused(idSchedule);
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
        public object GetsSchedulebyIdTour(string idtour, bool isDelete)
        {
            res = _schedule.GetsSchedulebyIdTour(idtour, isDelete);
            return Ok(res);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("gets-schedule-idtour-waiting")]
        public object GetsSchedulebyIdTourWaiting(string idtour, Guid idUser)
        {
            res = _schedule.GetSchedulebyIdTourWaiting(idtour, idUser);
            return Ok(res);
        }



        [HttpGet]
        [AllowAnonymous]
        [Route("cus-search-schedule")]
        public async Task<object> SearchSchedule(string from = null, string to = null,DateTime? departureDate = null, DateTime? returnDate = null)
        {
            res = await _schedule.SearchTour(from,to,departureDate,returnDate);
            return Ok(res);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("cus-gets-schedule-idtour")]
        public object CusGetsSchedulebyIdTour(string idtour)
        {
            res = _schedule.CusGetsSchedulebyIdTour(idtour);
            return Ok(res);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("cus-gets-schedule")]
        public async Task<object> GetsSchedule()
        {
            res = await _schedule.GetsSchedule();
            return Ok(res);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("gets-schedule-promotion")]
        public async Task<object> GetsSchedulePromotion()
        {
            res = await _schedule.GetsSchedulePromotion();
            return Ok(res);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("gets-schedule-flash-sale")]
        public async Task<object> GetsScheduleFlashSale()
        {
            res = await _schedule.GetsScheduleFlashSale();
            return Ok(res);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("gets-schedule-relate")]
        public async Task<object> GetsScheduleRelate(string idSchedule)
        {
            res = await _schedule.GetsRelatedSchedule(idSchedule);
            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        [Route("search-schedule")]
        public object SearchSchedule([FromBody] JObject frmData, string idTour)
        {
            res = _schedule.SearchSchedule(frmData, idTour);
            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        [Route("search-schedule-waiting")]
        public object SearchScheduleWaiting([FromBody] JObject frmData, string idTour)
        {
            res = _schedule.SearchScheduleWaiting(frmData, idTour);
            return Ok(res);
        }
    }
}
