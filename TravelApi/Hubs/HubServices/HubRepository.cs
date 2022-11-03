using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelApi.Hubs.HubServices
{
    public class HubRepository : IHubRepository
    {
        IHubContext<TravelHub> _hubContext;
        public HubRepository(IHubContext<TravelHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task Send()
        {
            await _hubContext.Clients.All.SendAsync("Init");
        }
    }
}
