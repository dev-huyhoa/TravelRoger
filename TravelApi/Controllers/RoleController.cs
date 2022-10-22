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
    public class RoleController : Controller
    {
        private IRole role;
        private Notification message;
        private Response res;
        private IHubContext<TravelHub, ITravelHub> _messageHub;
        public RoleController(IRole _role, IHubContext<TravelHub, ITravelHub> messageHub)
        {
            _messageHub = messageHub;
            role = _role;
            res = new Response();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [Route("gets-role")]
        public object GetsRole(bool isDelete)
        {
            res = role.GetsRole(isDelete);
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
        [AllowAnonymous]
        [Route("create-role")]
        public object CreateRole([FromBody] JObject frmData)
        {
            message = null;
            var result = role.CheckBeforSave(frmData, ref message, false);
            if (message == null)
            {
                var createObj = JsonSerializer.Deserialize<CreateRoleViewModel>(result);
                res = role.CreateRole(createObj);
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
        [Route("update-role")]
        public object UpdateRole([FromBody] JObject frmData)
        {

            message = null;
            var result = role.CheckBeforSave(frmData, ref message, false);
            if (message == null)
            {
                var updateObj = JsonSerializer.Deserialize<UpdateRoleViewModel>(result);
                res = role.UpdateRole(updateObj);
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
        [Route("restore-role")]
        public object RestoreRole(int idRole)
        {
            res = role.RestoreRole(idRole);
            _messageHub.Clients.All.Init();
            return Ok(res);
        }
        [HttpGet("{id}")]
        [Authorize]
        [Route("delete-role")]
        public object DeleteRole(int idRole)
        {
            res = role.DeleteRole(idRole);
            _messageHub.Clients.All.Init();
            return Ok(res);
        }
    }
}
