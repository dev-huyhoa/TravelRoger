﻿using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using PrUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Transactions;
using Travel.Context.Models;
using Travel.Context.Models.Travel;
using Travel.Data.Interfaces;
using Travel.Shared.Ultilities;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel.TourBookingVM;
using Microsoft.Extensions.Configuration;
using Travel.Shared.SpeedSMSAPI;

namespace Travel.Data.Repositories
{
    public class TourBookingRes : ITourBooking
    {
        private readonly TravelContext _db;
        private readonly ISchedule _schedule;
        private readonly string keySecurity;
        private readonly IConfiguration _config;
        private void UpdateDatabase<T>(T input)
        {
            _db.Entry(input).State = EntityState.Modified;
        }
        private void DeleteDatabase<T>(T input)
        {
            _db.Entry(input).State = EntityState.Deleted;
        }
        private void CreateDatabase<T>(T input)
        {
            _db.Entry(input).State = EntityState.Added;
        }
        private async Task SaveChangeAsync()
        {
            await _db.SaveChangesAsync();
        }
        private void SaveChange()
        {
            _db.SaveChanges();
        }
        public TourBookingRes(TravelContext db,
            ISchedule schedule,
            IConfiguration config)
        {
            _db = db;
            _schedule = schedule;
            _config = config;
            keySecurity = _config["keySecurity"];
        }
        public string CheckBeforSave(JObject frmData, ref Notification _message, bool isUpdate)
        {
            try
            {
                var idTourBooking = PrCommon.GetString("idTourBooking", frmData);
                if (String.IsNullOrEmpty(idTourBooking))
                {
                    //   payment.IdPayment = idPay;
                }
                var customerId = Guid.Empty;
                var stringIdCustomer = PrCommon.GetString("customerId", frmData);
                if (!String.IsNullOrEmpty(stringIdCustomer))
                {
                    customerId = Guid.Parse(stringIdCustomer);
                }

                var baby = PrCommon.GetString("baby", frmData);
                if (String.IsNullOrEmpty(baby))
                {
                    //   payment.IdPayment = idPay;
                }

                var child = PrCommon.GetString("child", frmData);
                if (String.IsNullOrEmpty(child))
                {
                    //   payment.IdPayment = idPay;
                }

                var adult = PrCommon.GetString("adult", frmData);
                if (String.IsNullOrEmpty(adult))
                {
                    //   payment.IdPayment = idPay;
                }

                var status = PrCommon.GetString("status", frmData);
                if (String.IsNullOrEmpty(status))
                {
                    //   payment.IdPayment = idPay;
                }
                var hotelId = PrCommon.GetString("hotelId", frmData);
                if (String.IsNullOrEmpty(hotelId))
                {
                    //   payment.IdPayment = idPay;
                }
                var restaurantId = PrCommon.GetString("restaurantId", frmData);
                if (String.IsNullOrEmpty(restaurantId))
                {
                    //   payment.IdPayment = idPay;
                }
                var placeId = PrCommon.GetString("placeId", frmData);
                if (String.IsNullOrEmpty(placeId))
                {
                    //   payment.IdPayment = idPay;
                }

                var scheduleId = PrCommon.GetString("scheduleId", frmData);
                if (String.IsNullOrEmpty(scheduleId))
                {
                    //   payment.IdPayment = idPay;
                }
                var paymentId = PrCommon.GetString("paymentId", frmData);
                if (String.IsNullOrEmpty(paymentId))
                {
                    //   payment.IdPayment = idPay;
                }
                var nameCustomer = PrCommon.GetString("nameCustomer", frmData);
                if (String.IsNullOrEmpty(nameCustomer))
                {
                    // payment.IdPayment = namePay;
                }
                var address = PrCommon.GetString("address", frmData);
                if (String.IsNullOrEmpty(address))
                {
                    // payment.IdPayment = type;
                }
                var email = PrCommon.GetString("email", frmData);
                if (String.IsNullOrEmpty(email))
                { }
                var phone = PrCommon.GetString("phone", frmData);
                if (String.IsNullOrEmpty(phone))
                { }

                var nameContact = PrCommon.GetString("nameContact", frmData);
                if (String.IsNullOrEmpty(nameContact))
                { }
                var vat = PrCommon.GetString("vat", frmData);
                if (String.IsNullOrEmpty(vat))
                { }
                var pincode = PrCommon.GetString("pincode", frmData);
                if (String.IsNullOrEmpty(pincode))
                { }

                var totalPrice = PrCommon.GetString("totalPrice", frmData);
                if (String.IsNullOrEmpty(totalPrice))
                { }
                var valuePromotion = PrCommon.GetString("valuePromotion", frmData);
                if (isUpdate)
                {
                    CreateTourBookingViewModel updateObj = new CreateTourBookingViewModel();
                    updateObj.IdTourBooking = idTourBooking;
                    updateObj.NameCustomer = nameCustomer;
                    updateObj.Address = address;
                    updateObj.Email = email;
                    updateObj.Phone = phone;
                    updateObj.NameContact = nameContact;
                    updateObj.Vat = Convert.ToInt16(vat);
                    updateObj.Pincode = pincode;
                    return JsonSerializer.Serialize(updateObj);
                }
                CreateBookingDetailViewModel createDetailObj = new CreateBookingDetailViewModel();
                createDetailObj.Baby = Convert.ToInt16(baby);
                createDetailObj.Child = Convert.ToInt16(child);
                createDetailObj.Adult = Convert.ToInt16(adult);
                createDetailObj.Status = (Enums.StatusBooking)(Convert.ToInt16(status));
                createDetailObj.HotelId = Guid.Parse(hotelId);
                createDetailObj.RestaurantId = Guid.Parse(restaurantId);
                createDetailObj.PlaceId = Guid.Parse(placeId);

                CreateTourBookingViewModel createObj = new CreateTourBookingViewModel();
                createObj.ScheduleId = scheduleId;
                createObj.PaymentId = Convert.ToInt16(paymentId);
                createObj.NameCustomer = nameCustomer;
                createObj.Address = address;
                createObj.Email = email;
                createObj.Phone = phone;
                createObj.NameContact = nameContact;
                createObj.Vat = Convert.ToInt16(vat);
                createObj.TotalPrice = float.Parse(totalPrice);
                createObj.Pincode = $"PIN{Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now)}";
                createObj.BookingDetails = createDetailObj;
                createObj.CustomerId = customerId;
                if (!string.IsNullOrEmpty(valuePromotion))
                {
                    createObj.ValuePromotion = Convert.ToInt16(valuePromotion);

                }
                return JsonSerializer.Serialize(createObj);
            }
            catch (Exception e)
            {
                _message = Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message).Notification;
                return null;
            }
        }

