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
    public class RoleController : Controller
    {
        private IRole role;
        private Notification message;
        private Response res;
        public RoleController(IRole _role)
        {
            role = _role;
            res = new Response();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("gets-role")]
        public object GetsRole()
        {
            res = role.GetsRole();
            return Ok(res);
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("create-role")]
        public object CreateRole([FromBody] JObject frmData)
        {

            var result = role.CheckBeforSave(frmData, ref message);
            if (message == null)
            {
              res = role.CreateRole(result);
            }
            
            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        [Route("update-role")]
        public object UpdateRole([FromBody] JObject frmData)
        {

            var result = role.CheckBeforSave(frmData, ref message);
            if (message == null)
            {
                res = role.UpdateRole(result);
            }

            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        [Route("restore-role")]
        public object RestoreRole([FromBody] JObject frmData)
        {

            var result = role.CheckBeforSave(frmData, ref message);
            if (message == null)
            {
                res = role.RestoreRole(result);
            }
            return Ok(res);
        }
        [HttpPost]
        [Authorize]
        [Route("search-role")]
        public object SearchRole([FromBody] JObject frmData)
        {
            res = role.SearchRole(frmData);
            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        [Route("delete-role")]
        public object DeleteRole([FromBody] JObject frmData)
        {
            var result = role.CheckBeforSave(frmData, ref message);
            if (message == null)
            {
                res = role.DeleteRole(result);
            }
            return Ok(res);
        }
    }
}
