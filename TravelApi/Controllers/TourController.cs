﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travel.Context.Models;
using Travel.Data.Interfaces;
using Travel.Shared.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TravelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TourController : ControllerBase
    {
        private readonly ITourRes _tourRes;
        private Notification message;
        private Response res;
        public TourController(ITourRes tourRes)
        {
            _tourRes = tourRes;
            res = new Response();
        }
        [HttpPost]
        [Authorize]
        [Route("create-tour")]
        public object Create([FromBody] JObject frmData)
        {
            message = new();
            var result = _tourRes.CheckBeforSave(frmData, ref message);
            if (message != null)
            {
                res = _tourRes.Create(result);
            }
            return Ok(res);
        }

        // GET api/<TourController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TourController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TourController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TourController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
