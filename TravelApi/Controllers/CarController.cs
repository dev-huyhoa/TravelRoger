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
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel;
using TravelApi.Hubs;

namespace TravelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private ICars car;
        private Notification message;
        private Response res;
        private IHubContext<TravelHub, ITravelHub> _messageHub;
        public CarController(ICars _car, IHubContext<TravelHub, ITravelHub> messageHub)
        {
            car = _car;
            res = new Response();
            _messageHub = messageHub;
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
        [Route("statistic-car")]
        public object StatisticCar()
        {
            res = car.StatisticCar();
            return Ok(res);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("create-car")]
        public object Create([FromBody] JObject frmData)
        {

            message = null;
            var result = car.CheckBeforeSave(frmData, ref message, false);
            if (message == null)
            {
                var createObj = JsonSerializer.Deserialize<CreateCarViewModel>(result);
                res = car.Create(createObj);
                _messageHub.Clients.All.Init();
            }
            else
            {
                res.Notification = message;
            }

            return Ok(res);
        }
    }
}