        public async Task<Response> Create(CreateTourBookingViewModel input)
        {
            using var transaction = _db.Database.BeginTransaction();

            try
            {
                await transaction.CreateSavepointAsync("BeforeSave");
                TourBooking tourbooking = Mapper.MapCreateTourBooking(input);
                TourBookingDetails tourBookingDetail = Mapper.MapCreateTourBookingDetail(input.BookingDetails);
                tourbooking.TourBookingDetails = tourBookingDetail;
                CreateDatabase<TourBooking>(tourbooking);
                CreateDatabase<TourBookingDetails>(tourBookingDetail);
                await SaveChangeAsync();

                // cập nhật số lượng
                int quantityAdult = tourbooking.TourBookingDetails.Adult;
                int quantityChild = tourbooking.TourBookingDetails.Child;
                int quantityBaby = tourbooking.TourBookingDetails.Baby;
                await _schedule.UpdateCapacity(input.ScheduleId, quantityAdult, quantityChild, quantityBaby);
                transaction.Commit();
                transaction.Dispose();

                //Gửi sms
                //SpeedSMSAPI api = new SpeedSMSAPI("eHTE2iExhWKHCRk4OvTVT2gFHuPl4wDd");
                //String[] phones = new String[] { tourbooking.Phone };
                //String str = "Lụm";
                //String response = api.sendSMS(phones, str, 5, "d675521d17749e04");

                #region send mail
                Ultility.sendEmail("",input.Email,"Thông báo booking","Bạn vừa đặt tour !", keySecurity);
                #endregion
                return Ultility.Responses("Đặt tour thành công !", Enums.TypeCRUD.Success.ToString(), tourbooking.IdTourBooking);
            }
            catch (Exception e)
            {
                transaction.RollbackToSavepoint("BeforeSave");
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
                ;
            }
        }

