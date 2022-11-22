using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Context.Models.Notification;
using Travel.Context.Models.Travel;
using Travel.Data.Interfaces;
using Travel.Shared.Ultilities;
using Travel.Shared.ViewModels;

namespace Travel.Data.Repositories
{
    public class StatisticRes : IStatistic
    {
        private readonly TravelContext _db;
        private readonly NotificationContext _dbNotyf;
        public StatisticRes(TravelContext db, NotificationContext dbNotyf)
        {
            _db = db;
            _dbNotyf = dbNotyf;
        }
        private void UpdateDatabase<T>(T input)
        {
            _dbNotyf.Entry(input).State = EntityState.Modified;
        }

        private void CreateDatabase<T>(T input)
        {
            _dbNotyf.Entry(input).State = EntityState.Added;
        }
        private async Task SaveChangeAsync()
        {
            await _dbNotyf.SaveChangesAsync();
        }
        public async Task<bool> SaveReportTourBookingEveryDay(DateTime dateInput)
        {
            var dateTimeYesterday = DateTime.Now.AddDays(-1);
            var day = dateTimeYesterday.Day;
            var month = dateTimeYesterday.Month;
            var year = dateTimeYesterday.Year;
            var input = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(dateInput);

            var yesterday = DateTime.Parse($"{year}/{month}/{day}");
            var unixEndOfYesterday = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(yesterday.AddDays(1).AddMinutes(-1));
            var unixYesterday = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(yesterday);
            using var transaction = _dbNotyf.Database.BeginTransaction();
            try
            {
                await transaction.CreateSavepointAsync("BeforeSave");
                if (input == unixYesterday)
                {
                    if (!await CheckReportByDateIsExist(unixYesterday))
                    {
                        var listTourBookingFinished =await (from tbk in _db.TourBookings.AsNoTracking()
                                                       join s in _db.Schedules.AsNoTracking()
                                                       on tbk.ScheduleId equals s.IdSchedule
                                                       where tbk.Status == (int)Enums.StatusBooking.Finished
                                                       && (s.ReturnDate >= unixYesterday && s.ReturnDate <= unixEndOfYesterday)
                                                       select tbk).ToListAsync() ;

                        var listGroupingTourbooking = listTourBookingFinished.GroupBy(x => x.ScheduleId);
                        foreach (var item in listGroupingTourbooking)
                        {
                            var schedule = await (from s in _db.Schedules.AsNoTracking()
                                                  join t in _db.Tour.AsNoTracking()
                                                  on s.TourId equals t.IdTour
                                                  where s.IdSchedule == item.Key
                                                  select new { t.NameTour, t.IdTour }).FirstOrDefaultAsync();
                            var costTour = await (from s in _db.Schedules.AsNoTracking()
                                                  where s.IdSchedule == item.Key
                                                  select s.TotalCostTour).FirstOrDefaultAsync();
                            long sumCostTour = (int)costTour * item.Count();
                            var sumNormalPrice = (long)item.Sum(x => x.TotalPrice);
                            var sumNormalPricePromotion = (long)item.Sum(x => x.TotalPricePromotion);
                            ReportTourBooking obj = new ReportTourBooking
                            {
                                DateSave = unixYesterday,
                                IdReportTourBooking = Guid.NewGuid(),
                                IdTour = schedule.IdTour,
                                NameTour = schedule.NameTour,
                                QuantityBooked = item.Count(),
                                TotalRevenue = (sumNormalPrice + sumNormalPricePromotion),
                                TotalCost = sumCostTour
                            };
                            CreateDatabase(obj);


                        }

                        await SaveChangeAsync();
                    }

                }
                transaction.Commit();
                transaction.Dispose();
                return true;
            }
            catch (Exception e)
            {
                transaction.RollbackToSavepoint("BeforeSave");
                return false;
            }

        }
        private async Task<bool> CheckReportByDateIsExist(long dateSave)
        {
            var obj = await (from x in _dbNotyf.ReportTourBooking.AsNoTracking()
                             where x.DateSave == dateSave
                             select x).FirstOrDefaultAsync();
            if (obj != null)
            {
                return true;
            }
            return false;
        }
        public Response StatisticTourBookingFromDateToDate(long fromDate, long toDate)
        {
            try
            {
                var lsReportTourBooking = (from x in _dbNotyf.ReportTourBooking.AsNoTracking()
                                           where x.DateSave >= fromDate
                                           && x.DateSave <= toDate
                                           select x).ToList();
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), lsReportTourBooking);
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }

     
    }
}
