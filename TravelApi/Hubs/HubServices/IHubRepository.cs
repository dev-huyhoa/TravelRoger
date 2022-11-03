using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelApi.Hubs.HubServices
{
    public interface IHubRepository
    {
        Task Send();
    }
}
