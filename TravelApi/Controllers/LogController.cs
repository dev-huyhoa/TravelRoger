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
    public class LogController : Controller
    {
        private ILog _log;
        private Notification message;
        private Response res;
        public LogController(ILog log)
        {
            _log = log;
            res = new Response();

        }
        [HttpPost]
        [Authorize]
        [Route("search-log-by-type")]
        public async Task<object> Gets(JObject frmData)
        {
           res =  await _log.SearchLogByType(frmData);
            return Ok(res);
        }
    }
}