        public async Task<Response> Gets()
        {
            try
            {
                var ListTourBooking = await (from x in _db.TourBookings.AsNoTracking()
                                             orderby x.DateBooking descending
                                             select new TourBooking
                                            {
                                                IdTourBooking = x.IdTourBooking,
                                                LastDate = x.LastDate,
                                                NameCustomer = x.NameCustomer,
                                                NameContact = x.NameContact,
                                                Pincode = x.Pincode,
                                                Email = x.Email,
                                                Phone = x.Phone,
                                                Status = x.Status,
                                                Address = x.Address,
                                                AdditionalPrice = x.AdditionalPrice,
                                                BookingNo = x.BookingNo,
                                                IsCalled = x.IsCalled,
                                                DateBooking = x.DateBooking,
                                                TotalPrice = x.TotalPrice,
                                                TotalPricePromotion = x.TotalPricePromotion,
                                                VoucherCode = x.VoucherCode,
                                                ValuePromotion = x.ValuePromotion,
                                                Payment = (from p in _db.Payment.AsNoTracking()
                                                           where p.IdPayment == x.PaymentId
                                                           select p).FirstOrDefault(),
                                                TourBookingDetails = (from tbd in _db.tourBookingDetails.AsNoTracking()
                                                                      where tbd.IdTourBookingDetails == x.IdTourBooking
                                                                      select tbd).FirstOrDefault(),
                                                Schedule = (from s in _db.Schedules.AsNoTracking()
                                                            where s.IdSchedule == x.ScheduleId
                                                            select new Schedule
                                                            {
                                                                DepartureDate = s.DepartureDate,
                                                                ReturnDate = s.ReturnDate,
                                                                DeparturePlace = s.DeparturePlace,
                                                                Description = s.Description,
                                                                QuantityCustomer = s.QuantityCustomer,
                                                                IdSchedule = s.IdSchedule,
                                                                Tour = (from t in _db.Tour.AsNoTracking()
                                                                        where t.IdTour == s.TourId
                                                                        select t).FirstOrDefault(),

                                                            }).FirstOrDefault()
                                            }).ToListAsync();
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), ListTourBooking);

            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }

        public Response GetTourBookingFromDateToDate(DateTime? fromDateInput, DateTime? toDateInput)
        {

            try
            {
                // khai báo
                long fromDate = 0;
                long toDate = 0;

                // gán dữ liệu
                if (fromDateInput != null)
                {
                    fromDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(fromDateInput.Value); // nếu ko bị null thì gán dữ liệu vào
                }
                if (toDateInput != null)
                {
                    toDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(toDateInput.Value); // nếu ko bị null thì gán dữ liệu vào
                }
                else
                {
                    toDate = long.MaxValue; // nếu toDate ko gán thì cho nó dữ liệu max, để ngày nào nó cũng lấy 
                }

                var list = (from x in _db.TourBookings
                            where x.DateBooking >= fromDate
                            && x.DateBooking <= toDate
                            select x).OrderByDescending(x => x.DateBooking).ToList();
                var result = Mapper.MapTourBooking(list);
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }
        public async Task<Response> TourBookingById(string idTourbooking)
        {
            try
            {
                var tourbooking = await (from x in _db.TourBookings.AsNoTracking()
                                         where x.IdTourBooking == idTourbooking
                                         orderby x.DateBooking descending
                                         select new TourBooking
                                         {
                                             IdTourBooking = x.IdTourBooking,
                                             LastDate = x.LastDate,
                                             NameCustomer = x.NameCustomer,
                                             NameContact = x.NameContact,
                                             Pincode = x.Pincode,
                                            ScheduleId = x.ScheduleId,
                                             Email = x.Email,
                                             Phone = x.Phone,
                                             Status = x.Status,
                                             Address = x.Address,
                                             AdditionalPrice = x.AdditionalPrice,
                                             BookingNo = x.BookingNo,
                                             DateBooking = x.DateBooking,
                                             TotalPrice = x.TotalPrice,
                                             TotalPricePromotion = x.TotalPricePromotion,
                                             VoucherCode = x.VoucherCode,
                                             ValuePromotion = x.ValuePromotion,
                                             Payment = (from p in _db.Payment.AsNoTracking()
                                                        where p.IdPayment == x.PaymentId
                                                        select p).FirstOrDefault(),
                                             TourBookingDetails = (from tbd in _db.tourBookingDetails
                                                                   where tbd.IdTourBookingDetails == x.IdTourBooking
                                                                   select tbd).FirstOrDefault(),
                                             Schedule = (from s in _db.Schedules
                                                         where s.IdSchedule == x.ScheduleId
                                                         select new Schedule
                                                         {
                                                             DepartureDate = s.DepartureDate,
                                                             ReturnDate = s.ReturnDate,
                                                             DeparturePlace = s.DeparturePlace,
                                                             Description = s.Description,
                                                             QuantityCustomer = s.QuantityCustomer,
                                                             IdSchedule = s.IdSchedule,
                                                             Tour = (from t in _db.Tour
                                                                     where t.IdTour == s.TourId
                                                                     select t).FirstOrDefault(),

                                                         }).FirstOrDefault()
                                         }).FirstOrDefaultAsync();
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), tourbooking);

            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }
        public Response DoPayment(string idTourBooking) // for admin if customer payment
        {
            try
            {
                var tourbooking = (from tb in _db.TourBookings.AsNoTracking()
                                   where tb.IdTourBooking == idTourBooking
                                   && tb.Status == (int)Enums.StatusBooking.Paying
                                   select tb).FirstOrDefault();
                if (tourbooking != null)
                {
                    var bookingNo = $"{tourbooking.IdTourBooking}NO";
                    tourbooking.Status = (int)Enums.StatusBooking.Paid;
                    tourbooking.BookingNo = bookingNo;
                    UpdateDatabase<TourBooking>(tourbooking);
                    SaveChange();
                    #region sendMail

                    var emailSend = _config["emailSend"];
                    var keySecurity = _config["keySecurity"];
                    var stringHtml = Ultility.getHtml($"{bookingNo} <br> Vui lòng ghi nhớ mã BookingNo này", "Thanh toán thành công", "BookingNo");

                    Ultility.sendEmail(stringHtml, tourbooking.Email, "Thanh toán dịch vụ", emailSend, keySecurity);
                    #endregion
                    return Ultility.Responses("Thanh toán thành công !", Enums.TypeCRUD.Success.ToString());

                }
                else
                {
                    return Ultility.Responses("Không tìm thấy dữ liệu !", Enums.TypeCRUD.Warning.ToString(), null);

                }
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }

        public async Task<Response> CancelBooking(string idTourBooking)
        {
            try
            {
                var tourbooking = await (from tb in _db.TourBookings.AsNoTracking()
                                         where tb.IdTourBooking == idTourBooking
                                         && tb.Status == (int)Enums.StatusBooking.Paying
                                         select tb).FirstOrDefaultAsync();
                if (tourbooking != null)
                {
                    tourbooking.Status = (int)Enums.StatusBooking.Cancel;
                    UpdateDatabase<TourBooking>(tourbooking);
                    SaveChange();
                    return Ultility.Responses("Đã hủy booking !", Enums.TypeCRUD.Success.ToString());
                }
                else
                {
                    return Ultility.Responses("Hủy booking thất bại !", Enums.TypeCRUD.Error.ToString());
                }

            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }
        public async Task<Response> RestoreBooking(string idTourBooking)
        {
            try
            {
                var tourbooking = await (from tb in _db.TourBookings.AsNoTracking()
                                         where tb.IdTourBooking == idTourBooking
                                         && tb.Status == (int)Enums.StatusBooking.Cancel
                                         select tb).FirstOrDefaultAsync();
                if (tourbooking != null)
                {
                    tourbooking.Status = (int)Enums.StatusBooking.Paying;
                    UpdateDatabase<TourBooking>(tourbooking);
                    SaveChange();
                    //#region sendMail

                    //var emailSend = _config["emailSend"];
                    //var keySecurity = _config["keySecurity"];
                    //var stringHtml = Ultility.getHtml($"{bookingNo} <br> Vui lòng ghi nhớ mã BookingNo này", "Thanh toán thành công", "BookingNo");

                    //Ultility.sendEmail(stringHtml, tourbooking.Email, "Thanh toán dịch vụ", emailSend, keySecurity);
                    //#endregion
                    return Ultility.Responses("Đã hủy booking !", Enums.TypeCRUD.Success.ToString());
                }
                else
                {
                    return Ultility.Responses("Hủy booking thất bại !", Enums.TypeCRUD.Error.ToString());
                }

            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }

        public async Task<Response> TourBookingByBookingNo(string bookingNo)
        {
            try
            {
                var tourbooking = await (from x in _db.TourBookings.AsNoTracking()
                                         where x.BookingNo == bookingNo
                                         select new TourBooking
                                         {
                                             LastDate = x.LastDate,
                                             NameCustomer = x.NameCustomer,
                                             NameContact = x.NameContact,
                                             Pincode = x.Pincode,
                                             Email = x.Email,
                                             Phone = x.Phone,
                                             Address = x.Address,
                                             AdditionalPrice = x.AdditionalPrice,
                                             BookingNo = x.BookingNo,
                                             DateBooking = x.DateBooking,
                                             TotalPrice = x.TotalPrice,
                                             TotalPricePromotion = x.TotalPricePromotion,
                                             VoucherCode = x.VoucherCode,
                                             ValuePromotion = x.ValuePromotion,
                                             Payment = (from p in _db.Payment.AsNoTracking()
                                                        where p.IdPayment == x.PaymentId
                                                        select p).FirstOrDefault(),
                                             TourBookingDetails = (from tbd in _db.tourBookingDetails.AsNoTracking()
                                                                   where tbd.IdTourBookingDetails == x.IdTourBooking
                                                                   select tbd).FirstOrDefault(),
                                             Schedule = (from s in _db.Schedules.AsNoTracking()
                                                         where s.IdSchedule == x.ScheduleId
                                                         select new Schedule
                                                         {
                                                             DepartureDate = s.DepartureDate,
                                                             ReturnDate = s.ReturnDate,
                                                             DeparturePlace = s.DeparturePlace,
                                                             Description = s.Description,
                                                             QuantityCustomer = s.QuantityCustomer,
                                                             IdSchedule = s.IdSchedule,
                                                             Tour = (from t in _db.Tour
                                                                     where t.IdTour == s.TourId
                                                                     select t).FirstOrDefault(),

                                                         }).FirstOrDefault()
                                         }).FirstOrDefaultAsync();
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), tourbooking);

            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }

        public Response StatisticTourBooking()
        {
            try
            {  // Đã đặt tour nhưng chưa thanh toán
                var lsTourBookingPaying = (from x in _db.TourBookings.AsNoTracking()
                                           where x.Status == (int)Enums.StatusBooking.Paying
                                           select x).Count();
                // tour đã thanh toán hết  
                var lsTourBookingPaid = (from x in _db.TourBookings.AsNoTracking()
                                         where x.Status == (int)Enums.StatusBooking.Paid
                                         select x).Count();
                // tourr đã hủy
                var lsTourBookingCancel = (from x in _db.TourBookings.AsNoTracking()
                                           where x.Status == (int)Enums.StatusBooking.Cancel
                                           select x).Count();
                var ab = String.Format("tourPaying: {0} && tourPaid: {1} && tourCancel: {2}", lsTourBookingPaying, lsTourBookingPaid, lsTourBookingCancel);
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), ab);
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);

              
            }
        }

        public Response CheckCalled(string idTourBooking)
        {
            try
            {
                var tourbooking = (from tb in _db.TourBookings.AsNoTracking()
                                   where tb.IdTourBooking == idTourBooking
                                   select tb).FirstOrDefault();
                if (tourbooking != null)
                {
                    tourbooking.IsCalled = true;
                    UpdateDatabase<TourBooking>(tourbooking);
                    SaveChange();

                    //#region sendMail

                    //var emailSend = _config["emailSend"];
                    //var keySecurity = _config["keySecurity"];
                    //var stringHtml = Ultility.getHtml($"{bookingNo} <br> Vui lòng ghi nhớ mã BookingNo này", "Thanh toán thành công", "BookingNo");

                    //Ultility.sendEmail(stringHtml, tourbooking.Email, "Thanh toán dịch vụ", emailSend, keySecurity);
                    //#endregion
                    return Ultility.Responses("Đã gọi !", Enums.TypeCRUD.Success.ToString());
                }
                else
                {
                    return Ultility.Responses("Gọi thất bại !", Enums.TypeCRUD.Error.ToString());
                }

            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }

        private void UpdateDatabase(TourBooking tourbooking)
        {
            _db.Entry(tourbooking).State = EntityState.Modified;
            _db.SaveChanges();
        }
        private void DeleteDatabase(TourBooking tourbooking)
        {
            _db.Entry(tourbooking).State = EntityState.Deleted;
            _db.SaveChanges();
        }
        private void CreateDatabase(TourBooking tourbooking)
        {
            _db.TourBookings.Add(tourbooking);
            _db.SaveChanges();
        }

        public Response SearchTourBooking(JObject frmData)
        {
            try
            {
                Keywords keywords = new Keywords();
           
                var kwId = PrCommon.GetString("IdTourBooking", frmData).Trim();
                if (!String.IsNullOrEmpty(kwId))
                {
                    keywords.KwId = kwId.Trim().ToLower();
                }
                else
                {
                    keywords.KwId = "";

                }
                var kwPincode = PrCommon.GetString("Pincode", frmData).Trim();
                if (!String.IsNullOrEmpty(kwPincode))
                {
                    keywords.KwPincode = kwPincode.Trim().ToLower();
                }
                else
                {
                    keywords.KwPincode = "";

                }
                var kwEmail = PrCommon.GetString("Email", frmData).Trim();
                if (!String.IsNullOrEmpty(kwEmail))
                {
                    keywords.KwEmail = kwEmail.Trim().ToLower();
                }
                else
                {
                    keywords.KwEmail = "";

                }
                var kwPhone = PrCommon.GetString("phone", frmData).Trim();
                if (!String.IsNullOrEmpty(kwPhone))
                {
                    keywords.KwPhone = kwPhone.Trim().ToLower();
                }
                else
                {
                    keywords.KwPhone = "";

                }
                var kwDate = PrCommon.GetString("DateBooking", frmData).Trim();
                if (!String.IsNullOrEmpty(kwDate))
                {
                    keywords.KwDate = long.Parse(kwDate);
                }
                else
                {

                    keywords.KwDate = 0;
                }
                var kwIsCall = PrCommon.GetString("IsCalled", frmData);

                if (!String.IsNullOrEmpty(kwIsCall))
                {
                    keywords.kwIsCalled = Boolean.Parse(kwIsCall);
                }
                var listTourBooking = new List<TourBooking>();

                if (!string.IsNullOrEmpty(kwIsCall))
                {
                  
                    listTourBooking = (from x in _db.TourBookings
                                       where
                                                       x.IdTourBooking.ToLower().Contains(keywords.KwId) &&
                                                       x.Pincode.ToLower().Contains(keywords.KwPincode) &&
                                                       x.Phone.ToLower().Contains(keywords.KwPhone) &&
                                                           x.Email.ToLower().Contains(keywords.KwEmail) &&
                                                            x.IsCalled == keywords.kwIsCalled
                                       orderby x.DateBooking
                                       select x).ToList();

                }
                else
                {
                    listTourBooking = (from x in _db.TourBookings
                                       where
                                                       x.IdTourBooking.ToLower().Contains(keywords.KwId) &&
                                                       x.Pincode.ToLower().Contains(keywords.KwPincode) &&
                                                       x.Phone.ToLower().Contains(keywords.KwPhone) &&
                                                           x.Email.ToLower().Contains(keywords.KwEmail) &&
                                                          x.IsCalled == keywords.kwIsCalled
                                       orderby x.DateBooking
                                       select x).ToList();
                }



                var result = Mapper.MapTourBooking(listTourBooking);
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }

        public Response UpdateStatus(string pincode)
        {
            try
            {
                var tourBooking = (from x in _db.TourBookings.AsNoTracking()
                            where x.Pincode == pincode
                            select x).FirstOrDefault();
               
                if (tourBooking != null )
                {
                    if(tourBooking.Status == 1)
                    {
                        tourBooking.Status = (int)Enums.StatusBooking.Paid;
                        UpdateDatabase(tourBooking);
                    }
                    else
                    {
                        return Ultility.Responses($"Không tìm thấy !", Enums.TypeCRUD.Warning.ToString());
                    }
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
    }
}
