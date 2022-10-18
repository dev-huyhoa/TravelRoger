using Microsoft.AspNetCore.Authorization;
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
using Travel.Context.Models.Travel;
using Travel.Data.Interfaces;
using Travel.Shared.Ultilities;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel;
using TravelApi.Hubs;

namespace TravelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployee employee;
        private Notification message;
        private Response res;
        private IHubContext<TravelHub, ITravelHub> _messageHub;
        public EmployeeController(IEmployee _employee,
                 IHubContext<TravelHub, ITravelHub> messageHub)
        {
            employee = _employee;
            _messageHub = messageHub;
            res = new Response();
        }

        [HttpGet("{id}")]
        [Authorize]
        [Route("gets-employee")]
        public object GetsEmployee(bool isDelete)
        {
            res =  employee.GetsEmployee(isDelete);
            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        [Route("search-employee")]
        public object SearchEmployee([FromBody] JObject frmData)
        {
            res = employee.SearchEmployee(frmData);
            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        [Route("create-employee")]
        public object CreateEmployee(IFormCollection frmdata, IFormFile file)
        {
            message = null;
            var result = employee.CheckBeforeSave(frmdata, file, ref message, false);
            if (message == null)
            {
                var createObj = JsonSerializer.Deserialize<CreateEmployeeViewModel>(result);
                res = employee.CreateEmployee(createObj);
                _messageHub.Clients.All.Init();
            }
            else
            {
                res.Notification = message;
            }
            return Ok(res);
        }



        [HttpPost]
        [Authorize] 
        [Route("update-employee")]
        public object UpdateEmployee(IFormCollection frmdata, IFormFile file)
        {
            message = null;
            var result = employee.CheckBeforeSave(frmdata, file, ref message, true);
            if (message == null)
            {
                var updateObj = JsonSerializer.Deserialize<UpdateEmployeeViewModel>(result);
                res = employee.UpdateEmployee(updateObj);
                _messageHub.Clients.All.Init();
            }
            else
            {
                res.Notification = message;
            }
            return Ok(res);
        }
        [HttpGet("{id}")]
        [Authorize]
        [Route("delete-employee")]
        public object DeleteEmployee(Guid idEmployee)
        {

            res = employee.DeleteEmployee(idEmployee);
            _messageHub.Clients.All.Init();
            return Ok(res);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Route("restore-employee")]
        public object RestoreEmployee(Guid idEmployee)
        {
            res = employee.RestoreEmployee(idEmployee);
            _messageHub.Clients.All.Init();
            return Ok(res);
        }
    }
}
