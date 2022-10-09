﻿using Microsoft.AspNetCore.Authorization;
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
        [Route("view-all")]
        public object ViewAll()
        {
            res = role.ViewAll();
            return Ok(res);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("create")]
        public object Create([FromBody] JObject frmData)
        {

            var result = role.CheckBeforSave(frmData, ref message);
            if (message == null)
            {
              res = role.Create(result);
            }
            
            return Ok(res);
        }
    }
}
