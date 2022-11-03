using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
using TravelApi.Hubs.HubServices;

namespace TravelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployee employee;
        private Notification message;
        private Response res;
        private IHubRepository _hubRepo;

        public EmployeeController(IEmployee _employee, IHubRepository hubRepo)
        {
            employee = _employee;
            _hubRepo = hubRepo;
            res = new Response();
        }

        [HttpGet]
        [Authorize]
        [Route("gets-employee")]
        public object GetsEmployee(bool isDelete)
        {
            res = employee.GetsEmployee(isDelete);
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
                _hubRepo.Send();
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
            }
            else
            {
                res.Notification = message;
            }
            return Ok(res);
        }
        [HttpGet]
        [Authorize]
        [Route("delete-employee")]
        public object DeleteEmployee(Guid idEmployee)
        {
            res = employee.DeleteEmployee(idEmployee);
            return Ok(res);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("restore-employee")]
        public object RestoreEmployee(Guid idEmployee)
        {
            res = employee.RestoreEmployee(idEmployee);
            return Ok(res);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("get-employee")]
        public object GetEmployee(Guid idEmployee)
        {
            res = employee.GetEmployee(idEmployee);
            return Ok(res);
        }


        [HttpGet]
        [AllowAnonymous]
        [Route("statistic-employee")]
        public object StatisticEmployee(bool isDelete , bool isActive)
        {
            res = employee.StatisticEmployee();
            return Ok(res);
        }
    }
}
