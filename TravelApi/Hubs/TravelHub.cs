using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Travel.Context.Models;

namespace TravelApi.Hubs
{
    public class TravelHub : Hub
    {
        public async Task Send()
        {
            var httpContext = Context.GetHttpContext();
            await Clients.All.SendAsync("Init");
        }
    }
}
