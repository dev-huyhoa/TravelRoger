using Microsoft.AspNetCore.Http;
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
using static Travel.Shared.Ultilities.Enums;

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
        private Employee GetCurrentUser(Guid IdUserModify)
        {
            return (from x in _db.Employees
                    where x.IdEmployee == IdUserModify
                    select x).FirstOrDefault();
        }
        public string CheckBeforSave(IFormCollection frmdata, IFormFile file, ref Notification _message, bool isUpdate)
        {
            try
            {
                JObject frmData = JObject.Parse(frmdata["data"]);
                if (frmData != null)
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

                    #region check update info tour when having booking

                    if (isUpdate)
                    {
                        idTour = PrCommon.GetString("idTour", frmData);
                        if (CheckAnyBookingInTour(idTour))
                        {
                            _message = Ultility.Responses("Tour đang có booking !", Enums.TypeCRUD.Warning.ToString()).Notification;
                            return null;
                        }
                    }

                    #endregion

                    var thumbnail = PrCommon.GetString("thumbnail", frmData);
                    if (String.IsNullOrEmpty(thumbnail))
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
                    var typeAction = PrCommon.GetString("typeAction", frmData);
                    var idUserModify = PrCommon.GetString("idUserModify", frmData);
                    if (String.IsNullOrEmpty(idUserModify))
                    {
                    }
                    if (isUpdate)
                    {
                        // map data
                        UpdateTourViewModel objUpdate = new UpdateTourViewModel();
                        objUpdate.NameTour = tourName;
                        objUpdate.Thumbnail = thumbnail;
                        objUpdate.ToPlace = toPlace;
                        objUpdate.TypeAction = typeAction;
                        objUpdate.IdUserModify = Guid.Parse(idUserModify);
                        // generate ID
                        objUpdate.IdTour = idTour;
                        return JsonSerializer.Serialize(objUpdate);
                    }
                    // map data
                    CreateTourViewModel obj = new CreateTourViewModel();
                    obj.NameTour = tourName;
                    obj.Thumbnail = thumbnail;
                    obj.ToPlace = toPlace;
                    obj.TypeAction = typeAction;
                    obj.IdUserModify = Guid.Parse(idUserModify);
                    // generate ID
                    obj.IdTour = idTour;

                    return JsonSerializer.Serialize(obj);
                }
                return string.Empty;
            }
            catch (Exception e)
            {
                _message = Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message).Notification;
                return string.Empty;
            }
        }
        public Response Create(CreateTourViewModel input)
        {
            try
            {
                Tour tour =
                tour = Mapper.MapCreateTour(input);
                tour.TypeAction = "insert";
                _db.Tour.Add(tour);
                _db.SaveChanges();
                return Ultility.Responses("Thêm thành công !", Enums.TypeCRUD.Success.ToString());
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }



        public Response Get(bool isDelete)
        {
            try
            {
                var list = (from x in _db.Tour
                            where x.IsDelete == isDelete
                            && x.ApproveStatus == Convert.ToInt16(Enums.ApproveStatus.Approved)
                            && x.IsTempdata == false
                            select x).ToList();
                var result = Mapper.MapTour(list);
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }

        public Response GetTour(string idTour)
        {
            try
            {
                // cách 1
                //Tour tour = _db.Tour.Find(idTour);
                // cashc 2
                Tour tour = (from x in _db.Tour
                             where x.IdTour == idTour
                             select x).FirstOrDefault();
                var result = Mapper.MapTour(tour);
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
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
                                  && x.IsTempdata == false
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
                                                   && s.Isdelete == false
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
                                                   }).ToList()
                                  }).ToListAsync();
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), list);
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }

        public async Task<Response> GetTourById(string idTour)
        {
            try
            {
                var dateTimeNow = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);
                var list = await (from x in _db.Tour
                                  where x.IsDelete == false
                                  && x.IdTour == idTour
                                  && x.ApproveStatus == Convert.ToInt16(Enums.ApproveStatus.Approved)
                                  && x.IsActive == true
                                  && x.IsTempdata == false
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
                                                   && s.EndDate >= dateTimeNow
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
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), list);
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }


        #region Đang chỉnh
        public Response Update(UpdateTourViewModel input)
        {
            try
            {
                Tour tour = (from x in _db.Tour
                             where x.IdTour == input.IdTour
                             && x.IsDelete == false
                             && x.ApproveStatus == (int)ApproveStatus.Approved
                             select x).FirstOrDefault();
                var userLogin = GetCurrentUser(input.IdUserModify);
                // clone new object
                var tourOld = new Tour();
                tourOld = Ultility.DeepCopy<Tour>(tour);
                tourOld.IdAction = tourOld.IdTour.ToString();
                tourOld.IdTour = $"{Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now)}TempData";
                tourOld.IsTempdata = true;
                _db.Tour.Add(tourOld);


                #region setdata
                tour.IdAction = tourOld.IdTour.ToString();
                tour.IdUserModify = input.IdUserModify;
                tour.ApproveStatus = (int)ApproveStatus.Waiting;
                tour.ModifyBy = userLogin.NameEmployee;
                tour.ModifyDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);

                tour.Alias = Ultility.SEOUrl(input.NameTour);
                tour.NameTour = input.NameTour;
                tour.Thumbnail = input.Thumbnail;
                tour.ToPlace = input.ToPlace;
                tour.TypeAction = "update";
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

        public Response Delete(string idTour, Guid idUser)
        {
            try
            {
                var tour = (from x in _db.Tour
                            where x.IdTour == idTour
                            select x).FirstOrDefault();
                var userLogin = (from x in _db.Employees
                                 where x.IdEmployee == idUser
                                 select x).FirstOrDefault();
                var unixNow = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);
                if (tour != null)
                {
                    if (tour.ApproveStatus == (int)ApproveStatus.Approved)
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

                        if (scheduleInTour.Count != 0)
                        {
                            tour.IsDelete = true;
                            tour.ApproveStatus = (int)ApproveStatus.Waiting;
                            tour.TypeAction = "delete";
                            tour.IdUserModify = idUser;
                            tour.ModifyBy = userLogin.NameEmployee;
                            tour.ModifyDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);
                            _db.SaveChanges();
                            res = Ultility.Responses("Đã gửi yêu cầu xóa !", Enums.TypeCRUD.Success.ToString());
                        }
                        else
                        {
                            res = Ultility.Responses("Tour đang có booking !", Enums.TypeCRUD.Warning.ToString());
                        }
                    }
                    else
                    {
                        if (tour.IdUserModify == idUser)
                        {
                            if (tour.TypeAction == "insert")
                            {
                                _db.Tour.Remove(tour);
                                res = Ultility.Responses("Đã xóa!", Enums.TypeCRUD.Success.ToString());
                            }
                            else if (tour.TypeAction == "update")
                            {
                                var idTourTemp = tour.IdAction;
                                // old hotel
                                var tourTemp = (from x in _db.Tour
                                                where x.IdTour == idTourTemp
                                                select x).FirstOrDefault();

                                tour.IdAction = null;
                                tour.TypeAction = null;

                                #region restore old data

                                tour.Alias = tourTemp.Alias;
                                tour.NameTour = tourTemp.NameTour;
                                tour.Thumbnail = tourTemp.Thumbnail;
                                tour.ApproveStatus = (int)ApproveStatus.Approved;
                                tour.ToPlace = tourTemp.ToPlace;
                                #endregion
                                _db.Tour.Remove(tourTemp);
                                res = Ultility.Responses("Đã hủy yêu cầu chỉnh sửa !", Enums.TypeCRUD.Success.ToString());

                            }

                            else if (tour.TypeAction == "restore")
                            {
                                tour.IdAction = null;
                                tour.TypeAction = null;
                                tour.IsDelete = true;
                                tour.ApproveStatus = (int)ApproveStatus.Approved;
                                res = Ultility.Responses("Đã hủy yêu cầu khôi phục !", Enums.TypeCRUD.Success.ToString());

                            }
                            else // delete
                            {
                                tour.IdAction = null;
                                tour.TypeAction = null;
                                tour.IsDelete = false;
                                tour.ApproveStatus = (int)ApproveStatus.Approved;
                                res = Ultility.Responses("Đã hủy yêu cầu xóa !", Enums.TypeCRUD.Success.ToString());
                            }
                            _db.SaveChanges();
                        }
                        else
                        {
                            res = Ultility.Responses("Bạn không thể thực thi hành động này !", Enums.TypeCRUD.Success.ToString());
                        }
                    }
                }
                else
                {
                    res = Ultility.Responses("Không tìm thấy !", Enums.TypeCRUD.Warning.ToString());
                }
                return res;
            }
            catch (Exception e)
            {
                res = Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
                return res;
            }
        }
        public Response GetWaiting(Guid idUser)
        {
            try
            {
                var userLogin = (from x in _db.Employees
                                 where x.IdEmployee == idUser
                                 select x).FirstOrDefault();
                var listWaiting = new List<Tour>();
                if (userLogin.RoleId == (int)Enums.TitleRole.Admin)
                {
                    listWaiting = (from x in _db.Tour
                                   where x.ApproveStatus == Convert.ToInt16(ApproveStatus.Waiting)
                                   select x).ToList();
                }
                else
                {
                    listWaiting = (from x in _db.Tour
                                   where x.IdUserModify == idUser
                                   && x.ApproveStatus == Convert.ToInt16(ApproveStatus.Waiting)
                                   select x).ToList();
                }
                var result = Mapper.MapTour(listWaiting);
                if (listWaiting.Count() > 0)
                {
                    res = Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);
                }
                return res;
            }
            catch (Exception e)
            {
                res = Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
                return res;
            }
        }

        public Response Approve(string idTour)
        {
            try
            {
                var tour = (from x in _db.Tour
                            where x.IdTour == idTour
                            && x.ApproveStatus == (int)ApproveStatus.Waiting
                            select x).FirstOrDefault();
                if (tour != null)
                {


                    if (tour.TypeAction == "update")
                    {
                        var idTourTemp = tour.IdAction;
                        tour.ApproveStatus = (int)ApproveStatus.Approved;
                        tour.IdAction = null;
                        tour.TypeAction = null;


                        // delete tempdata
                        var tourTemp = (from x in _db.Tour
                                        where x.IdTour == idTourTemp
                                        select x).FirstOrDefault();
                        _db.Tour.Remove(tourTemp);
                    }
                    else if (tour.TypeAction == "insert")
                    {
                        tour.IdAction = null;
                        tour.TypeAction = null;
                        tour.ApproveStatus = (int)ApproveStatus.Approved;
                    }
                    else if (tour.TypeAction == "restore")
                    {
                        tour.IdAction = null;
                        tour.TypeAction = null;
                        tour.ApproveStatus = (int)ApproveStatus.Approved;
                        tour.IsDelete = false;

                    }
                    else
                    {
                        tour.IdAction = null;
                        tour.TypeAction = null;
                        tour.ApproveStatus = (int)ApproveStatus.Approved;
                        tour.IsDelete = true;
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
        public Response Refused(string idTour)
        {
            try
            {
                var tour = (from x in _db.Tour
                            where x.IdTour == idTour
                            && x.ApproveStatus == (int)ApproveStatus.Waiting
                            select x).FirstOrDefault();
                if (tour != null)
                {
                    if (tour.TypeAction == "insert")
                    {
                        tour.ApproveStatus = (int)ApproveStatus.Refused;
                        tour.IdAction = null;
                        tour.TypeAction = null;
                    }
                    else if (tour.TypeAction == "update")
                    {
                        var idTourTemp = tour.IdAction;
                        // old hotel
                        var tourTemp = (from x in _db.Tour
                                        where x.IdTour == idTourTemp
                                        select x).FirstOrDefault();

                        tour.IdAction = null;
                        tour.TypeAction = null;

                        #region restore old data

                        tour.Alias = tourTemp.Alias;
                        tour.NameTour = tourTemp.NameTour;
                        tour.Thumbnail = tourTemp.Thumbnail;
                        tour.ToPlace = tourTemp.ToPlace;
                        tour.ApproveStatus = (int)ApproveStatus.Approved;
                        #endregion
                        _db.Tour.Remove(tourTemp);

                    }

                    else if (tour.TypeAction == "restore")
                    {
                        tour.IdAction = null;
                        tour.TypeAction = null;
                        tour.IsDelete = true;
                        tour.ApproveStatus = (int)ApproveStatus.Approved;

                    }
                    else // delete
                    {
                        tour.IdAction = null;
                        tour.TypeAction = null;
                        tour.IsDelete = false;
                        tour.ApproveStatus = (int)ApproveStatus.Approved;
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


        public Response RestoreTour(string idTour, Guid idUser)
        {
            try
            {
                var tour = (from x in _db.Tour
                            where x.IdTour == idTour
                            && x.IsDelete == true
                            select x).FirstOrDefault();
                var userLogin = (from x in _db.Employees
                                 where x.IdEmployee == idUser
                                 select x).FirstOrDefault();
                if (tour != null)
                {
                    tour.IsDelete = false;
                    tour.IdUserModify = userLogin.IdEmployee;
                    tour.ApproveStatus = (int)ApproveStatus.Waiting;
                    tour.TypeAction = "restore";
                    tour.ModifyBy = userLogin.NameEmployee;
                    tour.ModifyDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);
                    _db.SaveChanges();

                    res = Ultility.Responses($"Đã gửi yêu cầu khôi phục !", Enums.TypeCRUD.Success.ToString());
                }
                else
                {
                    res = Ultility.Responses($"Không tìm thấy !", Enums.TypeCRUD.Warning.ToString());
                }
                return res;
            }
            catch (Exception e)
            {
                res = Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
                return res;
            }
        }

        #endregion
        public async Task<Response> GetsTourByRating()
        {
            try
            {
                var dateTimeNow = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);
                var list = await (from x in _db.Tour
                                  where x.Rating >= 9
                                  && x.IsDelete == false
                                  && x.ApproveStatus == Convert.ToInt16(Enums.ApproveStatus.Approved)
                                  && x.IsActive == true
                                  && x.IsTempdata == false
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
                                                   }).ToList()
                                  }).OrderByDescending(x => x.Rating).ToListAsync();

                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), list);
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }


        private bool CheckAnyBookingInTour(string idTour) // chỉ dùng khi thay đổi thông tin tour
        {
            // cách 1
            var scheduleInTour = (from x in _db.Schedules
                                  where x.TourId == idTour
                                  && x.Isdelete == false
                                  && x.Approve == (int)Enums.ApproveStatus.Approved
                                  && (x.Status == (int)Enums.StatusSchedule.Finished || (x.Status == (int)Enums.StatusSchedule.Free && x.QuantityCustomer == 0))
                                  select x).ToList();
            if (scheduleInTour.Count != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Response UpdateRating(int rating, string idTour)
        {
            try
            {
                var tour = (from x in _db.Tour
                            where x.IdTour == idTour
                            select x).FirstOrDefault();
               
                if (tour != null)
                {
                    tour.Rating =(rating);                 
                    _db.SaveChanges();

                    return Ultility.Responses($"Đổi thành công !", Enums.TypeCRUD.Success.ToString());
                }
                else
                {
                    return Ultility.Responses($"Không tìm thấy !", Enums.TypeCRUD.Warning.ToString());
                }
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
                
            }
        }

        public async Task<Response> SearchAutoComplete(string key)
        {
            try
            {
                var keyUnSign = Ultility.removeVietnameseSign(key);
                var dateTimeNow = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);
                var result = await (from x in _db.Tour
                              where x.IsDelete == false
                              && x.IsActive == true
                              && x.ApproveStatus == (int)ApproveStatus.Approved
                              && (from s in _db.Schedules
                                  where s.TourId == x.IdTour
                                  && s.EndDate >= dateTimeNow
                                  select s.IdSchedule).Count() > 0
                                    select x).OrderByDescending(x => x.Rating).ToListAsync();

                return Ultility.Responses($"", Enums.TypeCRUD.Success.ToString(),content:result);

            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }
    } 
}
