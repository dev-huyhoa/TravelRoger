using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Travel.Data.Interfaces;
using Travel.Shared.ViewModels;

namespace TravelApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomer customer;
        private Notification message;
        private Response res;
        public CustomerController(ICustomer _customer)
        {
            customer = _customer;
            res = new Response();
        }

        [HttpPost]
        [Authorize]
        [Route("create-customer")]
        public object Create([FromBody] JObject frmData)
        {
            var result = customer.CheckBeforeSave(frmData, ref message);
            if (message == null)
            {
                res = customer.Create(result);
            }
            else
            {
                res.Notification = message;
            }
            return Ok(res);
        }

        [HttpGet]
        [Authorize]
        [Route("gets-customer")]
        public object Gets()
        {
            res = customer.Gets();
            return Ok(res);
        }
    }
}
