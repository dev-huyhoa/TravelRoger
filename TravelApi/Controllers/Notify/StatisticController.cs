﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travel.Data.Interfaces;
using Travel.Shared.ViewModels;

namespace TravelApi.Controllers.Notify
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticController : ControllerBase
    {
        private IStatistic _statistic;
        private Response res;

        public StatisticController(IStatistic statistic)
        {
            _statistic = statistic;
            res = new Response();
        }

        [HttpGet]
        [Authorize]
        [Route("list-statistic-tourbooking-by-date")]
        public object GetStatisticTourbookingFromDateToDate(long fromDate, long toDate)
        {
            res = _statistic.StatisticTourBookingFromDateToDate(fromDate, toDate);
            return Ok(res);
        }
        [HttpPost]
        [Authorize]
        [Route("saving-tourbooking-finished")]
        public async Task<object> SaveReportTourBookingEveryDay(DateTime date)
        {
            return await _statistic.SaveReportTourBookingEveryDay(date);
        }
    }
}
