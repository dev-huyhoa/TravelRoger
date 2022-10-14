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

namespace TravelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployee employee;
        private Notification message;
        private Response res;
        public EmployeeController(IEmployee _employee)
        {
            employee = _employee;
            res = new Response();
        }

        [HttpPost]
        [Authorize]
        [Route("gets-employee")]
        public object GetsEmployee([FromBody] JObject frmData)
        {
            res =  employee.GetsEmployee(frmData);
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
        [HttpPost]
        [Authorize]
        [Route("delete-employee")]
        public object DeleteEmployee([FromBody] JObject frmData)
        {

            res = employee.DeleteEmployee(frmData);
            return Ok(res);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("restore-employess")]
        public object RestoreEmployee([FromBody] JObject frmData)
        {
            res = employee.RestoreEmployee(frmData);
            return Ok(res);
        }
    }
}
