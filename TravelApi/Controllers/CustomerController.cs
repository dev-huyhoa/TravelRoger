using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Travel.Data.Interfaces;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel.CustomerVM;

namespace TravelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomer customer;
        private Notification message;
        private Response res;
        private readonly ILog _log;
        public CustomerController(ICustomer _customer)
        {
            customer = _customer;
            res = new Response();
        }

        [NonAction]
        private Claim GetEmailUserLogin()
        {
            return (User.Identity as ClaimsIdentity).Claims.Where(c => c.Type == ClaimTypes.Email).FirstOrDefault();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("create-customer")]
        public object Create([FromBody] JObject frmdata)
        {
            message = null;
            var result = customer.CheckBeforeSave(frmdata, ref message, false);
            if (message == null)
            {
                var createObj = JsonSerializer.Deserialize<CreateCustomerViewModel>(result);
                var emailUser = GetEmailUserLogin().Value;
                res = customer.Create(createObj, emailUser);

            }
            else
            {
                res.Notification = message;
            }
            return Ok(res);
        }

        [HttpGet]
        [Authorize]
        [Route("list-customer")]
        public object GetCustomer(int pageIndex, int pageSize)
        {
            res = customer.Gets(pageIndex,pageSize);
            return Ok(res);
        }
        [HttpGet]
        [Authorize]
        [Route("list-history-booking-bycustomer")]
        public object GetHistoryByIdCustomer(Guid idCustomer)
        {
            res = customer.GetsHistory(idCustomer);
            return Ok(res);
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("send-otp")]
        public async Task<object> SendOTP(string email)
        {
            res = await customer.SendOTP(email);
            return Ok(res);
        }

        [HttpGet]
        [Authorize]
        [Route("detail-customer")]
        public object GetCustomer(Guid idCustomer)
        {
            res = customer.GetCustomer(idCustomer);
            //_messageHub.Clients.All.Init();
            return Ok(res);
        }

        [HttpPut]
        [Authorize]
        [Route("update-customer")]
        public async Task<object> UpdateCustomer([FromBody] JObject frmdata, Guid idCustomer)
        {
            message = null;
            var result = customer.CheckBeforeSave(frmdata, ref message, true);
            if (message == null)
            {
                var updateObj = JsonSerializer.Deserialize<UpdateCustomerViewModel>(result);
                var emailUser = GetEmailUserLogin().Value;
                
                res = await customer.UpdateCustomer(updateObj, emailUser);
                //_messageHub.Clients.All.Init();
            }
            else
            {
                res.Notification = message;
            }
            return Ok(res);
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("cus-vote-rating")]
        public async Task<object> CustomerVoteRateting(string idTour, int rating)
        {
            res = await customer.CustomerSendRate(idTour, rating);
            return Ok(res);
        }
    }
}
