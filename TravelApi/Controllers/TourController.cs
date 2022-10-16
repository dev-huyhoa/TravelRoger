using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public object Create([FromBody] JObject frmData)
        {
            message = null;
            var result = _tourRes.CheckBeforSave(frmData, ref message,false);
            if (message == null)
            {
                var createObj = JsonSerializer.Deserialize<CreateTourViewModel>(result);
                res = _tourRes.Create(createObj);
            }
            return Ok(res);
        }
        // GET api/<TourController>/5
        [HttpGet]
        [AllowAnonymous]
        [Route("gets-tour")]
        public object Get()
        {
            res = _tourRes.Get();
            return Ok(res);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("gets-tour-waiting")]
        public object GetWaiting()
        {
            res = _tourRes.GetWaiting();
            return Ok(res);
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
