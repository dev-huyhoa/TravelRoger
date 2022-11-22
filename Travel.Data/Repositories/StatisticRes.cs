//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Travel.Context.Models.Notification;
//using Travel.Context.Models.Travel;
//using Travel.Data.Interfaces;
//using Travel.Shared.Ultilities;
//using Travel.Shared.ViewModels;

//namespace Travel.Data.Repositories
//{
//   public class StatisticRes : IStatistic
//    {
//        private readonly TravelContext _db;
//        private readonly NotificationContext _dbNotyf;
//        public StatisticRes(TravelContext db , NotificationContext dbNotyf)
//        {
//            _db = db;
//            _dbNotyf = dbNotyf;
//        }

//        public async Task<bool> SaveReportTourBooking(DateTime dateInput)
//        {
//            var dateTimeNow = DateTime.Now;
//            var month = dateTimeNow.Month;
//            var year = dateTimeNow.Year;

//            var input = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(dateInput); 

//            var firstOfMonthDate = DateTime.Parse($"{year}/{month}/01");
//            var lastDayOfMonthDate = firstOfMonthDate.AddMonths(1).AddDays(-1);
//            var unixLastDayOfMonthDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(lastDayOfMonthDate);
//            using var transaction = _dbNotyf.Database.BeginTransaction();
//            try
//            {
//                await transaction.CreateSavepointAsync("BeforeSave");
//                if (input == unixLastDayOfMonthDate)
//                {
//                    if (!await CheckReportByDateIsExist(unixLastDayOfMonthDate))
//                    {
//                        var listGroupingTourbooking = _db.TourBookings
//                            .Where(x => x.Status == (int)Enums.StatusBooking.Finished)
//                            .GroupBy(x => x.ScheduleId);

//                        foreach (var item in listGroupingTourbooking)
//                        {
//                            var schedule = await (from s in _db.Schedules.AsNoTracking()
//                                                  join t in _db.Tour.AsNoTracking()
//                                                  on s.TourId equals t.IdTour
//                                              where s.IdSchedule == item.Key
//                                              select new { t.NameTour,t.IdTour}).FirstOrDefaultAsync();
//                            schedule
//                            var sumNormalPrice = (long)item.Sum(x => x.TotalPrice);
//                            var sumNormalPricePromotion = (long)item.Sum(x => x.TotalPricePromotion);
//                            ReportTourBooking obj = new ReportTourBooking
//                            {
//                                DateSave = unixLastDayOfMonthDate,
//                                IdReportTourBooking = Guid.NewGuid(),
//                                IdTour = schedule.IdTour,
//                                NameTour = schedule.NameTour,
//                                QuantityBooked = item.Count(),
//                                TotalRevenue = (sumNormalPrice + sumNormalPricePromotion),
//                                TotalCost

//                            }

//                        }

//                    }

//                }
//                transaction.Commit();
//                transaction.Dispose();

//            }
//            catch (Exception e)
//            {
//                transaction.RollbackToSavepoint("BeforeSave");
//                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
//                ;
//            }

//        }
//        private async Task<bool> CheckReportByDateIsExist(long dateSave)
//        {
//            var obj = await (from x in _dbNotyf.ReportTourBooking.AsNoTracking()
//                       where x.DateSave == dateSave
//                       select x.IdReportTourBooking).FirstOrDefaultAsync();
//            if (obj != null)
//            {
//                return true;
//            }
//            return false;
//        }
//        public Response StatisticTourBookingFromDateToDate(long fromDate, long toDate)
//        {
//            return null;
//        }

//    }
//}
