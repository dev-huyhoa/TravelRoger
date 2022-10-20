﻿using Microsoft.AspNetCore.Authorization;
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
using Travel.Shared.ViewModels.Travel.CostTourVM;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostTourController : ControllerBase
    {
        private readonly ICostTour _costTourRes;
        private Notification message;
        private Response res;
        public CostTourController(ICostTour costTourRes)
        {
            _costTourRes = costTourRes;
            res = new Response();
        }
        
        [HttpGet()]
        [Authorize]
        [Route("gets-cost-tour")]
        public object Get()
        {
            res = _costTourRes.Get();
            return Ok(res);
        }
        [HttpGet("{id}")]
        [Authorize]
        [Route("gets-cost-tour-id-tour-detail")]
        public object GetGetCostByIdTourDetail(string idTourDetail)
        {
            res = _costTourRes.GetCostByIdTourDetail(idTourDetail);
            return Ok(res);
        }


        [HttpPost]
        [Authorize]
        [Route("create-cost-tour")]
        public object Create([FromBody] JObject frmData)
        {
            message = null;
            var result = _costTourRes.CheckBeforSave(frmData, ref message, false);
            if (message == null)
            {
                var createObj = JsonSerializer.Deserialize<CreateCostViewModel>(result);
                res = _costTourRes.Create(createObj);
            }
            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        [Route("update-cost-tour")]
        public object Update([FromBody] JObject frmData)
        {
            message = null;
            var result = _costTourRes.CheckBeforSave(frmData, ref message, true);
            if (message == null)
            {
                var updateObj = JsonSerializer.Deserialize<UpdateCostViewModel>(result);
                res = _costTourRes.Update(updateObj);
            }
            else
            {
                res.Notification = message;
            }
            return Ok(res);
        }

   

        // DELETE api/<CostTourController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
