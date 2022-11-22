using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Shared.ViewModels;

namespace Travel.Data.Interfaces
{
    public interface IStatistic
    {
        Response StatisticTourBookingFromDateToDate(long fromDate, long toDate);
        Task<bool> SaveReportTourBooking();
    }
}
