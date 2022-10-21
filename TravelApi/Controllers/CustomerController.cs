﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
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
        [AllowAnonymous]
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

        [HttpGet]
        [Authorize]
        [Route("gets-customer")]
        public object GetCustomer()
        {
            res = customer.Gets();
            return Ok(res);
        }
        [HttpGet]
        [Authorize]
        [Route("gets-history-booking-bycustomer")]
        public object GetHistoryByIdCustomer(string idCustomer)
        {
            res = customer.GetsHistory(Guid.Parse(idCustomer));
            return Ok(res);
        }
    }
}
