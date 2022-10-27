using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using PrUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Travel.Context.Models;
using Travel.Context.Models.Travel;
using Travel.Data.Interfaces;
using Travel.Shared.Ultilities;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel.TourVM;

namespace Travel.Data.Repositories
{
    public class TourRes : ITour
    {
        private readonly TravelContext _db;
        private Notification message;
        private Response res;
        public TourRes(TravelContext db)
        {
            _db = db;
            message = new Notification();
            res = new Response();
        }
        public string CheckBeforSave(IFormCollection frmdata, IFormFile file, ref Notification _message, bool isUpdate)
        {
            try
            {
                JObject frmData = JObject.Parse(frmdata["data"]);
                if(frmData != null)
                {

                    var tourName = PrCommon.GetString("nameTour", frmData);
                    if (String.IsNullOrEmpty(tourName))
                    {
                    }
                    if (isExistName(tourName))
                    {
                    }
                    var idTour = PrCommon.GetString("idTour", frmData);
                    if (String.IsNullOrEmpty(idTour))
                    {
                        idTour = Ultility.GenerateId(tourName);
                    }

                    var thumbnail = PrCommon.GetString("thumbnail", frmData);
                    if (String.IsNullOrEmpty(thumbnail))
                    {
                    }
                    var fromPlace = PrCommon.GetString("fromPlace", frmData);
                    if (String.IsNullOrEmpty(fromPlace))
                    {

                    }
                    var toPlace = PrCommon.GetString("toPlace", frmData);
                    if (String.IsNullOrEmpty(toPlace))
                    {
                    }
                    var description = PrCommon.GetString("description", frmData);
                    if (String.IsNullOrEmpty(description))
                    {
                    }
                    if (file != null)
                    {
                        thumbnail = Ultility.WriteFile(file, "Tour", idTour, ref _message).FilePath;
                        if (_message != null)
                        {
                            message = _message;
                        }
                    }
                    if (isUpdate)
                    {
                        // map data
                        UpdateTourViewModel objUpdate = new UpdateTourViewModel();
                        objUpdate.NameTour = tourName;
                        objUpdate.Thumbnail = thumbnail;
                        objUpdate.ToPlace = toPlace;
                        // generate ID
                        objUpdate.IdTour = idTour;
                        return JsonSerializer.Serialize(objUpdate);
                    }
                    // map data
                    CreateTourViewModel obj = new CreateTourViewModel();
                    obj.NameTour = tourName;
                    obj.Thumbnail = thumbnail;
                    obj.ToPlace = toPlace;

                    // generate ID
                    obj.IdTour = idTour;

                    return JsonSerializer.Serialize(obj);
                }
                return string.Empty;
            }
            catch (Exception e)
            {
                message.DateTime = DateTime.Now;
                message.Description = e.Message;
                message.Messenge = "Có lỗi xảy ra !";
                message.Type = "Error";
                _message = message;
                return null;
            }
        }
        public Response Create(CreateTourViewModel input)
        {
            try
            {
                Tour tour =
                tour = Mapper.MapCreateTour(input);
                tour.IsDelete = false;
                _db.Tour.Add(tour);
                _db.SaveChanges();
                res.Notification.DateTime = DateTime.Now;
                res.Notification.Messenge = "Thêm thành công !";
                res.Notification.Type = "Success";
                return res;
            }
            catch (Exception e)
            {

                res.Notification.DateTime = DateTime.Now;
                res.Notification.Description = e.Message;
                res.Notification.Messenge = "Có lỗi xảy ra !";
                res.Notification.Type = "Error";
                return res;
            }
        }

        public Response Update(UpdateTourViewModel input)
        {
            try
            {
                Tour tour =
                tour = Mapper.MapUpdateTour(input);
                _db.Tour.Update(tour);
                _db.SaveChanges();
                res.Notification.DateTime = DateTime.Now;
                res.Notification.Messenge = "Sửa thành công !";
                res.Notification.Type = "Success";
                return res;
            }
            catch (Exception e)
            {

                res.Notification.DateTime = DateTime.Now;
                res.Notification.Description = e.Message;
                res.Notification.Messenge = "Có lỗi xảy ra !";
                res.Notification.Type = "Error";
                return res;
            }
        }

