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
    public class PaymentController : Controller
    {
        private IPayment pay;
        private Notification message;
        private Response res;
        public PaymentController(IPayment _pay)
        {
            pay = _pay;
            res = new Response();
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("get-payment")]
        public object GetPayment()
        {
            res = pay.Gets();
            return Ok(res);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("create-payment")]
        public object Create([FromBody] JObject frmData)
        {

            var result = pay.CheckBeforSave(frmData, ref message);
            if (message == null)
            {
                res = pay.Create(result);
            }

            return Ok(res);
        }
    }
}
