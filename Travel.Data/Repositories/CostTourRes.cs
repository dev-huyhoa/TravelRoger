using Microsoft.Extensions.Configuration;
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
using Travel.Shared.ViewModels.Travel.CostTourVM;
using Travel.Shared.ViewModels.Travel.TourVM;

namespace Travel.Data.Repositories
{
    public class CostTourRes : ICostTour
    {
        private readonly TravelContext _db;
        private Notification message;
        private Response res;
        private IConfiguration _config;
        public CostTourRes(TravelContext db, IConfiguration config)
        {
            _db = db;
            message = new Notification();
            res = new Response();

            _config = config;
        }
        public string CheckBeforSave(JObject frmData, ref Notification _message, bool isUpdate = false)
        {
            try
            {
                var idSchedule = PrCommon.GetString("idSchedule", frmData);
                if (String.IsNullOrEmpty(idSchedule))
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
                var isHoliday = PrCommon.GetString("isHoliday", frmData);
                if (String.IsNullOrEmpty(isHoliday))
                {
                }
                if (isUpdate)
                {
                    // map data
                    UpdateCostViewModel objUpdate = new UpdateCostViewModel();
                    objUpdate.IdSchedule = idSchedule;
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
                    objUpdate.HotelId = Guid.Parse(hotelId);
                    objUpdate.RestaurantId = Guid.Parse(restaurantId);
                    objUpdate.PlaceId = Guid.Parse(placeId);
                    return JsonSerializer.Serialize(objUpdate);
                }
                // map data
                CreateCostViewModel obj = new CreateCostViewModel();
                obj.IdSchedule = idSchedule;
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
                var hotel = (from x in _db.Hotels where x.IdHotel == input.HotelId select x).First();
                var restaurant = (from x in _db.Restaurants where x.IdRestaurant == input.RestaurantId select x).First();
                var place = (from x in _db.Places where x.IdPlace == input.PlaceId select x).First();
                CostTour cost =
                cost = Mapper.MapCreateCost(input);
                cost.PriceHotelDB = hotel.DoubleRoomPrice;
                cost.PriceHotelSR = hotel.SingleRoomPrice;
                cost.PriceRestaurant = restaurant.ComboPrice;
                cost.PriceTicketPlace = place.PriceTicket;
                //
                _db.CostTours.Add(cost);
                _db.SaveChanges();
                // thêm schedule update giá
                // update price
                float holidayPercent = Convert.ToInt16(_config["PercentHoliday"]);
                var schedule = (from x in _db.Schedules where x.IdSchedule == input.IdSchedule select x).First();
                schedule.AdditionalPrice = cost.PriceHotelSR;
                schedule.AdditionalPriceHoliday = (cost.PriceHotelSR + (cost.PriceHotelSR * (holidayPercent / 100)));
                schedule.TotalCostTourNotService = cost.TotalCostTourNotService;

                float CostService = (cost.PriceHotelDB + cost.PriceRestaurant + cost.PriceTicketPlace);
                float VAT = schedule.Vat;
                int Profit = schedule.Profit;
                float FinalPrice = (cost.TotalCostTourNotService + CostService) + ((cost.TotalCostTourNotService + CostService) * VAT) + ((cost.TotalCostTourNotService + CostService) * Profit);
                schedule.FinalPrice = FinalPrice;
                schedule.FinalPriceHoliday = FinalPrice + (FinalPrice * (holidayPercent / 100));
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
            try
            {
                var list = (from x in _db.CostTours select x).ToList();
                var result = Mapper.MapCost(list);
                if (list.Count() > 0)
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
            catch(Exception e)
            {

                res.Notification.DateTime = DateTime.Now;
                res.Notification.Description = e.Message;
                res.Notification.Messenge = "Có lỗi xảy ra !";
                res.Notification.Type = "Error";
                return res;
            }
        }

        // hàm cần coi lại
        public Response GetCostByIdTourDetail(string idTourDetail)
        {
            try
            {
                //var tourdetail = from x in _db.CostTours where x.TourDetailId == idTourDetail select x;
                //var tourdetail = _db.CostTours.Where(x => x.IdCostTour == idTourDetail).FirstOrDefault();
                //var result = Mapper.MapCost(tourdetail);
                //if (result != null)
                //{
                //    res.Content = result;
                //}
                //else
                //{
                //    res.Notification.DateTime = DateTime.Now;
                //    res.Notification.Messenge = "Không có dữ liệu trả về !";
                //    res.Notification.Type = "Warning";
                //}
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

        public Response Update(UpdateCostViewModel input)
        {
            try
            {
                CostTour cost = (from x in _db.CostTours where x.IdSchedule == input.IdSchedule select x).First();
                  cost =  Mapper.MapUpdateCost(input);

                var hotel = (from x in _db.Hotels where x.IdHotel == input.HotelId select x).First();
                var restaurant = (from x in _db.Restaurants where x.IdRestaurant == input.RestaurantId select x).First();
                var place = (from x in _db.Places where x.IdPlace == input.PlaceId select x).First();
                cost.PriceHotelDB = hotel.DoubleRoomPrice;
                cost.PriceHotelSR = hotel.SingleRoomPrice;
                cost.PriceRestaurant = restaurant.ComboPrice;
                cost.PriceTicketPlace = place.PriceTicket;
                _db.CostTours.Update(cost);
                _db.SaveChanges();
                // update price
                float holidayPercent = Convert.ToInt16(_config["PercentHoliday"]);
                var schedule = (from x in _db.Schedules where x.IdSchedule == input.IdSchedule select x).First();
                schedule.AdditionalPrice = cost.PriceHotelSR;
                schedule.AdditionalPriceHoliday = (cost.PriceHotelSR + (cost.PriceHotelSR * (holidayPercent / 100)));
                schedule.TotalCostTourNotService = cost.TotalCostTourNotService;

                float CostService = (cost.PriceHotelDB + cost.PriceRestaurant + cost.PriceTicketPlace);
                float VAT = schedule.Vat;
                int Profit = schedule.Profit;
                float FinalPrice = (cost.TotalCostTourNotService + CostService) + ((cost.TotalCostTourNotService + CostService) * VAT) + ((cost.TotalCostTourNotService + CostService) * Profit);
                schedule.FinalPrice = FinalPrice;
                schedule.FinalPriceHoliday = FinalPrice + (FinalPrice * (holidayPercent / 100));

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
