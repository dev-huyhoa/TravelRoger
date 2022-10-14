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
using Travel.Shared.ViewModels.Travel.TourBookingVM;

namespace Travel.Data.Repositories
{
    public class TourBookingRes : ITourBooking
    {
        private readonly TravelContext _db;
        private Notification message;
        private Response res;
        public TourBookingRes(TravelContext db)
        {
            _db = db;
            message = new Notification();
            res = new Response();
        }
        public string CheckBeforSave(JObject frmData, ref Notification _message, bool isUpdate)
        {
            try
            {
                var idTourBooking = PrCommon.GetString("idTourBooking", frmData);
                if (!String.IsNullOrEmpty(idTourBooking))
                {
                    //   payment.IdPayment = idPay;
                }
                var nameCustomer = PrCommon.GetString("nameCustomer", frmData);
                if (!String.IsNullOrEmpty(nameCustomer))
                {
                    // payment.IdPayment = namePay;
                }
                var address = PrCommon.GetString("address", frmData);
                if (!String.IsNullOrEmpty(address))
                {
                    // payment.IdPayment = type;
                }
                var email = PrCommon.GetString("email", frmData);
                if (!String.IsNullOrEmpty(email))
                {   }
                var phone = PrCommon.GetString("phone", frmData);
                if (!String.IsNullOrEmpty(phone))
                { }     

                var nameContact = PrCommon.GetString("nameContact", frmData);
                if (!String.IsNullOrEmpty(nameContact))
                { }

                var dateBooking = PrCommon.GetString("dateBooking", frmData);
                if (!String.IsNullOrEmpty(dateBooking))
                { }

                var lastDate = PrCommon.GetString("lastDate", frmData);
                if (!String.IsNullOrEmpty(lastDate))
                { }

                var vat = PrCommon.GetString("vat", frmData);
                if (!String.IsNullOrEmpty(vat))
                { }

                var pincode = PrCommon.GetString("pincode", frmData);
                if (!String.IsNullOrEmpty(pincode))
                { }

                var voucherCode = PrCommon.GetString("voucherCode", frmData);
                if (!String.IsNullOrEmpty(voucherCode))
                { }

                var bookingNo = PrCommon.GetString("bookingNo", frmData);
                if (!String.IsNullOrEmpty(bookingNo))
                { }

                var isCalled = PrCommon.GetString("isCalled", frmData);
                if (!String.IsNullOrEmpty(isCalled))
                { }

                var deposit = PrCommon.GetString("deposit", frmData);
                if (!String.IsNullOrEmpty(deposit))
                { }

                var remainPrice = PrCommon.GetString("remainPrice", frmData);
                if (!String.IsNullOrEmpty(remainPrice))
                { }

                var totalPrice = PrCommon.GetString("deposit", frmData);
                if (!String.IsNullOrEmpty(deposit))
                { }

                var modifyBy = PrCommon.GetString("modifyBy", frmData);
                if (!String.IsNullOrEmpty(modifyBy))
                { }

                var modifyDate = PrCommon.GetString("modifyDate", frmData);
                if (!String.IsNullOrEmpty(modifyDate))
                { }
                if (isUpdate)
                {
                    CreateTourBookingViewModel updateObj = new CreateTourBookingViewModel();
                    updateObj.IdTourBooking = idTourBooking;
                    updateObj.NameCustomer = nameCustomer;
                    updateObj.Address = address;
                    updateObj.Email = email;
                    updateObj.Phone = phone;
                    updateObj.NameContact = nameCustomer;
                    updateObj.DateBooking = Convert.ToInt32(dateBooking); 
                    updateObj.LastDate = Convert.ToInt32(lastDate); 
                    updateObj.Vat = Convert.ToInt32(vat);
                    updateObj.Pincode = pincode;
                    updateObj.VoucherCode = voucherCode;
                    updateObj.BookingNo = bookingNo;
                    updateObj.IsCalled =  Convert.ToBoolean(isCalled);
                    updateObj.Deposit =  Convert.ToInt32(deposit);
                    updateObj.RemainPrice = Convert.ToInt32(remainPrice);
                    updateObj.TotalPrice = Convert.ToInt32(totalPrice);
                    updateObj.ModifyBy = modifyBy;
                    updateObj.ModifyDate =  Convert.ToInt32(modifyDate);
                    return JsonSerializer.Serialize(updateObj);
                }
                CreateTourBookingViewModel createObj = new CreateTourBookingViewModel();
                createObj.IdTourBooking = idTourBooking;
                createObj.NameCustomer = nameCustomer;
                createObj.Address = address;
                createObj.Email = email;
                createObj.Phone = phone;
                createObj.NameContact = nameCustomer;
                createObj.DateBooking = Convert.ToInt32(dateBooking);
                createObj.LastDate = Convert.ToInt32(lastDate);
                createObj.Vat = Convert.ToInt32(vat);
                createObj.Pincode = pincode;
                createObj.VoucherCode = voucherCode;
                createObj.BookingNo = bookingNo;
                createObj.IsCalled = Convert.ToBoolean(isCalled);
                createObj.Deposit = Convert.ToInt32(deposit);
                createObj.RemainPrice = Convert.ToInt32(remainPrice);
                createObj.TotalPrice = Convert.ToInt32(totalPrice);
                createObj.ModifyBy = modifyBy;
                createObj.ModifyDate = Convert.ToInt32(modifyDate);
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

        public Response Create(CreateTourBookingViewModel input)
        {
            try
            {
                Tourbooking tourbooking =
                tourbooking = Mapper.MapCreateTourBooking(input);
                _db.Tourbookings.Add(tourbooking);
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

        public Response Gets()
        {
            try
            {
                var ListTourBooking = _db.Tourbookings.ToList();
                var result = Mapper.MapTourBooking(ListTourBooking);
                if (result.Count() > 0)
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
    }
}
