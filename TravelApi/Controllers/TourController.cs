﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Travel.Context.Models;
using Travel.Data.Interfaces;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel.TourVM;
using TravelApi.Hubs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourController : ControllerBase
    {
        private readonly ITour _tourRes;
        private Notification message;
        private Response res;


        public TourController(ITour tourRes)
        {
            _tourRes = tourRes;
            res = new Response();
        }

        [HttpPost]
        [Authorize]
        [Route("create-tour")]
        public object Create(IFormCollection frmdata, IFormFile file)
        {
            message = null;
            var result = _tourRes.CheckBeforSave(frmdata, file, ref message,false);
            if (message == null)
            {
                var createObj = JsonSerializer.Deserialize<CreateTourViewModel>(result);
                res = _tourRes.Create(createObj);
            }
            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        [Route("update-tour")]
        public object Update(IFormCollection frmdata, IFormFile file)
        {
            message = null;
            var result = _tourRes.CheckBeforSave(frmdata, file, ref message, true);
            if (message == null)
            {
                var createObj = JsonSerializer.Deserialize<UpdateTourViewModel>(result);
                res = _tourRes.Update(createObj);
            }
            return Ok(res);
        }

        // GET api/<TourController>/5
        [HttpGet]
        [AllowAnonymous]
        [Route("gets-tour")]
        public object Get(bool isDelete)
        {
            res = _tourRes.Get(isDelete);
            return Ok(res);
        }
        [HttpGet]
        [Authorize]
        [Route("gets-tour-waiting")]
        public object GetWaiting( Guid idUser)
        {
            res = _tourRes.GetWaiting(idUser);
            return Ok(res);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("get-tour")]
        public object GetTour(string idTour)
        {
            res = _tourRes.GetTour(idTour);
            return Ok(res);
        }



        // POST api/<TourController>
        [HttpGet]
        [Authorize]
        [Route("approve-tour")]
        public object Approve(string idTour)
        {
            res = _tourRes.Approve(idTour);
            return Ok(res);
        }

        [HttpGet]
        [Authorize]
        [Route("refused-tour")]
        public object Refused(string idTour)
        {
            res = _tourRes.Refused(idTour);
            return Ok(res);
        }

        [HttpGet]
        [Authorize]
        [Route("delete-tour")]
        public object DeleteTour(string idTour, Guid idUser)
        {
            res = _tourRes.Delete(idTour, idUser);
            return Ok(res);
        }


        [HttpGet]
        [Authorize]
        [Route("restore-tour")]
        public object RestoreTour(string idTour,Guid idUser)
        {
            res = _tourRes.RestoreTour(idTour, idUser);
            return Ok(res);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("get-tour-with-schedule")]
        public async Task<object> GetTourWithSchedule()
        {
            res = await _tourRes.GetsTourWithSchedule();
            return Ok(res);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("get-tour-by-id")]
        public async Task<object> GetTourById(string idTour)
        {
            res = await _tourRes.GetTourById(idTour);
            return Ok(res);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("gets-tour-by-rating")]
        public async Task<object> GetsTourByRating()
        {
            res = await _tourRes.GetsTourByRating();
            return Ok(res);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("search-complete")]
        public async Task<object> SearchComplete(string key)
        {
            res = await _tourRes.SearchAutoComplete(key);
            return Ok(res);
        }

        [HttpGet]
        [Authorize]
        [Route("update-rating-tour")]
        public object UpdateRatingTour(int rating, string idTour)
        {
            res = _tourRes.UpdateRating(rating, idTour);
            return Ok(res);
        }
      
    }
}
