using Newtonsoft.Json.Linq;
using PrUtility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Context.Models;
using Travel.Context.Models.Travel;
using Travel.Data.Interfaces;
using Travel.Shared.Ultilities;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel;

namespace Travel.Data.Repositories
{
    public class PaymentRes : IPayment
    {
        private readonly TravelContext _db;
        private Notification message;
        private Response res;
        public PaymentRes(TravelContext db)
        {
            _db = db;
            message = new Notification();
            res = new Response();
        }
        public CreateUpdatePaymentViewModel CheckBeforSave(JObject frmData, ref Notification _message)
        {
            CreateUpdatePaymentViewModel payment = new CreateUpdatePaymentViewModel();

            try
            {
                var idPay = PrCommon.GetString("idPayment", frmData);
                if (!String.IsNullOrEmpty(idPay))
                {
                    payment.IdPayment = idPay;
                }
                var namePay = PrCommon.GetString("namePayment", frmData);
                if (!String.IsNullOrEmpty(namePay))
                {
                    payment.IdPayment = namePay;
                }
                var type = PrCommon.GetString("type", frmData);
                if (!String.IsNullOrEmpty(type))
                {
                    payment.IdPayment = type;
                }
                return payment;
            }
            catch (Exception e)
            {
                message.DateTime = DateTime.Now;
                message.Description = e.Message;
                message.Messenge = "Có lỗi xảy ra !";
                message.Type = "Error";

                _message = message;
                return payment;
            }
        }

        public Response Create(CreateUpdatePaymentViewModel input)
        {
            try
            {
                Payment pay = new Payment();
               pay = Mapper.MapCreatePayment(input);

                _db.Payment.Add(pay);
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
                var listPay = _db.Payment.ToList();
                var result = Mapper.MapPayment(listPay);
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

        public Response Update(CreateUpdatePaymentViewModel input)
        {
            try
            {
                Payment pay = new Payment();
                pay = Mapper.MapCreatePayment(input);

                _db.Payment.Update(pay);
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
    }
}
