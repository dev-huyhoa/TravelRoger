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
                var idHotel = PrCommon.GetString("idHotel", frmData);
                if (String.IsNullOrEmpty(idHotel))
                {
                }
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
                var comboPrice = PrCommon.GetString("comboPrice", frmData);
                if (String.IsNullOrEmpty(comboPrice))
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
                var name = PrCommon.GetString("nameHotel", frmData);
                if (String.IsNullOrEmpty(name))
                {
                }
                var nameContract = PrCommon.GetString("nameContract", frmData);
                if (String.IsNullOrEmpty(nameContract))
                {
                }
                var typeAction = PrCommon.GetString("typeAction", frmData);
                if (String.IsNullOrEmpty(typeAction))
                {
                }
                var idUserModify = PrCommon.GetString("idUserModify", frmData);
                if (String.IsNullOrEmpty(idUserModify))
                {
                }

                if (isUpdate)
                {
                    if (type == TypeService.Hotel)
                    {
                        UpdateHotelViewModel uHotelObj = new UpdateHotelViewModel();
                        uHotelObj.IdHotel = Guid.Parse(idHotel);
                        uHotelObj.Address = address;
                        uHotelObj.DoubleRoomPrice = float.Parse(doubleRoomPrice);
                        uHotelObj.Name = name;
                        uHotelObj.Phone = phone;
                        uHotelObj.QuantityDBR = Convert.ToInt16(quantityDBR);
                        uHotelObj.QuantitySR = Convert.ToInt16(quantitySR);
                        uHotelObj.SingleRoomPrice = float.Parse(singleRoomPrice);
                        uHotelObj.Star = Convert.ToInt16(star);
                        uHotelObj.IdUserModify = Guid.Parse(idUserModify);
                        uHotelObj.TypeAction = typeAction;
                        return JsonSerializer.Serialize(uHotelObj);

                    }
                    else if (type == TypeService.Restaurant)
                    {
                        UpdateRestaurantViewModel uRestaurantObj = new UpdateRestaurantViewModel();
                        uRestaurantObj.Address = address;
                        uRestaurantObj.Name = name;
                        uRestaurantObj.Phone = phone;
                        uRestaurantObj.IdUserModify = Guid.Parse(idUserModify);
                        uRestaurantObj.ComboPrice = float.Parse(comboPrice);
                        uRestaurantObj.TypeAction = typeAction;
                        return JsonSerializer.Serialize(uRestaurantObj);
                    }
                    else
                    {
                        UpdatePlaceViewModel uPlaceObj = new UpdatePlaceViewModel();
                        uPlaceObj.PriceTicket = float.Parse(priceTicket);
                        uPlaceObj.Address = address;
                        uPlaceObj.Name = name;
                        uPlaceObj.Phone = phone;
                        uPlaceObj.IdUserModify = Guid.Parse(idUserModify);
                        uPlaceObj.TypeAction = typeAction;
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
                        hotelObj.NameContract = nameContract;
                        return JsonSerializer.Serialize(hotelObj);

                    }
                    else if (type == TypeService.Restaurant)
                    {
                        CreateRestaurantViewModel restaurantObj = new CreateRestaurantViewModel();
                        restaurantObj.Address = address;
                        restaurantObj.Name = name;
                        restaurantObj.Phone = phone;
                        restaurantObj.NameContract = nameContract;
                        return JsonSerializer.Serialize(restaurantObj);
                    }
                    else
                    {
                        CreatePlaceViewModel placeObj = new CreatePlaceViewModel();
                        placeObj.PriceTicket = float.Parse(priceTicket);
                        placeObj.Address = address;
                        placeObj.Name = name;
                        placeObj.Phone = phone;
                        placeObj.NameContract = nameContract;
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

        #region Hotel
        public Response GetWaitingHotel(Guid idUser)
        {
            try
            {
                var userLogin = (from x in _db.Employees
                                 where x.IdEmployee == idUser
                                 select x).FirstOrDefault();
                var listWaiting = new List<Hotel>();
                if (userLogin.RoleId == (int)Enums.TitleRole.Admin)
                {
                    listWaiting = (from x in _db.Hotels where x.Approve == Convert.ToInt16(ApproveStatus.Waiting)
                                   && x.IsTempdata == false
                                   select x).ToList();
                }
                else
                {
                    listWaiting = (from x in _db.Hotels
                                   where x.IdUserModify == idUser
                                   && x.Approve == Convert.ToInt16(ApproveStatus.Waiting)
                                   && x.IsTempdata == false
                                   select x).ToList();
                }
                var result = Mapper.MapHotel(listWaiting);
                if (listWaiting.Count() > 0)
                {
                    res = Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);
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
        public Response GetHotel()
        {
            try
            {
                var list = (from x in _db.Hotels
                            where x.Approve == Convert.ToInt16(Enums.ApproveStatus.Approved)
                            && x.IsTempdata == false
                            select x).ToList();
                var result = Mapper.MapHotel(list);
                if (list.Count() > 0)
                {
                    res = Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);
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
                hotel.TypeAction = "insert";
                _db.Hotels.Add(hotel);
                _db.SaveChanges();
                res = Ultility.Responses("Thêm thành công !", Enums.TypeCRUD.Success.ToString());
                return res;
            }
            catch (Exception e)
            {
                res = Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
                return res;
            }
        }

        public Response DeleteHotel(Guid id, Guid idUser)
        {
            try
            {
                var hotel = (from x in _db.Hotels
                             where x.IdHotel == id
                             select x).FirstOrDefault();

                var userLogin = (from x in _db.Employees
                                 where x.IdEmployee == idUser
                                 select x).FirstOrDefault();
                if (hotel.Approve == (int)ApproveStatus.Approved)
                {
                    hotel.ModifyBy = userLogin.NameEmployee;
                    hotel.TypeAction = "delete";
                    hotel.IdUserModify = userLogin.IdEmployee;
                    hotel.ModifyDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);
                    hotel.Approve = (int)ApproveStatus.Waiting;

                    res = Ultility.Responses("Đã gửi yêu cầu xóa !", Enums.TypeCRUD.Success.ToString());
                }
                else
                {
                    if (hotel.IdUserModify == idUser)
                    {
                        if (hotel.TypeAction == "insert")
                        {
                            _db.Hotels.Remove(hotel);
                            res = Ultility.Responses("Đã xóa!", Enums.TypeCRUD.Success.ToString());
                        }
                        else if (hotel.TypeAction == "update")
                        {
                            var idHotelTemp = hotel.IdAction;
                            // old hotel
                            var hotelTemp = (from x in _db.Hotels
                                             where x.IdHotel == Guid.Parse(idHotelTemp)
                                             select x).FirstOrDefault();
                            hotel.Approve = (int)ApproveStatus.Approved;

                            hotel.IdAction = null;
                            hotel.TypeAction = null;

                            #region restore old data
                            hotel.Approve = (int)ApproveStatus.Approved;


                            hotel.Address = hotelTemp.Address;
                            hotel.DoubleRoomPrice = hotelTemp.DoubleRoomPrice;
                            hotel.SingleRoomPrice = hotelTemp.SingleRoomPrice;
                            hotel.NameHotel = hotelTemp.NameHotel;
                            hotel.Phone = hotelTemp.Phone;
                            hotel.QuantityDBR = hotelTemp.QuantityDBR;
                            hotel.QuantitySR = hotelTemp.QuantitySR;
                            hotel.Star = hotelTemp.Star;
                            #endregion
                            _db.Hotels.Remove(hotelTemp);
                            res = Ultility.Responses("Đã hủy yêu cầu chỉnh sửa !", Enums.TypeCRUD.Success.ToString());

                        }
                    
                        else if (hotel.TypeAction == "restore")
                        {
                            hotel.IdAction = null;
                            hotel.TypeAction = null;
                            hotel.IsDelete = true;
                            hotel.Approve = (int)ApproveStatus.Approved;
                            res = Ultility.Responses("Đã hủy yêu cầu khôi phục !", Enums.TypeCRUD.Success.ToString());

                        }
                        else // delete
                        {
                            hotel.IdAction = null;
                            hotel.TypeAction = null;
                            hotel.IsDelete = false;
                            hotel.Approve = (int)ApproveStatus.Approved;
                        }
                        hotel.Approve = (int)ApproveStatus.Approved;
                        res = Ultility.Responses("Đã hủy yêu cầu xóa !", Enums.TypeCRUD.Success.ToString());
                    }
                    else
                    {
                        res = Ultility.Responses("Bạn không thể thực thi hành động này !", Enums.TypeCRUD.Success.ToString());
                    }
                }
                _db.SaveChanges();
                return res;

            }
            catch (Exception e)
            {
                res = Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
                return res;
            }
        }
        public Response UpdateHotel(UpdateHotelViewModel input)
        {
            try
            {
                var userLogin = (from x in _db.Employees
                                 where x.IdEmployee == input.IdUserModify
                                 select x).FirstOrDefault();

                var hotel = (from x in _db.Hotels
                             where x.IdHotel == input.IdHotel
                             select x).FirstOrDefault();

                // clone new object
                var hotelOld = new Hotel();
                hotelOld = Ultility.DeepCopy<Hotel>(hotel);
                hotelOld.IdAction = hotelOld.IdHotel.ToString();
                hotelOld.IdHotel = Guid.NewGuid();
                hotelOld.IsTempdata = true;

                _db.Hotels.Add(hotelOld);


                #region setdata
                hotel.IdAction = hotelOld.IdHotel.ToString();
                hotel.IdUserModify = input.IdUserModify;
                hotel.TypeAction = input.TypeAction;
                hotel.Approve = (int)ApproveStatus.Waiting;
                hotel.ModifyBy = userLogin.NameEmployee;
                hotel.ModifyDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);


                hotel.Address = input.Address;
                hotel.DoubleRoomPrice = input.DoubleRoomPrice;
                hotel.SingleRoomPrice = input.SingleRoomPrice;
                hotel.NameHotel = input.Name;
                hotel.Phone = input.Phone;
                hotel.QuantityDBR = input.QuantityDBR;
                hotel.QuantitySR = input.QuantitySR;
                hotel.Star = input.Star;
                #endregion


                _db.SaveChanges();
                res = Ultility.Responses("Đã gửi yêu cầu sửa !", Enums.TypeCRUD.Success.ToString());
                return res;

            }
            catch (Exception e)
            {
                res = Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
                return res;
            }
        }
        public Response ApproveHotel(Guid id)
        {
            try
            {
                var hotel = (from x in _db.Hotels
                             where x.IdHotel == id
                             && x.Approve == (int)ApproveStatus.Waiting
                             select x).FirstOrDefault();
                if (hotel.Approve == (int)ApproveStatus.Waiting)
                {


                    if (hotel.TypeAction == "update")
                    {
                        var idHotelTemp = hotel.IdAction;
                        hotel.Approve = (int)ApproveStatus.Approved;
                        hotel.IdAction = null;
                        hotel.TypeAction = null;


                        // delete tempdata
                        var hotelTemp = (from x in _db.Hotels
                                         where x.IdHotel == Guid.Parse(idHotelTemp)
                                         select x).FirstOrDefault();
                        _db.Hotels.Remove(hotelTemp);
                    }
                    else if (hotel.TypeAction == "insert")
                    {
                        hotel.IdAction = null;
                        hotel.TypeAction = null;
                        hotel.Approve = (int)ApproveStatus.Approved;
                    }
                    else if (hotel.TypeAction == "restore")
                    {
                        hotel.IdAction = null;
                        hotel.TypeAction = null;
                        hotel.Approve = (int)ApproveStatus.Approved;
                        hotel.IsDelete = false;

                    }
                    else
                    {
                        hotel.IdAction = null;
                        hotel.TypeAction = null;
                        hotel.Approve = (int)ApproveStatus.Approved;
                        hotel.IsDelete = true;
                    }




                    _db.SaveChanges();
                    res = Ultility.Responses($"Duyệt thành công !", Enums.TypeCRUD.Success.ToString());
                }
                return res;

            }
            catch (Exception e)
            {
                res = Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
                return res;
            }
        }

        public Response RefusedHotel(Guid id)
        {
            try
            {
                var hotel = (from x in _db.Hotels
                             where x.IdHotel == id
                             && x.Approve == (int)ApproveStatus.Waiting
                             select x).FirstOrDefault();
                if (hotel.Approve == (int)ApproveStatus.Waiting)
                {
                    if (hotel.TypeAction == "update")
                    {
                        var idHotelTemp = hotel.IdAction;
                        // old hotel
                        var hotelTemp = (from x in _db.Hotels
                                         where x.IdHotel == Guid.Parse(idHotelTemp)
                                         select x).FirstOrDefault();
                        hotel.Approve = (int)ApproveStatus.Approved;

                        hotel.IdAction = null;
                        hotel.TypeAction = null;

                        #region restore old data
                        hotel.Approve = (int)ApproveStatus.Approved;


                        hotel.Address = hotelTemp.Address;
                        hotel.DoubleRoomPrice = hotelTemp.DoubleRoomPrice;
                        hotel.SingleRoomPrice = hotelTemp.SingleRoomPrice;
                        hotel.NameHotel = hotelTemp.NameHotel;
                        hotel.Phone = hotelTemp.Phone;
                        hotel.QuantityDBR = hotelTemp.QuantityDBR;
                        hotel.QuantitySR = hotelTemp.QuantitySR;
                        hotel.Star = hotelTemp.Star;
                        #endregion

                        _db.Hotels.Remove(hotelTemp);
                    }
                    else if (hotel.TypeAction == "insert")
                    {
                        hotel.IdAction = null;
                        hotel.TypeAction = null;
                        hotel.Approve = (int)ApproveStatus.Refused;
                    }
                    else if (hotel.TypeAction == "restore")
                    {
                        hotel.IdAction = null;
                        hotel.TypeAction = null;
                        hotel.IsDelete = true;
                        hotel.Approve = (int)ApproveStatus.Approved;
                    }
                    else // delete
                    {
                        hotel.IdAction = null;
                        hotel.TypeAction = null;
                        hotel.IsDelete = false;
                        hotel.Approve = (int)ApproveStatus.Approved;
                    }
                    _db.SaveChanges();
                    res = Ultility.Responses($"Từ chối thành công !", Enums.TypeCRUD.Success.ToString());
                }
                return res;

            }
            catch (Exception e)
            {
                res = Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
                return res;
            }
        }
        #endregion
        #region Restaurant
        public Response GetRestaurant()
        {
            try
            {
                var list = (from x in _db.Restaurants where x.Approve == Convert.ToInt16(ApproveStatus.Approved) select x).ToList();
                var result = Mapper.MapRestaurant(list);
                if (list.Count() > 0)
                {
                    res = Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);
                }
                return res;
            }
            catch (Exception e)
            {
                res = Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
                return res;
            }
        }

        public Response GetWaitingRestaurant(Guid idUser)
        {
            try
            {
                var userLogin = (from x in _db.Employees
                                 where x.IdEmployee == idUser
                                 select x).FirstOrDefault();
                var listWaiting = new List<Restaurant>();
                if (userLogin.RoleId == (int)Enums.TitleRole.Admin)
                {
                    listWaiting = (from x in _db.Restaurants where x.Approve == Convert.ToInt16(ApproveStatus.Waiting) select x).ToList();
                }
                else
                {
                    listWaiting = (from x in _db.Restaurants
                                   where x.IdUserModify == idUser
                                   && x.Approve == Convert.ToInt16(ApproveStatus.Waiting)
                                   select x).ToList();
                }

                var result = Mapper.MapRestaurant(listWaiting);

                if (listWaiting.Count() > 0)
                {
                    res = Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);
                }
                return res;
            }
            catch (Exception e)
            {
                res = Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
                return res;
            }
        }
        public Response DeleteRestaurant(Guid id, Guid idUser)
        {
            try
            {
                var restaurant = (from x in _db.Restaurants
                                  where x.IdRestaurant == id
                                  select x).FirstOrDefault();
                var userLogin = (from x in _db.Employees
                                 where x.IdEmployee == idUser
                                 select x).FirstOrDefault();
                if (restaurant.Approve == (int)ApproveStatus.Approved)
                {
                    restaurant.ModifyBy = userLogin.NameEmployee;
                    restaurant.IdUserModify = userLogin.IdEmployee;
                    restaurant.ModifyDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);
                    restaurant.Approve = (int)ApproveStatus.Waiting;
                    restaurant.TypeAction = "delete";

                    res = Ultility.Responses("Đã gửi yêu cầu xóa !", Enums.TypeCRUD.Success.ToString());
                }
                else
                {
                    if (restaurant.IdUserModify == idUser)
                    {
                        if (restaurant.TypeAction == "insert")
                        {
                            _db.Restaurants.Remove(restaurant);
                            res = Ultility.Responses("Đã xóa!", Enums.TypeCRUD.Success.ToString());
                        }
                        else if (restaurant.TypeAction == "update")
                        {
                            var idRestaurantTemp = restaurant.IdAction;
                            var restaurantTemp = (from x in _db.Restaurants
                                                  where x.IdRestaurant == Guid.Parse(idRestaurantTemp)
                                                  select x).FirstOrDefault();
                            restaurant.Approve = (int)ApproveStatus.Approved;

                            restaurant.IdAction = null;
                            restaurant.TypeAction = null;

                            #region restore old data
                            restaurant.Approve = (int)ApproveStatus.Approved;


                            restaurant.Address = restaurantTemp.Address;

                            restaurant.Phone = restaurantTemp.Phone;
                            restaurant.NameRestaurant = restaurantTemp.NameRestaurant;
                            restaurant.ComboPrice = restaurantTemp.ComboPrice;
                            #endregion

                            _db.Restaurants.Remove(restaurantTemp);
                            res = Ultility.Responses("Đã hủy yêu cầu chỉnh sửa !", Enums.TypeCRUD.Success.ToString());

                        }
                        else if (restaurant.TypeAction == "restore")
                        {
                            restaurant.IdAction = null;
                            restaurant.TypeAction = null;
                            restaurant.IsDelete = true;
                            restaurant.Approve = (int)ApproveStatus.Approved;
                            res = Ultility.Responses("Đã hủy yêu cầu khôi phục !", Enums.TypeCRUD.Success.ToString());

                        }
                        else //delete
                        {
                            restaurant.IdAction = null;
                            restaurant.TypeAction = null;
                            restaurant.IsDelete = false;
                            restaurant.Approve = (int)ApproveStatus.Approved;
                            res = Ultility.Responses("Đã hủy yêu cầu xóa !", Enums.TypeCRUD.Success.ToString());

                        }
                    }
                    else
                    {
                        res = Ultility.Responses("Bạn không thể thực thi hành động này !", Enums.TypeCRUD.Success.ToString());
                    }
                }
                _db.SaveChanges();
                return res;

            }
            catch (Exception e)
            {
                res = Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
                return res;
            }
        }
        public Response UpdateRestaurant(UpdateRestaurantViewModel input)
        {
            try
            {
                var userLogin = (from x in _db.Employees
                                 where x.IdEmployee == input.IdUserModify
                                 select x).FirstOrDefault();

                var restaurant = (from x in _db.Restaurants
                                  where x.IdRestaurant == input.IdRestaurant
                                  select x).FirstOrDefault();

                // clone new object
                var restaurantOld = new Restaurant();
                restaurantOld = Ultility.DeepCopy<Restaurant>(restaurant);
                restaurantOld.IdAction = restaurantOld.IdRestaurant.ToString(); // gán ID cũ cho IdAction để có thể truy cập qua lại
                restaurantOld.IdRestaurant = Guid.NewGuid();
                restaurantOld.IsTempdata = true;
                _db.Restaurants.Add(restaurantOld);

                #region setdata
                restaurant.IdAction = restaurantOld.IdRestaurant.ToString();
                restaurant.IdUserModify = input.IdUserModify;
                restaurant.TypeAction = input.TypeAction;
                restaurant.Approve = (int)ApproveStatus.Waiting;
                restaurant.ModifyBy = userLogin.NameEmployee;
                restaurant.ModifyDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);

                restaurant.Address = input.Address;
                restaurant.NameRestaurant = input.Name;
                restaurant.Phone = input.Phone;
                restaurant.ComboPrice = input.ComboPrice;
                #endregion

                _db.SaveChanges();
                res = Ultility.Responses("Đã gửi yêu cầu sửa !", Enums.TypeCRUD.Success.ToString());
                return res;

            }
            catch (Exception e)
            {
                res = Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
                return res;
            }
        }
        public Response CreateRestaurant(CreateRestaurantViewModel input)
        {
            Restaurant restaurant
                         = Mapper.MapCreateRestaurant(input);

            _db.Restaurants.Add(restaurant);
            _db.SaveChanges();
            Ultility.Responses("Thêm thành công !", Enums.TypeCRUD.Success.ToString());
            return res;
        }

        public Response ApproveRestaurant(Guid id)
        {
            try
            {
                var restaurant = (from x in _db.Restaurants
                                  where x.IdRestaurant == id
                                  && x.Approve == (int)ApproveStatus.Waiting
                                  select x).FirstOrDefault();
                if (restaurant.Approve == (int)ApproveStatus.Waiting)
                {
                    if (restaurant.TypeAction == "update")
                    {
                        var idRestaurantTemp = restaurant.IdAction;
                        restaurant.Approve = (int)ApproveStatus.Approved;
                        restaurant.IdAction = null;
                        restaurant.TypeAction = null;
                        // delete tempdata
                        var restaurantTemp = (from x in _db.Restaurants
                                              where x.IdRestaurant == Guid.Parse(idRestaurantTemp)
                                              select x).FirstOrDefault();
                        _db.Restaurants.Remove(restaurantTemp);
                    }
                    else if (restaurant.TypeAction == "insert")
                    {
                        restaurant.IdAction = null;
                        restaurant.TypeAction = null;
                        restaurant.Approve = (int)ApproveStatus.Approved;
                    }
                    else if (restaurant.TypeAction == "restore")
                    {
                        restaurant.IdAction = null;
                        restaurant.TypeAction = null;
                        restaurant.Approve = (int)ApproveStatus.Approved;
                        restaurant.IsDelete = false;
                    }
                    else // delete
                    {
                        restaurant.IdAction = null;
                        restaurant.TypeAction = null;
                        restaurant.Approve = (int)ApproveStatus.Approved;
                        restaurant.IsDelete = true;
                    }




                    _db.SaveChanges();
                    res = Ultility.Responses($"Duyệt thành công !", Enums.TypeCRUD.Success.ToString());
                }
                return res;

            }
            catch (Exception e)
            {
                res = Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
                return res;
            }
        }

        public Response RefusedRestaurant(Guid id)
        {
            try
            {
                var restaurant = (from x in _db.Restaurants
                                  where x.IdRestaurant == id
                                  && x.Approve == (int)ApproveStatus.Waiting
                                  select x).FirstOrDefault();
                if (restaurant.Approve == (int)ApproveStatus.Waiting)
                {
                    if (restaurant.TypeAction == "update")
                    {
                        var idRestaurantTemp = restaurant.IdAction;
                        // old hotel
                        var restaurantTemp = (from x in _db.Restaurants
                                              where x.IdRestaurant == Guid.Parse(idRestaurantTemp)
                                              select x).FirstOrDefault();
                        restaurant.Approve = (int)ApproveStatus.Approved;
                        restaurant.IdAction = null;
                        restaurant.TypeAction = null;
                        #region restore old data

                        restaurant.Approve = (int)ApproveStatus.Approved;
                        restaurant.Address = restaurantTemp.Address;

                        restaurant.Phone = restaurantTemp.Phone;
                        restaurant.NameRestaurant = restaurantTemp.NameRestaurant;
                        restaurant.ComboPrice = restaurantTemp.ComboPrice;
                        #endregion

                        _db.Restaurants.Remove(restaurantTemp);
                    }
                    else if (restaurant.TypeAction == "insert")
                    {
                        restaurant.IdAction = null;
                        restaurant.TypeAction = null;
                        restaurant.Approve = (int)ApproveStatus.Refused;
                    }
                    else if (restaurant.TypeAction == "restore")
                    {
                        restaurant.IdAction = null;
                        restaurant.TypeAction = null;
                        restaurant.IsDelete = true;
                        restaurant.Approve = (int)ApproveStatus.Approved;
                    }
                    else // delete
                    {
                        restaurant.IdAction = null;
                        restaurant.TypeAction = null;
                        restaurant.IsDelete = false;
                        restaurant.Approve = (int)ApproveStatus.Approved;
                    }
                    _db.SaveChanges();
                    res = Ultility.Responses($"Từ chối thành công !", Enums.TypeCRUD.Success.ToString());
                }
                return res;

            }
            catch (Exception e)
            {
                res = Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
                return res;
            }
        }
        #endregion
        #region Place
        public Response CreatePlace(CreatePlaceViewModel input)
        {
            Place place
                           = Mapper.MapCreatePlace(input);
            _db.Places.Add(place);
            _db.SaveChanges();
            Ultility.Responses("Thêm thành công !", Enums.TypeCRUD.Success.ToString());
            return res;
        }
        public Response GetPlace()
        {
            try
            {
                var list = (from x in _db.Places where x.Approve == Convert.ToInt16(ApproveStatus.Approved) select x).ToList();
                var result = Mapper.MapPlace(list);
                if (list.Count() > 0)
                {
                    res = Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);
                }
                return res;
            }
            catch (Exception e)
            {
                res = Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
                return res;
            }
        }
        public Response GetWaitingHPlace(Guid idUser)
        {
            try
            {
                var userLogin = (from x in _db.Employees
                                 where x.IdEmployee == idUser
                                 select x).FirstOrDefault();
                var listWaiting = new List<Place>();
                if (userLogin.RoleId == (int)Enums.TitleRole.Admin)
                {
                    listWaiting = (from x in _db.Places where x.Approve == Convert.ToInt16(ApproveStatus.Waiting) select x).ToList();
                }
                else
                {
                    listWaiting = (from x in _db.Places
                                   where x.IdUserModify == idUser
                                   && x.Approve == Convert.ToInt16(ApproveStatus.Waiting)
                                   select x).ToList();
                }
                var result = Mapper.MapPlace(listWaiting);

                if (listWaiting.Count() > 0)
                {
                    res = Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);
                }
                return res;
            }
            catch (Exception e)
            {
                res = Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
                return res;
            }
        }
        public Response DeletePlace(Guid id, Guid idUser)
        {
            try
            {
                var place = (from x in _db.Places
                             where x.IdPlace == id
                             select x).FirstOrDefault();

                var userLogin = (from x in _db.Employees
                                 where x.IdEmployee == idUser
                                 select x).FirstOrDefault();
                if (place.Approve == (int)ApproveStatus.Approved)
                {
                    place.ModifyBy = userLogin.NameEmployee;
                    place.IdUserModify = userLogin.IdEmployee;
                    place.ModifyDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);
                    place.Approve = (int)ApproveStatus.Waiting;

                    res = Ultility.Responses("Đã gửi yêu cầu xóa !", Enums.TypeCRUD.Success.ToString());
                }
                else
                {
                    if (place.IdUserModify == idUser)
                    {
                        if (place.TypeAction == "insert")
                        {
                            _db.Places.Remove(place);
                            res = Ultility.Responses("Đã xóa!", Enums.TypeCRUD.Success.ToString());
                        }
                        else if (place.TypeAction == "update")
                        {
                            var idPlaceTemp = place.IdAction;
                            // old hotel
                            var placeTemp = (from x in _db.Places
                                             where x.IdPlace == Guid.Parse(idPlaceTemp)
                                             select x).FirstOrDefault();
                            place.Approve = (int)ApproveStatus.Approved;
                            place.IdAction = null;
                            place.TypeAction = null;
                            #region restore old data

                            place.Approve = (int)ApproveStatus.Approved;
                            place.Address = placeTemp.Address;

                            place.Phone = placeTemp.Phone;
                            place.NamePlace = placeTemp.NamePlace;
                            place.PriceTicket = placeTemp.PriceTicket;
                            #endregion

                            _db.Places.Remove(placeTemp);
                            res = Ultility.Responses("Đã hủy yêu cầu chỉnh sửa !", Enums.TypeCRUD.Success.ToString());
                        }
                        else if (place.TypeAction == "restore")
                        {
                            place.IdAction = null;
                            place.TypeAction = null;
                            place.IsDelete = true;
                            place.Approve = (int)ApproveStatus.Approved;
                            res = Ultility.Responses("Đã hủy yêu cầu khôi phục!", Enums.TypeCRUD.Success.ToString());

                        }
                        else // delete
                        {
                            place.IdAction = null;
                            place.TypeAction = null;
                            place.IsDelete = false;
                            place.Approve = (int)ApproveStatus.Approved;
                            res = Ultility.Responses("Đã hủy yêu cầu xóa !", Enums.TypeCRUD.Success.ToString());

                        }
                    }
                }
                _db.SaveChanges();
                return res;

            }
            catch (Exception e)
            {
                res = Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
                return res;
            }
        }
        public Response UpdatePlace(UpdateHotelViewModel input)
        {
            try
            {
                var userLogin = (from x in _db.Employees
                                 where x.IdEmployee == input.IdUserModify
                                 select x).FirstOrDefault();

                var hotel = (from x in _db.Hotels
                             where x.IdHotel == input.IdHotel
                             select x).FirstOrDefault();

                // clone new object
                var hotelOld = new Hotel();
                hotelOld = Ultility.DeepCopy<Hotel>(hotel);
                hotelOld.IdAction = hotelOld.IdHotel.ToString();
                hotelOld.IdHotel = Guid.NewGuid();
                hotelOld.IsTempdata = true;

                _db.Hotels.Add(hotelOld);

                #region setdata
                hotel.IdAction = hotelOld.IdHotel.ToString();
                hotel.IdUserModify = input.IdUserModify;
                hotel.TypeAction = input.TypeAction;
                hotel.Approve = (int)ApproveStatus.Waiting;
                hotel.ModifyBy = userLogin.NameEmployee;
                hotel.ModifyDate = Ultility.ConvertDatetimeToUnixTimeStampMiliSecond(DateTime.Now);


                hotel.Address = input.Address;
                hotel.DoubleRoomPrice = input.DoubleRoomPrice;
                hotel.SingleRoomPrice = input.SingleRoomPrice;
                hotel.NameHotel = input.Name;
                hotel.Phone = input.Phone;
                hotel.QuantityDBR = input.QuantityDBR;
                hotel.QuantitySR = input.QuantitySR;
                hotel.Star = input.Star;
                #endregion


                _db.SaveChanges();
                res = Ultility.Responses("Đã gửi yêu cầu sửa !", Enums.TypeCRUD.Success.ToString());
                return res;

            }
            catch (Exception e)
            {
                res = Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
                return res;
            }
        }
        public Response ApprovePlace(Guid id)
        {
            try
            {
                var hotel = (from x in _db.Hotels
                             where x.IdHotel == id
                             && x.Approve == (int)ApproveStatus.Waiting
                             select x).FirstOrDefault();
                if (hotel.Approve == (int)ApproveStatus.Waiting)
                {


                    if (hotel.TypeAction == "update")
                    {
                        var idHotelTemp = hotel.IdAction;
                        hotel.Approve = (int)ApproveStatus.Approved;
                        hotel.IdAction = null;
                        hotel.TypeAction = null;


                        // delete tempdata
                        var hotelTemp = (from x in _db.Hotels
                                         where x.IdHotel == Guid.Parse(idHotelTemp)
                                         select x).FirstOrDefault();
                        _db.Hotels.Remove(hotelTemp);
                    }
                    else if (hotel.TypeAction == "insert")
                    {
                        hotel.IdAction = null;
                        hotel.TypeAction = null;
                        hotel.Approve = (int)ApproveStatus.Approved;
                    }
                    else if (hotel.TypeAction == "restore")
                    {
                        hotel.IdAction = null;
                        hotel.TypeAction = null;
                        hotel.Approve = (int)ApproveStatus.Approved;
                        hotel.IsDelete = false;

                    }
                    else
                    {
                        hotel.IdAction = null;
                        hotel.TypeAction = null;
                        hotel.Approve = (int)ApproveStatus.Approved;
                        hotel.IsDelete = true;
                    }




                    _db.SaveChanges();
                    res = Ultility.Responses($"Duyệt thành công !", Enums.TypeCRUD.Success.ToString());
                }
                return res;

            }
            catch (Exception e)
            {
                res = Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
                return res;
            }
        }
        public Response RefusedPlace(Guid id)
        {
            try
            {
                var place = (from x in _db.Places
                             where x.IdPlace == id
                             && x.Approve == (int)ApproveStatus.Waiting
                             select x).FirstOrDefault();
                if (place.Approve == (int)ApproveStatus.Waiting)
                {
                    if (place.TypeAction == "update")
                    {
                        var idPlaceTemp = place.IdAction;
                        // old hotel
                        var placeTemp = (from x in _db.Places
                                         where x.IdPlace == Guid.Parse(idPlaceTemp)
                                         select x).FirstOrDefault();
                        place.Approve = (int)ApproveStatus.Approved;
                        place.IdAction = null;
                        place.TypeAction = null;
                        #region restore old data

                        place.Approve = (int)ApproveStatus.Approved;
                        place.Address = placeTemp.Address;

                        place.Phone = placeTemp.Phone;
                        place.NamePlace = placeTemp.NamePlace;
                        place.PriceTicket = placeTemp.PriceTicket;
                        #endregion

                        _db.Places.Remove(placeTemp);
                    }
                    else if (place.TypeAction == "insert")
                    {
                        place.IdAction = null;
                        place.TypeAction = null;
                        place.Approve = (int)ApproveStatus.Refused;
                    }
                    else if (place.TypeAction == "restore")
                    {
                        place.IdAction = null;
                        place.TypeAction = null;
                        place.IsDelete = true;
                        place.Approve = (int)ApproveStatus.Approved;
                    }
                    else // delete
                    {
                        place.IdAction = null;
                        place.TypeAction = null;
                        place.IsDelete = false;
                        place.Approve = (int)ApproveStatus.Approved;
                    }
                    _db.SaveChanges();
                    res = Ultility.Responses($"Từ chối thành công !", Enums.TypeCRUD.Success.ToString());
                }
                return res;

            }
            catch (Exception e)
            {
                res = Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
                return res;
            }
        }
        #endregion
        public Response CreateContract(CreateContractViewModel input)
        {
            try
            {
                Contract contract = Mapper.MapCreateContract(input);
                _db.Contracts.Add(contract);
                _db.SaveChanges();
                res.Notification.DateTime = DateTime.Now;
                res.Notification.Messenge = "Thêm thành công !";
                res.Notification.Type = "Success";
                return res;
            }
            catch (Exception e)
            {
                res = Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
                return res;
            }
        }

    }
}
