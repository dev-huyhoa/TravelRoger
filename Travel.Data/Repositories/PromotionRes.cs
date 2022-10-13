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
    public class PromotionRes : IPromotions
    {
        private readonly TravelContext _db;
        private Notification message;
        private Response res;
        public PromotionRes(TravelContext db)
        {
            _db = db;
            message = new Notification();
            res = new Response();
        }

        public string CheckBeforSave(JObject frmData, ref Notification _message, bool isUpdate)
        {
            try
            {

                //var tourName = PrCommon.GetString("nameTour", frmData);
                //if (String.IsNullOrEmpty(tourName))
                //{
                //}
                //if (isExistName(tourName))
                //{
                //}
                var value = PrCommon.GetString("value", frmData);
                if (String.IsNullOrEmpty(value))
                {
                }
                var toDate = PrCommon.GetString("toDate", frmData);
                if (String.IsNullOrEmpty(toDate))
                {

                }
                var fromDate = PrCommon.GetString("fromDate", frmData);
                if (String.IsNullOrEmpty(fromDate))
                {
                }

                //var vat = PrCommon.GetString("vat", frmData) ?? "0";
                //if (isUpdate)
                //{
                //    // map data
                //    UpdatePromotionViewModel objUpdate = new UpdatePromotionViewModel();
                //    //objUpdate.NameTour = "tentoatuspa";
                //    objUpdate.Value = value;
                //    objUpdate.ToDate = toDate;
                //    objUpdate.FromDate = fromDate;
                //    //objUpdate.Description = description;
                //    //objUpdate.VAT = Convert.ToInt16(vat);
                //    // generate ID
                //    objUpdate.IdPromotion = Ultility.GenerateId(promotion);
                //    return JsonSerializer.Serialize(objUpdate);
                //}
                //// map data
                CreatePromotionViewModel obj = new CreatePromotionViewModel();

                ////obj.NameTour = tourName;
                //obj.Value = value;
                //obj.ToDate = toDate;
                //obj.FromDate = fromDate;
                ////obj.Description = description;
                ////obj.VAT = Convert.ToInt16(vat);
                //// generate ID
                //obj.IdPromotion = Ultility.GenerateId(promotion);

                return JsonSerializer.Serialize(obj);
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

        //public Response Gets()
        //{
        //    try
        //    {
        //        var listPromotion = (from x in _db.Promotions select x).ToList();
        //        var result = Mapper.MapPromotion(listPromotion);
        //        if (result.Count() > 0)
        //        {
        //            res.Content = result;
        //        }
        //        else
        //        {
        //            res.Notification.DateTime = DateTime.Now;
        //            res.Notification.Messenge = "Không có dữ liệu trả về !";
        //            res.Notification.Type = "Warning";
        //        }
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

        public Response Create(CreatePromotionViewModel input)
        {
            try
            {
                Promotion promotion = new Promotion();
                promotion = Mapper.MapCreatePromotion(input);
                _db.Promotions.Add(promotion);
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

        //private bool isExistName(string input)
        //{
        //    try
        //    {
        //        var tour =
        //            (from x in _db.Promotions
        //             where x.Value.ToLower() == input.ToLower()
        //             select x).FirstOrDefault();
        //        if (tour != null)
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch (Exception e)
        //    {
        //        return false;
        //    }
        //}
    }
}