        public Response Delete(string idTour)
        {
            try
            {
                var tour = _db.Tour.Find(idTour);
                var unixNow = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);
                if (tour != null)
                {
                    // cách 1
                    var scheduleInTour = (from x in _db.Schedules
                                          where x.TourId == idTour
                                          && x.Isdelete == false
                                          && x.Approve == (int)Enums.ApproveStatus.Approved
                                          && (x.Status == (int)Enums.StatusSchedule.Finished || (x.Status == (int)Enums.StatusSchedule.Free && x.QuantityCustomer == 0))
                                          select x).ToList();
                    //cách 2
                    //var scheduleInTour = (from x in _db.Tourbookings
                    //           where (x.Status >= (int)Enums.StatusBooking.Paying && x.Status <= (int)Enums.StatusBooking.Paid)
                    //           select new
                    //           {
                    //               Schedule = (from s in _db.Schedules
                    //                           where s.TourId == idTour
                    //                           select s).FirstOrDefault()
                    //           }).ToList();

                    if (scheduleInTour.Count  != 0)
                    {
                        tour.IsDelete = true;
                        _db.SaveChanges();

                        res.Notification.DateTime = DateTime.Now;
                        res.Notification.Messenge = "Xóa thành công !";
                        res.Notification.Type = "Success";
                    }
                    res.Notification.DateTime = DateTime.Now;
                    res.Notification.Messenge = "Tour đang có booking!";
                    res.Notification.Type = "Warning";
                }
                else
                {
                    res.Notification.DateTime = DateTime.Now;
                    res.Notification.Messenge = "Không tìm thấy !";
                    res.Notification.Type = "Warning";
                }
                return res;
            }
            catch (Exception e)
            {
                res.Notification.DateTime = DateTime.Now;
                res.Notification.Description = e.Message;
                res.Notification.Messenge = "Có lỗi xảy ra !";
                res.Notification.Type = "Error";
                return res;
            }
        }

        public Response Get()
        {
            try
            {
                var list = (from x in _db.Tour where x.IsDelete == false 
                            where x.ApproveStatus == Convert.ToInt16(Enums.ApproveStatus.Approved) select x).ToList();
                var result = Mapper.MapTour(list);
                if (list.Count()>0)
                {
                    res.Content = result;
                }
                return res;
            }
            catch (Exception e)
            {
                res.Notification.DateTime = DateTime.Now;
                res.Notification.Description = e.Message;
                res.Notification.Messenge = "Có lỗi xảy ra !";
                res.Notification.Type = "Error";
                return res;
            }
        }

        public Response GetTour(string idTour)
        {
            try
            {
                Tour tour = _db.Tour.Find(idTour);
                var result = Mapper.MapTour(tour);
                if (result != null)
                {
                    res.Content = result;
                }
                return res;
            }
            catch (Exception e)
            {
                res.Notification.DateTime = DateTime.Now;
                res.Notification.Description = e.Message;
                res.Notification.Messenge = "Có lỗi xảy ra !";
                res.Notification.Type = "Error";
                return res;
            }
        }

        public Response GetWaiting()
        {
            try
            {
             
                var list = (from x in _db.Tour where(x.IsDelete == false)
                            where(x.ApproveStatus == Convert.ToInt16(Enums.ApproveStatus.Waiting)) select x ).ToList();
                var result = Mapper.MapTour(list);
                if (list.Count() > 0)
                {
                    res.Content = result;
                }
                return res;
            }
            catch(Exception e)
            {
                res.Notification.DateTime = DateTime.Now;
                res.Notification.Description = e.Message;
                res.Notification.Messenge = "Có lỗi xảy ra !";
                res.Notification.Type = "Error";
                return res;
            }
        }

        public Response Approve(JObject frmData)
        {
            try
            {
                var id = PrCommon.GetString("idTour", frmData);
                if (String.IsNullOrEmpty(id))
                {
                }
                var typeApprove = PrCommon.GetString("typeApprove", frmData);

                if (String.IsNullOrEmpty(typeApprove))
                {
                }
                var tour = (from x in _db.Tour where x.IdTour == id select x).First();
                tour.ApproveStatus = Convert.ToInt16(typeApprove);
                _db.SaveChanges();
                res.Content = $"Thao tác {tour.NameTour} thành công";
                return res;
            }
            catch (Exception e)
            {
                res.Notification.DateTime = DateTime.Now;
                res.Notification.Description = e.Message;
                res.Notification.Messenge = "Có lỗi xảy ra !";
                res.Notification.Type = "Error";
                return res;
            }
        }








