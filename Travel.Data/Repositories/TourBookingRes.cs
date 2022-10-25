using Microsoft.EntityFrameworkCore;
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

namespace Travel.Data.Repositories
{
    public class TourBookingRes : ITourBooking
    {
        private readonly TravelContext _db;
        private readonly ISchedule _schedule;
        private Notification message;
        private Response res;
        public TourBookingRes(TravelContext db,
            ISchedule schedule)
        {
            _db = db;
            message = new Notification();
            res = new Response();


            _schedule = schedule;
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
                createObj.Pincode = pincode;
                createObj.BookingDetails = createDetailObj;
                createObj.CustomerId = customerId;
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

        public async Task<Response> Create(CreateTourBookingViewModel input)
        {
            using var transaction = _db.Database.BeginTransaction();

            try
            {
                await transaction.CreateSavepointAsync("BeforeSave");
                Tourbooking tourbooking =
                    tourbooking = Mapper.MapCreateTourBooking(input);
                TourbookingDetails tourBookingDetail = Mapper.MapCreateTourBookingDetail(input.BookingDetails);
                tourbooking.TourbookingDetails = tourBookingDetail;
                _db.Tourbookings.Add(tourbooking);
                await _db.SaveChangesAsync();
                var payment = await (from x in _db.Payment where x.IdPayment == input.PaymentId select x).FirstAsync();
                tourbooking.Payment = payment;

                // cập nhật số lượng
                int quantityAdult = tourbooking.TourbookingDetails.Adult;
                int quantityChild = tourbooking.TourbookingDetails.Child;
                int quantityBaby = tourbooking.TourbookingDetails.Baby;
                await _schedule.UpdateCapacity(input.ScheduleId, quantityAdult, quantityChild, quantityBaby);
                transaction.Commit();
                transaction.Dispose();


                res.Content = tourbooking.IdTourbooking;
                res.Notification.DateTime = DateTime.Now;
                res.Notification.Messenge = "Đặt tour thành công !";
                res.Notification.Type = "Success";
                return res;
            }
            catch (Exception e)
            {
                transaction.RollbackToSavepoint("BeforeSave");

                res.Notification.DateTime = DateTime.Now;
                res.Notification.Description = e.Message;
                res.Notification.Messenge = "Có lỗi xảy ra !";
                res.Notification.Type = "Error";
                return res;
            }
        }

        public Response Gets()
        {
            try
            {
                var ListTourBooking = _db.Tourbookings.ToList();
                var result = Mapper.MapTourBooking(ListTourBooking);
                if (ListTourBooking.Count() > 0)
                {
                    res.Content = result;
                }
                else
                {
                    res.Notification.DateTime = DateTime.Now;
                    res.Notification.Messenge = "Không có dữ liệu trả về !";
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
            };
        }

        public async Task<Response> TourBookingById(string idTourbooking)
        {
            try
            {
                var tourbooking = await (from x in _db.Tourbookings
                                         where x.IdTourbooking == idTourbooking
                                         select new Tourbooking
                                         {
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
                                             Payment = (from p in _db.Payment
                                                        where p.IdPayment == x.PaymentId
                                                        select p).First(),
                                             TourbookingDetails = (from tbd in _db.tourbookingDetails
                                                                   where tbd.IdTourbookingDetails == x.IdTourbooking
                                                                   select tbd).First(),
                                             Schedule = (from s in _db.Schedules
                                                         where s.IdSchedule == x.ScheduleId
                                                         select s).First()
                                         }).FirstAsync();
                if (tourbooking  != null )
                {
                    res.Content = tourbooking;
                }
                else
                {
                    res.Notification.DateTime = DateTime.Now;
                    res.Notification.Messenge = "Không có dữ liệu trả về !";
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
            };
        }
    }
}
