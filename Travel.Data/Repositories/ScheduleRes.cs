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
                var idSchedule = PrCommon.GetString("idSchedule", frmData);

                if (String.IsNullOrEmpty(idSchedule))
                {
                }
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
                var departurePlace = PrCommon.GetString("departurePlace", frmData);
                if (String.IsNullOrEmpty(departurePlace))
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
                var minCapacity = PrCommon.GetString("minCapacity", frmData);
                if (String.IsNullOrEmpty(minCapacity))
                {
                }
                var maxCapacity = PrCommon.GetString("maxCapacity", frmData);
                if (String.IsNullOrEmpty(maxCapacity))
                {
                }
                if (isUpdate)
                {
                    UpdateScheduleViewModel updateObj = new UpdateScheduleViewModel();
                    updateObj.TourId = tourId;
                    updateObj.CarId = Guid.Parse(carId);
                    updateObj.EmployeeId = Guid.Parse(employeeId);
                    updateObj.PromotionId =Convert.ToInt32(promotionId);
                    updateObj.Description = description;
                    updateObj.Vat = float.Parse(vat);
                    updateObj.DeparturePlace = departurePlace;

                    updateObj.DepartureDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Parse(departureDate));
                    updateObj.ReturnDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Parse(returnDate));
                    updateObj.BeginDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Parse(beginDate));
                    updateObj.EndDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Parse(endDate));
                    updateObj.TimePromotion = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Parse(timePromotion));

                    updateObj.MinCapacity = Convert.ToInt16(minCapacity);
                    updateObj.MaxCapacity = Convert.ToInt16(maxCapacity);
                    updateObj.IdSchedule = idSchedule;
                    return JsonSerializer.Serialize(updateObj);
                }
                CreateScheduleViewModel createObj = new CreateScheduleViewModel();
                createObj.TourId = tourId;
                createObj.CarId = Guid.Parse(carId);
                createObj.EmployeeId = Guid.Parse(employeeId);
                createObj.PromotionId = Convert.ToInt32(promotionId);
                createObj.Description = description;
                createObj.Vat = float.Parse(vat);
                createObj.DeparturePlace = departurePlace;
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
                                CostTour = (from c in _db.CostTours where c.IdSchedule == s.IdSchedule select c).FirstOrDefault(),
                                Timelines = (from t in _db.Timelines where t.IdSchedule == s.IdSchedule select t).ToList(),
                                Promotions = (from p in _db.Promotions where p.IdPromotion == s.PromotionId select p).FirstOrDefault(),
                                Tour = (from t in _db.Tour where s.TourId == t.IdTour select new Tour {
                                    Thumbnail = t.Thumbnail,
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

                            }).OrderBy(x=> x.DepartureDate).ToList();


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

        public Response Update(UpdateScheduleViewModel input)
        {
            try
            {
                Schedule schedule = Mapper.MapUpdateSchedule(input);
                _db.Schedules.Update(schedule);
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

        public Response GetsSchedulebyIdTour(string idTour, bool isDelete)
        {
            try
            {
                var list = (from s in _db.Schedules
                            where s.TourId == idTour
                            && s.Isdelete == isDelete 
                            && s.Approve == (int)Enums.ApproveStatus.Approved
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
                                            Thumbnail = t.Thumbnail,
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

        public Response CusGetsSchedulebyIdTour(string idTour)
        {
            try
            {
                var dateTimeNow = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);
                var list = (from s in _db.Schedules
                            where s.TourId == idTour
                            && s.EndDate >= dateTimeNow
                            && s.Status == (int)Enums.StatusSchedule.Free
                            orderby s.DepartureDate
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
                            }).ToList();


                //var result = Mapper.MapSchedule(list);
                var result = list;
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
                                            Thumbnail = t.Thumbnail,
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
                                          MinCapacity = x.MinCapacity,
                                          MaxCapacity = x.MaxCapacity,
                                          QuantityCustomer = x.QuantityCustomer,
                                          AdditionalPrice = x.AdditionalPrice,
                                          AdditionalPriceHoliday = x.AdditionalPriceHoliday,
                                          Alias = x.Alias,
                                          FinalPrice = x.FinalPrice,
                                          FinalPriceHoliday = x.FinalPriceHoliday,
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
                                          DeparturePlace = x.DeparturePlace,
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
                if (departureDate != null && returnDate != null)
                {
                    long dateTimeNowUnix = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);
                    var list = await (from x in _db.Schedules
                                      where x.EndDate >= dateTimeNowUnix
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
                                CostTour = (from c in _db.CostTours where c.IdSchedule == s.IdSchedule select c).FirstOrDefault(),
                                Timelines = (from t in _db.Timelines where t.IdSchedule == s.IdSchedule select t).ToList(),
                                Tour = (from t in _db.Tour
                                        where s.TourId == t.IdTour
                                        select new Tour
                                        {
                                            Thumbnail = t.Thumbnail,
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
                                        }).FirstOrDefault(),

                            }).ToList();
                    if (!string.IsNullOrEmpty(to))
                    {
                        string keyTo = Ultility.removeVietnameseSign(to.ToLower());
                        list = (from x in list
                                where Ultility.removeVietnameseSign(x.Tour.ToPlace.ToLower()).Contains(keyTo)
                                select x).OrderByDescending(x => x.DepartureDate).ToList();
                    }
                    var result = Mapper.MapSchedule(list);
                    if (list.Count() > 0)
                    {
                        res.Content = result;
                    }

                    return res;
                }
                else if (departureDate == null && returnDate == null)
                {
                    long dateTimeNowUnix = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);
                    var list2 = await (from x in _db.Schedules
                                      where x.EndDate >= dateTimeNowUnix
                                      && x.Isdelete == false
                                      && x.Approve == (int)Enums.ApproveStatus.Approved
                                      select x
                                     ).ToListAsync();

                    if (!string.IsNullOrEmpty(from))
                    {
                        string keyFrom = Ultility.removeVietnameseSign(from.ToLower());
                        list2 = (from x in list2
                                 where Ultility.removeVietnameseSign(x.DeparturePlace.ToLower()).Contains(keyFrom)
                                select x).ToList();
                    }
                    list2 = (from s in list2
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
                                CostTour = (from c in _db.CostTours where c.IdSchedule == s.IdSchedule select c).FirstOrDefault(),
                                Timelines = (from t in _db.Timelines where t.IdSchedule == s.IdSchedule select t).ToList(),
                                Tour = (from t in _db.Tour
                                        where s.TourId == t.IdTour
                                        select new Tour
                                        {
                                            Thumbnail = t.Thumbnail,
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
                                        }).FirstOrDefault(),

                            }).ToList();
                    if (!string.IsNullOrEmpty(to))
                    {
                        string keyTo = Ultility.removeVietnameseSign(to.ToLower());
                        list2 = (from x in list2
                                 where Ultility.removeVietnameseSign(x.Tour.ToPlace.ToLower()).Contains(keyTo)
                                select x).OrderByDescending(x => x.DepartureDate).ToList();
                    }
                    var result = Mapper.MapSchedule(list2);
                    if (list2.Count() > 0)
                    {
                        res.Content = result;
                    }

                    return res;

                }
                else
                {
                     long dateTimeNowUnix1 = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);
                    var list1 = new List<Schedule>();
                    if (departureDate != null)
                    {
                        var fromDepartTureDate1 = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(departureDate.Value);
                        var toDepartTureDate1 = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(departureDate.Value.AddDays(1).AddMinutes(-1));
                        // cách 1
                        list1 = await (from x in _db.Schedules
                                       where x.EndDate >= dateTimeNowUnix1
                                       && x.DepartureDate >= fromDepartTureDate1
                                       && x.DepartureDate <= toDepartTureDate1
                                       && x.Isdelete == false
                                       && x.Approve == (int)Enums.ApproveStatus.Approved
                                       select x
                                      ).ToListAsync();
                        // cách 2 
                        //list1 = await (from x in _db.Schedules
                        //               where x.EndDate <= dateTimeNowUnix1
                        //               && (x.DepartureDate >= fromDepartTureDate1 && x.DepartureDate <= toDepartTureDate1)
                        //               && x.Isdelete == false
                        //               && x.Approve == (int)Enums.ApproveStatus.Approved
                        //               select x
                        //              ).ToListAsync();
                    }
                    else
                    {
                        var fromReturnDate1 = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(returnDate.Value);
                        var toReturnDate1 = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(returnDate.Value.AddDays(1).AddMinutes(-1));
                        list1 = await (from x in _db.Schedules
                                       where x.EndDate >= dateTimeNowUnix1
                                       && x.ReturnDate >= fromReturnDate1
                                       && x.ReturnDate <= toReturnDate1
                                       && x.Isdelete == false
                                       && x.Approve == (int)Enums.ApproveStatus.Approved
                                       select x
                                                             ).ToListAsync();
                    }


                    if (!string.IsNullOrEmpty(from))
                    {
                        string keyFrom = Ultility.removeVietnameseSign(from.ToLower());
                        list1 = (from x in list1
                                 where Ultility.removeVietnameseSign(x.DeparturePlace.ToLower()).Contains(keyFrom)
                                select x).ToList();
                    }
                    list1 = (from s in list1
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
                                CostTour = (from c in _db.CostTours where c.IdSchedule == s.IdSchedule select c).FirstOrDefault(),
                                Timelines = (from t in _db.Timelines where t.IdSchedule == s.IdSchedule select t).ToList(),
                                Tour = (from t in _db.Tour
                                        where s.TourId == t.IdTour
                                        select new Tour
                                        {
                                            Thumbnail = t.Thumbnail,
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
                                        }).FirstOrDefault(),

                            }).ToList();
                    if (!string.IsNullOrEmpty(to))
                    {
                        string keyTo = Ultility.removeVietnameseSign(to.ToLower());
                        list1 = (from x in list1
                                 where Ultility.removeVietnameseSign(x.Tour.ToPlace.ToLower()).Contains(keyTo)
                                select x).OrderByDescending(x => x.DepartureDate).ToList();
                    }
                    var result1 = Mapper.MapSchedule(list1);
                    if (list1.Count() > 0)
                    {
                        res.Content = result1;
                    }

                    return res;
                }
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