        public Response RestoreTour(string idTour)
        {
            try
            {
                var tour = _db.Tour.Find(idTour);
                if (tour != null)
                {
                    tour.IsDelete = false;
                    _db.SaveChanges();

                    res.Notification.DateTime = DateTime.Now;
                    res.Notification.Messenge = "Khôi phục thành công !";
                    res.Notification.Type = "Success";
                }
                else
                {
                    res.Notification.DateTime = DateTime.Now;
                    res.Notification.Messenge = "Không tìm thấy !";
                    res.Notification.Type = "Warning";
                }
                return res;
            }
            catch (Exception e)
            {
                res.Notification.DateTime = DateTime.Now;
                res.Notification.Description = e.Message;
                res.Notification.Messenge = "Có lỗi xảy ra !";
                res.Notification.Type = "Error";
                return res;
            }
        }

        private bool isExistName(string input)
        {
            try
            {
                var tour =
                    (from x in _db.Tour
                     where x.NameTour.ToLower() == input.ToLower()
                     select x).FirstOrDefault();
                if (tour != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<Response> GetsTourWithSchedule()
        {
            try
            {
                var dateTimeNow = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);
                var list = await (from x in _db.Tour
                                  where x.IsDelete == false
                                  && x.ApproveStatus == Convert.ToInt16(Enums.ApproveStatus.Approved)
                                  && x.IsActive == true
                                  select new Tour {
                                      Thumbnail = x.Thumbnail,
                                      ToPlace = x.ToPlace,
                                      IdTour = x.IdTour,
                                      NameTour = x.NameTour,
                                      Alias = x.Alias,
                                      Rating = x.Rating,
                                      QuantityBooked = x.QuantityBooked,
                                      Schedules = (from s in _db.Schedules
                                                   where s.TourId == x.IdTour
                                                   && s.EndDate < dateTimeNow
                                                   && s.Status == (int)Enums.StatusSchedule.Free
                                                   select new Schedule {
                                                       DepartureDate = s.DepartureDate,
                                                       ReturnDate = s.ReturnDate,
                                                       DeparturePlace = s.DeparturePlace,
                                                       Description = s.Description,
                                                       BeginDate = s.BeginDate,
                                                       EndDate = s.EndDate,
                                                       MetaDesc = s.MetaDesc,
                                                       MetaKey = s.MetaKey,
                                                       AdditionalPrice = s.AdditionalPrice,
                                                       AdditionalPriceHoliday = s.AdditionalPriceHoliday,
                                                       Alias = s.Alias,
                                                       Status = s.Status,
                                                       Approve = s.Approve,
                                                       FinalPrice = s.FinalPrice,
                                                       FinalPriceHoliday = s.FinalPriceHoliday,
                                                       IdSchedule = s.IdSchedule,
                                                       IsHoliday = s.IsHoliday,
                                                       MinCapacity = s.MinCapacity,
                                                       MaxCapacity = s.MaxCapacity,
                                                       PriceAdult = s.PriceAdult,
                                                       PriceAdultHoliday = s.PriceAdultHoliday,
                                                       PriceChild = s.PriceChild,
                                                       PriceBabyHoliday = s.PriceChildHoliday,
                                                       PriceBaby = s.PriceBaby,
                                                       PriceChildHoliday = s.PriceChildHoliday,
                                                       QuantityAdult = s.QuantityAdult,
                                                       QuantityBaby = s.QuantityBaby,
                                                       QuantityChild = s.QuantityChild,
                                                       QuantityCustomer = s.QuantityCustomer,
                                                       TotalCostTourNotService = s.TotalCostTourNotService,
                                                       Vat = s.Vat,
                                                       Profit = s.Profit,
                                                       TimePromotion = s.TimePromotion,
                                                       Promotions = (from pro in _db.Promotions
                                                                     where pro.IdPromotion == s.PromotionId
                                                                     select pro).FirstOrDefault(),
                                                       Car = (from car in _db.Cars
                                                              where car.IdCar == s.CarId
                                                              select car).First(),
                                                       Timelines = (from timeline in _db.Timelines
                                                                    where timeline.IdSchedule == s.IdSchedule
                                                                    && timeline.IsDelete == false
                                                                    select new Timeline { 
                                                                    Description = timeline.Description,
                                                                    FromTime = timeline.FromTime,
                                                                    ToTime = timeline.ToTime,
                                                                    }).ToList(),
                                                       CostTour = (from c in _db.CostTours
                                                                   where c.IdSchedule == s.IdSchedule
                                                                   select c).First(),
                                                       Employee = (from e in _db.Employees
                                                                   where e.IdEmployee ==  s.EmployeeId
                                                                   select e).First()
                                                   }).ToList()
                                  }).ToListAsync();
                if (list.Count() > 0)
                {
                    res.Content = list;
                }
                return res;
            }
            catch (Exception e)
            {
                res.Notification.DateTime = DateTime.Now;
                res.Notification.Description = e.Message;
                res.Notification.Messenge = "Có lỗi xảy ra !";
                res.Notification.Type = "Error";
                return res;
            }
        }

        public async Task<Response> GetTourById(string idTour)
        {
            try
            {
                var dateTimeNow = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);
                var list = await(from x in _db.Tour
                                 where x.IsDelete == false
                                 && x.IdTour == idTour
                                 && x.ApproveStatus == Convert.ToInt16(Enums.ApproveStatus.Approved)
                                 && x.IsActive == true
                                 select new Tour
                                 {
                                     Thumbnail = x.Thumbnail,
                                     ToPlace = x.ToPlace,
                                     IdTour = x.IdTour,
                                     NameTour = x.NameTour,
                                     Alias = x.Alias,
                                     Rating = x.Rating,
                                     QuantityBooked = x.QuantityBooked,
                                     Schedules = (from s in _db.Schedules
                                                  where s.TourId == x.IdTour
                                                  && s.EndDate < dateTimeNow
                                                  && s.Status == (int)Enums.StatusSchedule.Free
                                                  select new Schedule
                                                  {
                                                      DepartureDate = s.DepartureDate,
                                                      ReturnDate = s.ReturnDate,
                                                      DeparturePlace = s.DeparturePlace,
                                                      Description = s.Description,
                                                      BeginDate = s.BeginDate,
                                                      EndDate = s.EndDate,
                                                      MetaDesc = s.MetaDesc,
                                                      MetaKey = s.MetaKey,
                                                      AdditionalPrice = s.AdditionalPrice,
                                                      AdditionalPriceHoliday = s.AdditionalPriceHoliday,
                                                      Alias = s.Alias,
                                                      Status = s.Status,
                                                      Approve = s.Approve,
                                                      FinalPrice = s.FinalPrice,
                                                      FinalPriceHoliday = s.FinalPriceHoliday,
                                                      IdSchedule = s.IdSchedule,
                                                      IsHoliday = s.IsHoliday,
                                                      MinCapacity = s.MinCapacity,
                                                      MaxCapacity = s.MaxCapacity,
                                                      PriceAdult = s.PriceAdult,
                                                      PriceAdultHoliday = s.PriceAdultHoliday,
                                                      PriceChild = s.PriceChild,
                                                      PriceBabyHoliday = s.PriceChildHoliday,
                                                      PriceBaby = s.PriceBaby,
                                                      PriceChildHoliday = s.PriceChildHoliday,
                                                      QuantityAdult = s.QuantityAdult,
                                                      QuantityBaby = s.QuantityBaby,
                                                      QuantityChild = s.QuantityChild,
                                                      QuantityCustomer = s.QuantityCustomer,
                                                      TotalCostTourNotService = s.TotalCostTourNotService,
                                                      Vat = s.Vat,
                                                      Profit = s.Profit,
                                                      TimePromotion = s.TimePromotion,
                                                      Promotions = (from pro in _db.Promotions
                                                                    where pro.IdPromotion == s.PromotionId
                                                                    select pro).FirstOrDefault(),
                                                      Car = (from car in _db.Cars
                                                             where car.IdCar == s.CarId
                                                             select car).First(),
                                                      Timelines = (from timeline in _db.Timelines
                                                                   where timeline.IdSchedule == s.IdSchedule
                                                                   && timeline.IsDelete == false
                                                                   select new Timeline
                                                                   {
                                                                       Description = timeline.Description,
                                                                       FromTime = timeline.FromTime,
                                                                       ToTime = timeline.ToTime,
                                                                   }).ToList(),
                                                      CostTour = (from c in _db.CostTours
                                                                  where c.IdSchedule == s.IdSchedule
                                                                  select c).First(),
                                                      Employee = (from e in _db.Employees
                                                                  where e.IdEmployee == s.EmployeeId
                                                                  select e).First()
                                                  }).ToList()
                                 }).FirstAsync();
                if (list != null)
                {
                    res.Content = list;
                }
                return res;
            }
            catch (Exception e)
            {
                res.Notification.DateTime = DateTime.Now;
                res.Notification.Description = e.Message;
                res.Notification.Messenge = "Có lỗi xảy ra !";
                res.Notification.Type = "Error";
                return res;
            }
        }
    }
}
