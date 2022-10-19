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
using Travel.Shared.ViewModels.Travel.ContractVM;
using static Travel.Shared.Ultilities.Enums;

namespace Travel.Data.Repositories
{
    public class ServiceRes : IService
    {
        private readonly TravelContext _db;
        private Notification message;
        private Response res;

        public ServiceRes(TravelContext db)
        {
            _db = db;
            message = new Notification();
            res = new Response();
        }
        public string CheckBeforSave(JObject frmData, ref Notification _message, TypeService type, bool isUpdate = false)
        {
            try
            {

                var priceTicket = PrCommon.GetString("priceTicket", frmData) ?? "0";
               
                var star = PrCommon.GetString("star", frmData);
                if (String.IsNullOrEmpty(star))
                {
                }
                var quantitySR = PrCommon.GetString("quantitySR", frmData);
                if (String.IsNullOrEmpty(quantitySR))
                {
                }
                var quantityDBR = PrCommon.GetString("quantityDBR", frmData);
                if (String.IsNullOrEmpty(quantityDBR))
                {
                }
                var singleRoomPrice = PrCommon.GetString("singleRoomPrice", frmData);
                if (String.IsNullOrEmpty(singleRoomPrice))
                {
                }

                var doubleRoomPrice = PrCommon.GetString("doubleRoomPrice", frmData);
                if (String.IsNullOrEmpty(doubleRoomPrice))
                {
                }

                var phone = PrCommon.GetString("phone", frmData);
                if (String.IsNullOrEmpty(phone))
                {
                }
                var address = PrCommon.GetString("address", frmData);
                if (String.IsNullOrEmpty(address))
                {
                }
                var name = PrCommon.GetString("name", frmData);
                if (String.IsNullOrEmpty(name))
                {
                }

                


                if (isUpdate)
                {
                    if (type == TypeService.Hotel)
                    {
                        UpdateHotelViewModel uHotelObj = new UpdateHotelViewModel();
                        uHotelObj.Address = address;
                        uHotelObj.DoubleRoomPrice = float.Parse(doubleRoomPrice);
                        uHotelObj.Name = name;
                        uHotelObj.Phone = phone;
                        uHotelObj.QuantityDBR = Convert.ToInt16(quantityDBR);
                        uHotelObj.QuantitySR = Convert.ToInt16(quantitySR);
                        uHotelObj.SingleRoomPrice = float.Parse(singleRoomPrice);
                        uHotelObj.Star = Convert.ToInt16(star);
                        return JsonSerializer.Serialize(uHotelObj);

                    }
                    else if (type == TypeService.Restaurant)
                    {
                        UpdateRestaurantViewModel uRestaurantObj = new UpdateRestaurantViewModel();
                        uRestaurantObj.Address = address;
                        uRestaurantObj.Name = name;
                        uRestaurantObj.Phone = phone;
                        return JsonSerializer.Serialize(uRestaurantObj);
                    }
                    else
                    {
                        UpdatePlaceViewModel uPlaceObj = new UpdatePlaceViewModel();
                        uPlaceObj.PriceTicket = float.Parse(priceTicket);
                        uPlaceObj.Address = address;
                        uPlaceObj.Name = name;
                        uPlaceObj.Phone = phone;
                        return JsonSerializer.Serialize(uPlaceObj);
                    }
                }
                else
                {
                    if (type == TypeService.Hotel)
                    {
                        CreateHotelViewModel hotelObj = new CreateHotelViewModel();
                        hotelObj.Address = address;
                        hotelObj.DoubleRoomPrice = float.Parse(doubleRoomPrice);
                        hotelObj.Name = name;
                        hotelObj.Phone = phone;
                        hotelObj.QuantityDBR = Convert.ToInt16(quantityDBR);
                        hotelObj.QuantitySR = Convert.ToInt16(quantitySR);
                        hotelObj.SingleRoomPrice = float.Parse(singleRoomPrice);
                        hotelObj.Star = Convert.ToInt16(star);
                        return JsonSerializer.Serialize(hotelObj);

                    }
                    else if (type == TypeService.Restaurant)
                    {
                        CreateRestaurantViewModel restaurantObj = new CreateRestaurantViewModel();
                        restaurantObj.Address = address;
                        restaurantObj.Name = name;
                        restaurantObj.Phone = phone;
                        return JsonSerializer.Serialize(restaurantObj);
                    }
                    else
                    {
                        CreatePlaceViewModel placeObj = new CreatePlaceViewModel();
                        placeObj.PriceTicket = float.Parse(priceTicket);
                        placeObj.Address = address;
                        placeObj.Name = name;
                        placeObj.Phone = phone;
                        return JsonSerializer.Serialize(placeObj);
                    }
                }
               
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

        public Response GetHotel()
        {
            try
            {
                var list = (from x in _db.Hotels where x.Approve
                            == Convert.ToInt16(Enums.ApproveStatus.Approved)
                            select x).ToList();
                var result = Mapper.MapHotel(list);
                if (list.Count() > 0)
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


        public Response CreateHotel(CreateHotelViewModel input)
        {
            try
            {
                Hotel hotel 
                 = Mapper.MapCreateHotel(input);
                _db.Hotels.Add(hotel);
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

        public Response CreatePlace(CreatePlaceViewModel input)
        {
            Place place
                           = Mapper.MapCreatePlace(input);
            _db.Places.Add(place);
            _db.SaveChanges();
            res.Notification.DateTime = DateTime.Now;
            res.Notification.Messenge = "Thêm thành công !";
            res.Notification.Type = "Success";
            return res;
        }

        public Response GetRestaurant()
        {
            try
            {
                var list = (from x in _db.Restaurants where x.Approve == Convert.ToInt16(ApproveStatus.Approved) select x).ToList();
                var result = Mapper.MapRestaurant(list);
                if (list.Count() > 0)
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
        public Response CreateRestaurant(CreateRestaurantViewModel input)
        {
            Restaurant restaurant
                         = Mapper.MapCreateRestaurant(input);
            _db.Restaurants.Add(restaurant);
            _db.SaveChanges();
            res.Notification.DateTime = DateTime.Now;
            res.Notification.Messenge = "Thêm thành công !";
            res.Notification.Type = "Success";
            return res;
        }
        public Response GetWaitingRestaurant()
        {
            try
            {
                var listWaiting = (from x in _db.Restaurants where x.Approve == Convert.ToInt16(ApproveStatus.Waiting) select x).ToList();
                var result = Mapper.MapRestaurant(listWaiting);

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

        public Response GetPlace()
        {
            try
            {
                var list = (from x in _db.Places where x.Approve == Convert.ToInt16(ApproveStatus.Approved) select x).ToList();
                var result = Mapper.MapPlace(list);
                if (list.Count() > 0)
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

        public Response GetWaitingHotel()
        {
            try
            {
                var listWaiting = (from x in _db.Hotels where x.Approve == Convert.ToInt16(ApproveStatus.Waiting) select x).ToList();
                var result = Mapper.MapHotel(listWaiting);

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

        public Response GetWaitingHPlace()
        {
            try
            {
                var listWaiting = (from x in _db.Places where x.Approve == Convert.ToInt16(ApproveStatus.Waiting) select x).ToList();
                var result = Mapper.MapPlace(listWaiting);

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
