using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TravelApi.Helpers;

namespace TravelApi.Hubs.HubServices
{
    [Authorize]
    public class HubRepository : IHubRepository
    {
        IHubContext<TravelHub> _hubContext;
        public HubRepository(IHubContext<TravelHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task Send(string idusser)
        {
            //await _hubContext.Clients.User("4C4683A0-7D4A-4499-8F5B-0DA0ED144398").SendAsync("Init");
            await _hubContext.Clients.All.SendAsync("Init");
        }
    }
}
