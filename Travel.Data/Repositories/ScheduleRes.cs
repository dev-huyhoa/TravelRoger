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
using Travel.Shared.ViewModels.Travel;

namespace Travel.Data.Repositories
{

    public class ScheduleRes : ISchedule
    {
        private readonly TravelContext _db;
        private Notification message;
        private Response res;
        public ScheduleRes(TravelContext db)
        {
            _db = db;
            message = new Notification();
            res = new Response();
        }
        public string CheckBeforSave(JObject frmData, ref Notification _message,bool isUpdate)
        {
            try
            {

                var tourId = PrCommon.GetString("tourId", frmData);
               
                if (String.IsNullOrEmpty(tourId))
                {
                }
                var carId = PrCommon.GetString("carId", frmData);
                if (String.IsNullOrEmpty(carId))
                {
                }
                var employeeId = PrCommon.GetString("employeeId", frmData);
                if (String.IsNullOrEmpty(employeeId))
                {
                }
                var promotionId = PrCommon.GetString("promotionId", frmData);
                if (String.IsNullOrEmpty(promotionId))
                {
                }
                var departureDate = PrCommon.GetString("departureDate", frmData);
                if (String.IsNullOrEmpty(departureDate))
                {
                }
                var returnDate = PrCommon.GetString("returnDate", frmData);
                if (String.IsNullOrEmpty(returnDate))
                {
                }
                var beginDate = PrCommon.GetString("beginDate", frmData);
                if (String.IsNullOrEmpty(beginDate))
                {
                }
                var endDate = PrCommon.GetString("endDate", frmData);
                if (String.IsNullOrEmpty(endDate))
                {
                }
                var timePromotion = PrCommon.GetString("timePromotion", frmData);
                if (String.IsNullOrEmpty(timePromotion))
                {
                }
                var description = PrCommon.GetString("description", frmData);
                if (String.IsNullOrEmpty(description))
                {
                }
                var vat = PrCommon.GetString("vat", frmData);
                if (String.IsNullOrEmpty(vat))
                {
                }
                if (isUpdate)
                {
                    CreateScheduleViewModel updateObj = new CreateScheduleViewModel();
                    updateObj.TourId = tourId;
                    updateObj.CarId = Guid.Parse(carId);
                    updateObj.EmployeeId = Guid.Parse(employeeId);
                    updateObj.PromotionId =Convert.ToInt32(promotionId);
                    updateObj.DepartureDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Parse(departureDate));
                    updateObj.BeginDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Parse(beginDate));
                    updateObj.EndDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Parse(endDate));
                    updateObj.TimePromotion = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Parse(timePromotion));
                    updateObj.IdSchedule = $"{tourId}-S{Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now)}";
                    return JsonSerializer.Serialize(updateObj);
                }
                CreateScheduleViewModel createObj = new CreateScheduleViewModel();
                createObj.TourId = tourId;
                createObj.CarId = Guid.Parse(carId);
                createObj.EmployeeId = Guid.Parse(employeeId);
                createObj.PromotionId = Convert.ToInt32(promotionId);
                createObj.Description = description;
                createObj.Vat = float.Parse(vat);
                createObj.DepartureDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Parse(departureDate));
                createObj.ReturnDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Parse(returnDate));
                createObj.BeginDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Parse(beginDate));
                createObj.EndDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Parse(endDate));
                createObj.TimePromotion = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Parse(timePromotion));
                createObj.IdSchedule = $"{tourId}-S{Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now)}";
                return JsonSerializer.Serialize(createObj);
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

        public Response Gets()
        {
            try
            {
                var list = (from s in _db.Schedules
                            where s.Isdelete == false &&
                            s.Approve == (int)Enums.ApproveStatus.Approved
                            select new Schedule
                            {
                                Alias = s.Alias,
                                Approve = s.Approve,
                                BeginDate = s.BeginDate,
                                QuantityAdult = s.QuantityAdult,
                                QuantityBaby = s.QuantityBaby,
                                QuantityChild = s.QuantityChild,
                                CarId = s.CarId,
                                Description = s.Description,
                                DepartureDate = s.DepartureDate,
                                ReturnDate = s.ReturnDate,
                                EndDate = s.EndDate,
                                Isdelete = s.Isdelete,
                                EmployeeId = s.EmployeeId,
                                IdSchedule = s.IdSchedule,
                                MaxCapacity = s.MaxCapacity,
                                MinCapacity = s.MinCapacity,
                                PromotionId = s.PromotionId,
                                DeparturePlace = s.DeparturePlace,
                                Status = s.Status,
                                TourId = s.TourId,
                                FinalPrice = s.FinalPrice,
                                FinalPriceHoliday = s.FinalPriceHoliday,
                                AdditionalPrice = s.AdditionalPrice,
                                AdditionalPriceHoliday = s.AdditionalPriceHoliday,
                                IsHoliday = s.IsHoliday,
                                Profit = s.Profit,
                                QuantityCustomer = s.QuantityCustomer,
                                TimePromotion = s.TimePromotion,
                                Vat = s.Vat,
                                TotalCostTourNotService = s.TotalCostTourNotService,
                                CostTour = (from c in _db.CostTours where c.IdSchedule == s.IdSchedule select c).First(),
                                Timelines = (from t in _db.Timelines where t.IdSchedule == s.IdSchedule select t).ToList(),
                                Tour = (from t in _db.Tour where s.TourId == t.IdTour select new Tour {
                                    Thumbsnail = t.Thumbsnail,
                                    ToPlace = t.ToPlace,
                                    IdTour = t.IdTour,
                                    NameTour = t.NameTour,
                                    Alias = t.Alias,
                                    ApproveStatus = t.ApproveStatus,
                                    CreateDate = t.CreateDate,
                                    IsActive = t.IsActive,
                                    IsDelete = t.IsDelete,
                                    ModifyBy = t.ModifyBy,
                                    ModifyDate = t.ModifyDate,
                                    Rating = t.Rating,
                                    Status = t.Status
                                }).First(),

                            }).ToList();


                var result = Mapper.MapSchedule(list);
                if (list.Count() > 0)
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

        public Response Create(CreateScheduleViewModel input)
        {
            try
            {
                Schedule schedule =
                schedule = Mapper.MapCreateSchedule(input);
                string nameTour = (from x in _db.Tour where x.IdTour == input.TourId select x).First().NameTour;
                schedule.Alias = $"S{Ultility.SEOUrl(nameTour)}";
                _db.Schedules.Add(schedule);
                _db.SaveChanges();
                res.Content = schedule.IdSchedule;
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

        public Response GetSchedulebyIdTour(string idTour)
        {
            try
            {
                var list = (from s in _db.Schedules
                            where s.TourId == idTour
                            where s.Isdelete == false &&
                            s.Approve == (int)Enums.ApproveStatus.Approved
                            select new Schedule
                            {
                                Alias = s.Alias,
                                Approve = s.Approve,
                                BeginDate = s.BeginDate,
                                QuantityAdult = s.QuantityAdult,
                                QuantityBaby = s.QuantityBaby,
                                QuantityChild = s.QuantityChild,
                                CarId = s.CarId,
                                DepartureDate = s.DepartureDate,
                                ReturnDate = s.ReturnDate,
                                EndDate = s.EndDate,
                                DeparturePlace = s.DeparturePlace,
                                Description = s.Description,
                                MetaDesc = s.MetaDesc,
                                MetaKey = s.MetaKey,
                                Isdelete = s.Isdelete,
                                EmployeeId = s.EmployeeId,
                                IdSchedule = s.IdSchedule,
                                MaxCapacity = s.MaxCapacity,
                                MinCapacity = s.MinCapacity,
                                PromotionId = s.PromotionId,
                                Status = s.Status,
                                TourId = s.TourId,
                                FinalPrice = s.FinalPrice,
                                FinalPriceHoliday = s.FinalPriceHoliday,
                                AdditionalPrice = s.AdditionalPrice,
                                AdditionalPriceHoliday = s.AdditionalPriceHoliday,
                                IsHoliday = s.IsHoliday,
                                Profit = s.Profit,
                                QuantityCustomer = s.QuantityCustomer,
                                TimePromotion = s.TimePromotion,
                                Vat = s.Vat,
                                TotalCostTourNotService = s.TotalCostTourNotService,
                                CostTour = (from c in _db.CostTours where c.IdSchedule == s.IdSchedule select c).First(),
                                Timelines = (from t in _db.Timelines where t.IdSchedule == s.IdSchedule select t).ToList(),
                                Tour = (from t in _db.Tour
                                        where s.TourId == t.IdTour
                                        select new Tour
                                        {
                                            Thumbsnail = t.Thumbsnail,
                                            ToPlace = t.ToPlace,
                                            IdTour = t.IdTour,
                                            NameTour = t.NameTour,
                                            Alias = t.Alias,
                                            ApproveStatus = t.ApproveStatus,
                                            CreateDate = t.CreateDate,
                                            IsActive = t.IsActive,
                                            IsDelete = t.IsDelete,
                                            ModifyBy = t.ModifyBy,
                                            ModifyDate = t.ModifyDate,
                                            Rating = t.Rating,
                                            Status = t.Status,
                                            QuantityBooked = t.QuantityBooked,
                                        }).First(),

                            }).ToList();


                var result = Mapper.MapSchedule(list);
                if (list.Count() > 0)
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

        public Response GetSchedulebyIdTourWaiting(string idTour)
        {
            try
            {
                var list = (from s in _db.Schedules
                            where s.TourId == idTour
                            where s.Isdelete == false &&
                            s.Approve == (int)Enums.ApproveStatus.Waiting
                            select new Schedule
                            {
                                Alias = s.Alias,
                                Approve = s.Approve,
                                BeginDate = s.BeginDate,
                                QuantityAdult = s.QuantityAdult,
                                QuantityBaby = s.QuantityBaby,
                                QuantityChild = s.QuantityChild,
                                CarId = s.CarId,
                                DepartureDate = s.DepartureDate,
                                ReturnDate = s.ReturnDate,
                                DeparturePlace = s.DeparturePlace,
                                Description = s.Description,
                                MetaDesc = s.MetaDesc,
                                MetaKey = s.MetaKey,
                                EndDate = s.EndDate,
                                Isdelete = s.Isdelete,
                                EmployeeId = s.EmployeeId,
                                IdSchedule = s.IdSchedule,
                                MaxCapacity = s.MaxCapacity,
                                MinCapacity = s.MinCapacity,
                                PromotionId = s.PromotionId,
                                Status = s.Status,
                                TourId = s.TourId,
                                FinalPrice = s.FinalPrice,
                                FinalPriceHoliday = s.FinalPriceHoliday,
                                AdditionalPrice = s.AdditionalPrice,
                                AdditionalPriceHoliday = s.AdditionalPriceHoliday,
                                IsHoliday = s.IsHoliday,
                                Profit = s.Profit,
                                QuantityCustomer = s.QuantityCustomer,
                                TimePromotion = s.TimePromotion,
                                Vat = s.Vat,
                                TotalCostTourNotService = s.TotalCostTourNotService,
                                CostTour = (from c in _db.CostTours where c.IdSchedule == s.IdSchedule select c).First(),
                                Timelines = (from t in _db.Timelines where t.IdSchedule == s.IdSchedule select t).ToList(),
                                Tour = (from t in _db.Tour
                                        where s.TourId == t.IdTour
                                        select new Tour
                                        {
                                            Thumbsnail = t.Thumbsnail,
                                            ToPlace = t.ToPlace,
                                            IdTour = t.IdTour,
                                            NameTour = t.NameTour,
                                            Alias = t.Alias,
                                            ApproveStatus = t.ApproveStatus,
                                            CreateDate = t.CreateDate,
                                            IsActive = t.IsActive,
                                            IsDelete = t.IsDelete,
                                            ModifyBy = t.ModifyBy,
                                            ModifyDate = t.ModifyDate,
                                            Rating = t.Rating,
                                            Status = t.Status,
                                            QuantityBooked = t.QuantityBooked,
                                        }).First(),

                            }).ToList();


                var result = Mapper.MapSchedule(list);
                if (list.Count() > 0)
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
        public Response Delete(string idSchedule)
        {
            try
            {
                var schedule = _db.Schedules.Find(idSchedule);
                if (schedule != null)
                {
                    schedule.Isdelete = true;
                    _db.SaveChanges();

                    res.Notification.DateTime = DateTime.Now;
                    res.Notification.Messenge = "Xóa thành công !";
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

        public Response RestoreShedule(string idSchedule)
        {
            try
            {
                var schedule = _db.Schedules.Find(idSchedule);
                if (schedule != null)
                {
                    schedule.Isdelete = false;
                    _db.SaveChanges();

                    res.Notification.DateTime = DateTime.Now;
                    res.Notification.Messenge = "Xóa thành công !";
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


        public Response UpdatePromotion(string idSchedule, int idPromotion)
        {
            try
            {
                var schedule = (from x in _db.Schedules where x.IdSchedule == idSchedule select x).First();
                if (schedule != null)
                {
                    var promotion = (from x in _db.Promotions where x.IdPromotion == idPromotion select x).First();
                    if (promotion != null)
                    {
                        schedule.PromotionId = promotion.IdPromotion;
                        schedule.TimePromotion = promotion.ToDate;
                        _db.SaveChanges();
                     
                    }
                    res.Notification.DateTime = DateTime.Now;
                    res.Notification.Messenge = "Cập nhật thành công !";
                    res.Notification.Type = "Success";
                    return res;
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
        // chưa cập nhật
        public async Task<string> UpdateCapacity(string idSchedule,int adult = 1, int child = 0, int baby = 0)
        {
            try
            {
                var schedule =await (from x in _db.Schedules where x.IdSchedule == idSchedule select x).FirstAsync();
                int quantity = (adult + child + baby % 2);
                schedule.QuantityAdult = adult;
                schedule.QuantityBaby = baby;
                schedule.QuantityChild = child;
                schedule.QuantityCustomer = quantity;
                await _db.SaveChangesAsync();
                return "Success";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public async Task<Response> Get(string idSchedule)
        {
            try
            {
                int approve = Convert.ToInt16(Enums.ApproveStatus.Approved);
                var schedule = await (from x in _db.Schedules
                                      where x.Isdelete == false
                                      && x.Approve == approve
                                      && x.IdSchedule == idSchedule
                                      select new Schedule
                                      {
                                          IdSchedule = x.IdSchedule,
                                          AdditionalPrice = x.AdditionalPrice,
                                          AdditionalPriceHoliday = x.AdditionalPriceHoliday,
                                          Alias = x.Alias,
                                          PriceAdult = x.PriceAdult,
                                          PriceBaby = x.PriceBaby,
                                          PriceChild = x.PriceChild,
                                          PriceAdultHoliday = x.PriceAdultHoliday,
                                          PriceBabyHoliday = x.PriceBabyHoliday,
                                          PriceChildHoliday = x.PriceChildHoliday,
                                          QuantityAdult = x.QuantityAdult,
                                          QuantityBaby = x.QuantityBaby,
                                          QuantityChild = x.QuantityChild,
                                          BeginDate = x.BeginDate,
                                          EndDate = x.EndDate,
                                          DepartureDate = x.DepartureDate,
                                          ReturnDate = x.ReturnDate,
                                          Description = x.Description,
                                          IsHoliday = x.IsHoliday,
                                          Timelines = (from t in _db.Timelines
                                                       where t.IdSchedule == x.IdSchedule
                                                       select t).ToList(),
                                          Tour = (from tour in _db.Tour
                                                  where x.TourId == tour.IdTour
                                                  select tour).First()
                                      }).FirstAsync();
                if (schedule != null)
                {
                    res.Content = schedule;
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

        public async Task<Response> SearchTour(string from, string to, DateTime? departureDate, DateTime? returnDate)
        {
            try
            {
                //var list = await (from s in _db.Schedules
                //            where s.Isdelete == false
                //            && s.Approve == (int)Enums.ApproveStatus.Approved
                //            select new Schedule
                //            {
                //                Alias = s.Alias,
                //                Approve = s.Approve,
                //                BeginDate = s.BeginDate,
                //                QuantityAdult = s.QuantityAdult,
                //                QuantityBaby = s.QuantityBaby,
                //                QuantityChild = s.QuantityChild,
                //                CarId = s.CarId,
                //                DepartureDate = s.DepartureDate,
                //                ReturnDate = s.ReturnDate,
                //                DeparturePlace = s.DeparturePlace,
                //                Description = s.Description,
                //                MetaDesc = s.MetaDesc,
                //                MetaKey = s.MetaKey,
                //                EndDate = s.EndDate,
                //                Isdelete = s.Isdelete,
                //                EmployeeId = s.EmployeeId,
                //                IdSchedule = s.IdSchedule,
                //                MaxCapacity = s.MaxCapacity,
                //                MinCapacity = s.MinCapacity,
                //                PromotionId = s.PromotionId,
                //                Status = s.Status,
                //                TourId = s.TourId,
                //                FinalPrice = s.FinalPrice,
                //                FinalPriceHoliday = s.FinalPriceHoliday,
                //                AdditionalPrice = s.AdditionalPrice,
                //                AdditionalPriceHoliday = s.AdditionalPriceHoliday,
                //                IsHoliday = s.IsHoliday,
                //                Profit = s.Profit,
                //                QuantityCustomer = s.QuantityCustomer,
                //                TimePromotion = s.TimePromotion,
                //                Vat = s.Vat,
                //                TotalCostTourNotService = s.TotalCostTourNotService,
                //                CostTour = (from c in _db.CostTours where c.IdSchedule == s.IdSchedule select c).First(),
                //                Timelines = (from t in _db.Timelines where t.IdSchedule == s.IdSchedule select t).ToList(),
                //                Tour = (from t in _db.Tour
                //                        where s.TourId == t.IdTour
                //                        select new Tour
                //                        {
                //                            Thumbsnail = t.Thumbsnail,
                //                            ToPlace = t.ToPlace,
                //                            IdTour = t.IdTour,
                //                            NameTour = t.NameTour,
                //                            Alias = t.Alias,
                //                            ApproveStatus = t.ApproveStatus,
                //                            CreateDate = t.CreateDate,
                //                            IsActive = t.IsActive,
                //                            IsDelete = t.IsDelete,
                //                            ModifyBy = t.ModifyBy,
                //                            ModifyDate = t.ModifyDate,
                //                            Rating = t.Rating,
                //                            Status = t.Status,
                //                            QuantityBooked = t.QuantityBooked,
                //                        }).First(),

                //            }).ToListAsync();
                long dateTimeNowUnix = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);
                var list = await (from x in _db.Schedules
                                  where x.EndDate <= dateTimeNowUnix
                                  && x.Isdelete == false
                                  && x.Approve == (int)Enums.ApproveStatus.Approved
                                  select x
                                  ).ToListAsync();
                if (departureDate != null)
                {
                    long unixDepartureDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(departureDate.Value);
                    list = (from x in list
                            where x.DepartureDate >= unixDepartureDate
                            select x).ToList();
                }
                if (returnDate != null)
                {
                    long unixReturnDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(returnDate.Value);
                    list = (from x in list
                            where x.DepartureDate <= unixReturnDate
                            select x).ToList();
                }
                if (!string.IsNullOrEmpty(from))
                {
                    string keyFrom = Ultility.removeVietnameseSign(from.ToLower());
                    list = (from x in list
                            where Ultility.removeVietnameseSign(x.DeparturePlace.ToLower()).Contains(keyFrom)
                            select x).ToList();
                }
                list = (from s in list
                        select new Schedule
                        {
                            Alias = s.Alias,
                            Approve = s.Approve,
                            BeginDate = s.BeginDate,
                            QuantityAdult = s.QuantityAdult,
                            QuantityBaby = s.QuantityBaby,
                            QuantityChild = s.QuantityChild,
                            CarId = s.CarId,
                            Description = s.Description,
                            DepartureDate = s.DepartureDate,
                            ReturnDate = s.ReturnDate,
                            EndDate = s.EndDate,
                            Isdelete = s.Isdelete,
                            EmployeeId = s.EmployeeId,
                            IdSchedule = s.IdSchedule,
                            MaxCapacity = s.MaxCapacity,
                            MinCapacity = s.MinCapacity,
                            PromotionId = s.PromotionId,
                            DeparturePlace = s.DeparturePlace,
                            Status = s.Status,
                            TourId = s.TourId,
                            FinalPrice = s.FinalPrice,
                            FinalPriceHoliday = s.FinalPriceHoliday,
                            AdditionalPrice = s.AdditionalPrice,
                            AdditionalPriceHoliday = s.AdditionalPriceHoliday,
                            IsHoliday = s.IsHoliday,
                            Profit = s.Profit,
                            QuantityCustomer = s.QuantityCustomer,
                            TimePromotion = s.TimePromotion,
                            Vat = s.Vat,
                            TotalCostTourNotService = s.TotalCostTourNotService,
                            CostTour = (from c in _db.CostTours where c.IdSchedule == s.IdSchedule select c).First(),
                            Timelines = (from t in _db.Timelines where t.IdSchedule == s.IdSchedule select t).ToList(),
                            Tour = (from t in _db.Tour
                                    where s.TourId == t.IdTour
                                    select new Tour
                                    {
                                        Thumbsnail = t.Thumbsnail,
                                        ToPlace = t.ToPlace,
                                        IdTour = t.IdTour,
                                        NameTour = t.NameTour,
                                        Alias = t.Alias,
                                        ApproveStatus = t.ApproveStatus,
                                        CreateDate = t.CreateDate,
                                        IsActive = t.IsActive,
                                        IsDelete = t.IsDelete,
                                        ModifyBy = t.ModifyBy,
                                        ModifyDate = t.ModifyDate,
                                        Rating = t.Rating,
                                        Status = t.Status
                                    }).First(),

                        }).ToList();
                if (!string.IsNullOrEmpty(to))
                {
                    string keyTo = Ultility.removeVietnameseSign(to.ToLower());
                    list = (from x in list
                            where Ultility.removeVietnameseSign(x.Tour.ToPlace.ToLower()).Contains(keyTo)
                            select x).ToList();
                }
                var result = Mapper.MapSchedule(list);
                if (list.Count() > 0)
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
    }
}
