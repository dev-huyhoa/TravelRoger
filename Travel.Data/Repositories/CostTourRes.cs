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
using Travel.Shared.ViewModels.Travel.CostTourVM;
using Travel.Shared.ViewModels.Travel.TourVM;

namespace Travel.Data.Repositories
{
    public class CostTourRes : ICostTour
    {
        private readonly TravelContext _db;
        private Notification message;
        private Response res;
        public CostTourRes(TravelContext db)
        {
            _db = db;
            message = new Notification();
            res = new Response();
        }
        public string CheckBeforSave(JObject frmData, ref Notification _message, bool isUpdate = false)
        {
            try
            {
                var tourDetailId = PrCommon.GetString("tourDetailId", frmData);
                if (String.IsNullOrEmpty(tourDetailId))
                {
                }
                var hotelId = PrCommon.GetString("hotelId", frmData);
                if (String.IsNullOrEmpty(hotelId))
                {
                }
                var restaurantId = PrCommon.GetString("restaurantId", frmData);
                if (String.IsNullOrEmpty(restaurantId))
                {
                }
                var placeId = PrCommon.GetString("placeId", frmData);
                if (String.IsNullOrEmpty(placeId))
                {
                }

                var breakfast = PrCommon.GetString("breakfast", frmData);
                if (!breakfast.IsNumber())
                {
                }
                var water = PrCommon.GetString("water", frmData);
                if (!water.IsNumber())
                {

                }
                var feeGas = PrCommon.GetString("feeGas", frmData);
                if (!feeGas.IsNumber())
                {
                }
                var distance = PrCommon.GetString("distance", frmData);
                if (!distance.IsNumber())
                {
                }
                var sellCost = PrCommon.GetString("sellCost", frmData);
                if (!sellCost.IsNumber())
                {
                }
                var depreciation = PrCommon.GetString("depreciation", frmData);
                if (!depreciation.IsNumber())
                {
                }
                var otherPrice = PrCommon.GetString("otherPrice", frmData);
                if (!otherPrice.IsNumber())
                {
                }
                var tolls = PrCommon.GetString("tolls", frmData);
                if (!tolls.IsNumber())
                {
                }
                var cusExpected = PrCommon.GetString("cusExpected", frmData);
                if (!cusExpected.IsNumber())
                {
                }
                var insuranceFee = PrCommon.GetString("insuranceFee", frmData);
                if (!insuranceFee.IsNumber())
                {
                }
                var isHoliday = PrCommon.GetString("isHoliday", frmData) ?? "false";
                if (isUpdate)
                {
                    // map data
                    UpdateCostViewModel objUpdate = new UpdateCostViewModel();

                    objUpdate.TourDetailId = tourDetailId;
                    objUpdate.Breakfast = float.Parse(breakfast);
                    objUpdate.Water = float.Parse(water);
                    objUpdate.FeeGas = float.Parse(feeGas);
                    objUpdate.Distance = float.Parse(distance);
                    objUpdate.SellCost = float.Parse(sellCost);
                    objUpdate.Depreciation = float.Parse(depreciation);
                    objUpdate.OtherPrice = float.Parse(otherPrice);
                    objUpdate.Tolls = float.Parse(tolls);
                    objUpdate.CusExpected = Convert.ToInt16(cusExpected);
                    objUpdate.InsuranceFee = float.Parse(insuranceFee);
                    objUpdate.IsHoliday = bool.Parse(isHoliday);
                    return JsonSerializer.Serialize(objUpdate);
                }
                // map data
                CreateCostViewModel obj = new CreateCostViewModel();

                obj.TourDetailId = tourDetailId;
                obj.Breakfast = float.Parse(breakfast);
                obj.Water = float.Parse(water);
                obj.FeeGas = float.Parse(feeGas);
                obj.Distance = float.Parse(distance);
                obj.SellCost = float.Parse(sellCost);
                obj.Depreciation = float.Parse(depreciation);
                obj.OtherPrice = float.Parse(otherPrice);
                obj.Tolls = float.Parse(tolls);
                obj.CusExpected = Convert.ToInt16(cusExpected);
                obj.InsuranceFee = float.Parse(insuranceFee);
                obj.IsHoliday = bool.Parse(isHoliday);
                obj.HotelId = Guid.Parse(hotelId);
                obj.RestaurantId = Guid.Parse(restaurantId);
                obj.PlaceId = Guid.Parse(placeId);

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

        public Response Create(CreateCostViewModel input)
        {
            try
            {
                CostTour cost =
                cost = Mapper.MapCreateCost(input);
                _db.CostTours.Add(cost);
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

        public Response Get()
        {
            throw new NotImplementedException();
        }
    }
}
