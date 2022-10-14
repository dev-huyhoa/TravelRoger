using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Travel.Data.Interfaces;
using Travel.Shared.ViewModels;
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
        // GET: api/<CostTourController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CostTourController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }


        [HttpPost]
        [Authorize]
        [Route("create-tour")]
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

        // PUT api/<CostTourController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CostTourController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
