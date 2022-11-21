using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Context.Models.Travel;
using Travel.Data.Interfaces;
using Travel.Shared.ViewModels;

namespace Travel.Data.Repositories
{
   public class StatisticRes : IStatistic
    {
        private readonly TravelContext _db;
        public StatisticRes(TravelContext db)
        {
            _db = db;
        }

        public Response StatisticTourBookingFromDateToDate(long fromDate, long toDate)
        {
            return null;
        }

    }
}
