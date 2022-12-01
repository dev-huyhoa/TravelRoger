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
    public class PaymentRes : IPayment
    {
        private readonly TravelContext _db;
        private Notification message;
        private Response res; 
        private readonly ILog _log;
        public PaymentRes(TravelContext db , ILog log)
        {
            _db = db;
            _log = log;
            message = new Notification();
            res = new Response();
        }


        public string CheckBeforSave(JObject frmData, ref Notification _message, bool isUpdate = false)
        {
            try
            {
                var idPay = PrCommon.GetString("idPayment", frmData);
                if (!String.IsNullOrEmpty(idPay))
                {
                    //   payment.IdPayment = idPay;
                }
                var namePay = PrCommon.GetString("namePayment", frmData);
                if (!String.IsNullOrEmpty(namePay))
                {
                    // payment.IdPayment = namePay;
                }
                var type = PrCommon.GetString("type", frmData);
                if (!String.IsNullOrEmpty(type))
                {
                    // payment.IdPayment = type;
                }
                if (isUpdate)
                {
                    CreatePaymentViewModel updateObj = new CreatePaymentViewModel();
                    updateObj.IdPayment = idPay;
                    updateObj.NamePayment = namePay;
                    updateObj.Type = type;
                    return JsonSerializer.Serialize(updateObj);
                }
                CreatePaymentViewModel createObj = new CreatePaymentViewModel();
                createObj.IdPayment = idPay;
                createObj.NamePayment = namePay;
                createObj.Type = type;
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

        public Response Create(CreatePaymentViewModel input, string emailUser)
        {
            try
            {
                Payment pay = new Payment();
                pay = Mapper.MapCreatePayment(input);
                string jsonContent = JsonSerializer.Serialize(pay);
                _db.Payment.Add(pay);
                _db.SaveChanges();

                bool result = _log.AddLog(content: jsonContent, type: "create", emailCreator: emailUser, classContent: "Payment");
                if (result)
                {
                    return Ultility.Responses("Thêm thành công !", Enums.TypeCRUD.Success.ToString());
                }
                else
                {
                    return Ultility.Responses("Lỗi log!", Enums.TypeCRUD.Error.ToString());
                }

            }
            catch (Exception e)
            {

                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);

            }
        }




        public Response Gets(int pageIndex, int pageSize)
        {
            try
            {
                var queryListPayment = (from x in _db.Payment.AsNoTracking()
                                        select x);
                int totalResult = queryListPayment.Count();
                var list = queryListPayment.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
                var result = Mapper.MapPayment(list);

                res = Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);
                res.TotalResult = totalResult;
                return res;

            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);

            };
        }

        //public Response Update(CreateUpdatePaymentViewModel input)
        //{
        //    try
        //    {
        //        Payment pay = new Payment();
        //        pay = Mapper.MapCreatePayment(input);

        //        _db.Payment.Update(pay);
        //        _db.SaveChanges();

        //        res.Notification.DateTime = DateTime.Now;
        //        res.Notification.Messenge = "Sửa thành công !";
        //        res.Notification.Type = "Success";
        //        return res;
        //    }
        //    catch (Exception e)
        //    {

        //        res.Notification.DateTime = DateTime.Now;
        //        res.Notification.Description = e.Message;
        //        res.Notification.Messenge = "Có lỗi xảy ra !";
        //        res.Notification.Type = "Error";
        //        return res;
        //    }
        //}
    }
}
