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
using static Travel.Shared.Ultilities.Enums;

namespace Travel.Data.Repositories
{

    public class ScheduleRes : ISchedule
    {
        private readonly TravelContext _db;
        private Notification message;
        public ScheduleRes(TravelContext db)
        {
            _db = db;
            message = new Notification();
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
            
                var idUserModify = PrCommon.GetString("idUserModify", frmData);
                if (String.IsNullOrEmpty(idUserModify))
                {
                }
                var typeAction = PrCommon.GetString("typeAction", frmData);

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
                    updateObj.TypeAction = typeAction;
                    updateObj.IdUserModify = Guid.Parse(idUserModify);
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
                createObj.IdUserModify = Guid.Parse(idUserModify); 

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
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);
            }
            catch (Exception e)
            {
               return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);

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
        
                return Ultility.Responses("Thêm thành công !", Enums.TypeCRUD.Success.ToString(), schedule.IdSchedule);
            }
            catch (Exception e)
            {

                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
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
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
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
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
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
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
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

                    return Ultility.Responses("Xóa thành công !", Enums.TypeCRUD.Success.ToString());
                }
                else
                {
                    return Ultility.Responses($"Không tìm thấy Id [{idSchedule}] !", Enums.TypeCRUD.Warning.ToString());
                }
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
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

                    return Ultility.Responses("Khôi phục thành công !", Enums.TypeCRUD.Success.ToString());
                }
                else
                {
                    return Ultility.Responses($"Không tìm thấy Id [{idSchedule}] !", Enums.TypeCRUD.Warning.ToString());
                }
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
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
                    return Ultility.Responses("Cập nhật thành công !", Enums.TypeCRUD.Success.ToString());
                }
                else
                {
                    return Ultility.Responses($"Không tìm thấy Id [{idSchedule}] !", Enums.TypeCRUD.Warning.ToString());
                }
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
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
                                          CostTour = (from c in _db.CostTours
                                                      where c.IdSchedule == x.IdSchedule
                                                      select c).FirstOrDefault(),
                                          Timelines = (from t in _db.Timelines
                                                       where t.IdSchedule == x.IdSchedule
                                                       select t).ToList(),
                                          Tour = (from tour in _db.Tour
                                                  where x.TourId == tour.IdTour
                                                  select tour).FirstOrDefault()
                                      }).FirstAsync();
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), schedule);
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
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
                    return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);
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
                    return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);

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
                    return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result1);
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
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }

        public async Task<Response> GetsScheduleFlashSale()
        {
            try
            {
                var flashSaleDay  = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(Ultility.GetDateZeroTime(DateTime.Now.AddDays(3))); // sau này gắn config

                var list = await (from s in _db.Schedules
                            where s.Isdelete == false
                            && s.Approve == (int)Enums.ApproveStatus.Approved
                            && s.EndDate <= flashSaleDay
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
                                        }).First(),

                            }).OrderBy(x => x.DepartureDate).ToListAsync();


                var result = Mapper.MapSchedule(list);
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);

            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }

        #region dang chỉnh
        public Response DeletePlace(Guid id, Guid idUser)
        {
            try
            {
                var place = (from x in _db.Places
                             where x.IdPlace == id
                             select x).FirstOrDefault();

                var userLogin = (from x in _db.Employees
                                 where x.IdEmployee == idUser
                                 select x).FirstOrDefault();
                if (place.Approve == (int)ApproveStatus.Approved)
                {
                    place.ModifyBy = userLogin.NameEmployee;
                    place.IdUserModify = userLogin.IdEmployee;
                    place.ModifyDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);
                    place.Approve = (int)ApproveStatus.Waiting;

                    res = Ultility.Responses("Đã gửi yêu cầu xóa !", Enums.TypeCRUD.Success.ToString());
                }
                else
                {
                    if (place.IdUserModify == idUser)
                    {
                        if (place.TypeAction == "insert")
                        {
                            _db.Places.Remove(place);
                            res = Ultility.Responses("Đã xóa!", Enums.TypeCRUD.Success.ToString());
                        }
                        else if (place.TypeAction == "update")
                        {
                            var idPlaceTemp = place.IdAction;
                            // old hotel
                            var placeTemp = (from x in _db.Places
                                             where x.IdPlace == Guid.Parse(idPlaceTemp)
                                             select x).FirstOrDefault();
                            place.Approve = (int)ApproveStatus.Approved;
                            place.IdAction = null;
                            place.TypeAction = null;
                            #region restore old data

                            place.Approve = (int)ApproveStatus.Approved;
                            place.Address = placeTemp.Address;

                            place.Phone = placeTemp.Phone;
                            place.NamePlace = placeTemp.NamePlace;
                            place.PriceTicket = placeTemp.PriceTicket;
                            #endregion

                            _db.Places.Remove(placeTemp);
                            res = Ultility.Responses("Đã hủy yêu cầu chỉnh sửa !", Enums.TypeCRUD.Success.ToString());
                        }
                        else if (place.TypeAction == "restore")
                        {
                            place.IdAction = null;
                            place.TypeAction = null;
                            place.IsDelete = true;
                            place.Approve = (int)ApproveStatus.Approved;
                            res = Ultility.Responses("Đã hủy yêu cầu khôi phục!", Enums.TypeCRUD.Success.ToString());

                        }
                        else // delete
                        {
                            place.IdAction = null;
                            place.TypeAction = null;
                            place.IsDelete = false;
                            place.Approve = (int)ApproveStatus.Approved;
                            res = Ultility.Responses("Đã hủy yêu cầu xóa !", Enums.TypeCRUD.Success.ToString());

                        }
                    }
                }
                _db.SaveChanges();
                return res;

            }
            catch (Exception e)
            {
                res = Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
                return res;
            }
        }
        public Response ApprovePlace(Guid id)
        {
            try
            {
                var place = (from x in _db.Places
                             where x.IdPlace == id
                             && x.Approve == (int)ApproveStatus.Waiting
                             select x).FirstOrDefault();
                if (place != null)
                {


                    if (place.TypeAction == "update")
                    {
                        var idPlaceTemp = place.IdAction;
                        place.Approve = (int)ApproveStatus.Approved;
                        place.IdAction = null;
                        place.TypeAction = null;


                        // delete tempdata
                        var placeTemp = (from x in _db.Places
                                         where x.IdPlace == Guid.Parse(idPlaceTemp)
                                         select x).FirstOrDefault();
                        _db.Places.Remove(placeTemp);
                    }
                    else if (place.TypeAction == "insert")
                    {
                        place.IdAction = null;
                        place.TypeAction = null;
                        place.Approve = (int)ApproveStatus.Approved;
                    }
                    else if (place.TypeAction == "restore")
                    {
                        place.IdAction = null;
                        place.TypeAction = null;
                        place.Approve = (int)ApproveStatus.Approved;
                        place.IsDelete = false;

                    }
                    else
                    {
                        place.IdAction = null;
                        place.TypeAction = null;
                        place.Approve = (int)ApproveStatus.Approved;
                        place.IsDelete = true;
                    }




                    _db.SaveChanges();
                    res = Ultility.Responses($"Duyệt thành công !", Enums.TypeCRUD.Success.ToString());
                }
                else
                {
                    res = Ultility.Responses("Không tim thấy dữ liệu !", Enums.TypeCRUD.Warning.ToString());

                }
                return res;

            }
            catch (Exception e)
            {
                res = Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
                return res;
            }
        }
        public Response RefusedPlace(Guid id)
        {
            try
            {
                var place = (from x in _db.Places
                             where x.IdPlace == id
                             && x.Approve == (int)ApproveStatus.Waiting
                             select x).FirstOrDefault();
                if (place != null)
                {
                    if (place.TypeAction == "update")
                    {
                        var idPlaceTemp = place.IdAction;
                        // old hotel
                        var placeTemp = (from x in _db.Places
                                         where x.IdPlace == Guid.Parse(idPlaceTemp)
                                         select x).FirstOrDefault();
                        place.Approve = (int)ApproveStatus.Approved;
                        place.IdAction = null;
                        place.TypeAction = null;
                        #region restore old data

                        place.Approve = (int)ApproveStatus.Approved;
                        place.Address = placeTemp.Address;

                        place.Phone = placeTemp.Phone;
                        place.NamePlace = placeTemp.NamePlace;
                        place.PriceTicket = placeTemp.PriceTicket;
                        #endregion

                        _db.Places.Remove(placeTemp);
                    }
                    else if (place.TypeAction == "insert")
                    {
                        place.IdAction = null;
                        place.TypeAction = null;
                        place.Approve = (int)ApproveStatus.Refused;
                    }
                    else if (place.TypeAction == "restore")
                    {
                        place.IdAction = null;
                        place.TypeAction = null;
                        place.IsDelete = true;
                        place.Approve = (int)ApproveStatus.Approved;
                    }
                    else // delete
                    {
                        place.IdAction = null;
                        place.TypeAction = null;
                        place.IsDelete = false;
                        place.Approve = (int)ApproveStatus.Approved;
                    }
                    _db.SaveChanges();
                    res = Ultility.Responses($"Từ chối thành công !", Enums.TypeCRUD.Success.ToString());
                }
                else
                {
                    res = Ultility.Responses("Không tim thấy dữ liệu !", Enums.TypeCRUD.Warning.ToString());

                }
                return res;

            }
            catch (Exception e)
            {
                res = Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
                return res;
            }
        }

        public Response Update(UpdateScheduleViewModel input)
        {
            try
            {
                var userLogin = (from x in _db.Employees
                                 where x.IdEmployee == input.IdUserModify
                                 select x).FirstOrDefault();

                var schedule = (from x in _db.Schedules
                             where x.IdSchedule == input.IdSchedule
                             select x).FirstOrDefault();

                // clone new object
                var scheduleOld = new Schedule();
                scheduleOld = Ultility.DeepCopy<Schedule>(schedule);
                scheduleOld.IdAction = scheduleOld.IdSchedule.ToString();
                scheduleOld.IdSchedule = $"{Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now)}Temp";
                scheduleOld.IsTempData = true;

                _db.Schedules.Add(scheduleOld);

                #region setdata
                schedule.IdAction = scheduleOld.IdSchedule.ToString();
                schedule.IdUserModify = input.IdUserModify;
                schedule.TypeAction = input.TypeAction;
                schedule.Approve = (int)ApproveStatus.Waiting;


               
                #endregion


                _db.SaveChanges();
                res = Ultility.Responses("Đã gửi yêu cầu sửa !", Enums.TypeCRUD.Success.ToString());
                return res;

            }
            catch (Exception e)
            {
                res = Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
                return res;
            }
        }
        #endregion
    }
}
