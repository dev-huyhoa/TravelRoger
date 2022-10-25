using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travel.Data.Interfaces;
using Travel.Shared.ViewModels;

namespace TravelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private ICars car;
        private Notification message;
        private Response res;
        public CarController(ICars _car)
        {
            car = _car;
            res = new Response();
        }

        [HttpGet]
        [Authorize]
        [Route("gets-car")]
        public object Gets()
        {
            res = car.Gets();
            return Ok(res);
        }
        [HttpGet]
        [Authorize]
        [Route("get")]
        public object GetCar()
        {
            res = car.GetCarStatus();
            return Ok(res);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("create-car")]
        public object Create([FromBody] JObject frmData)
        {

            var result = car.CheckBeforeSave(frmData, ref message);
            if (message == null)
            {
                res = car.Create(result);
            }

            return Ok(res);
        }
    }
}
