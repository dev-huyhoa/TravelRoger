﻿using Newtonsoft.Json.Linq;
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
            CreatePromotionViewModel promotion = new CreatePromotionViewModel();
            try
            {

                //var tourName = PrCommon.GetString("nameTour", frmData);
                //if (String.IsNullOrEmpty(tourName))
                //{
                //}
                //if (isExistName(tourName))
                //{
                //}
                var idPromotion = PrCommon.GetString("idPromotion", frmData);
                if (!String.IsNullOrEmpty(idPromotion))
                {
                    
                }
               
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

             
                obj.Value = Convert.ToInt16(value);
                obj.ToDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Parse(toDate));
                obj.FromDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Parse(fromDate));
                obj.IdPromotion = Convert.ToInt16(idPromotion); 

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

        public Response Gets()
        {
            try
            {
                var list = (from x in _db.Promotions where x.Approve == Convert.ToInt16(ApproveStatus.Approved) select x).ToList();
                var result = Mapper.MapPromotion(list);
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
            }
        }

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

        public Response GetWaitingPromotion()
        {
            try
            {
                var listWaiting = (from x in _db.Promotions where x.Approve == Convert.ToInt16(ApproveStatus.Waiting) select x).ToList();
                var result = Mapper.MapPromotion(listWaiting);
                if (listWaiting.Count() > 0)
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
