using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Text.Json;
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
        public CustomerController(ICustomer _customer)
        {
            customer = _customer;
            res = new Response();
        }

        [HttpPost]
        [Authorize]
        [Route("create-customer")]
        public object Create(IFormCollection frmdata, IFormFile file)
        {
            message = null;
            var result = customer.CheckBeforeSave(frmdata, file, ref message, false);
            if (message == null)
            {
                var createObj = JsonSerializer.Deserialize<CreateCustomerViewModel>(result);
                res = customer.Create(createObj);
            }
            else
            {
                res.Notification = message;
            }
            return Ok(res);
        }

        [HttpPost]
        [Authorize]
        [Route("gets-customer")]
        public object GetsEmployee()
        {
            res = customer.Gets();
            return Ok(res);
        }
    }
}
