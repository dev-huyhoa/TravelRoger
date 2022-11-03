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
        public string GetConnectionId() => Context.ConnectionId;

        public async Task Init()
        {
            await Clients.All.SendAsync("Init");
        }
    }
}
