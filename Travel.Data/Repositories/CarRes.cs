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
    public class CarRes : ICars
    {
        private readonly TravelContext _db;
        private Notification message;
        private Response res;
        public CarRes(TravelContext db)
        {
            _db = db;
            message = new Notification();
            res = new Response();
        }

        public string CheckBeforeSave(JObject frmData, ref Notification _message, bool isUpdate) // hàm đăng nhập  sử cho create update delete
        {
            try
            {
                var idCar = PrCommon.GetString("idCar", frmData); 
                if (!String.IsNullOrEmpty(idCar))  
                {                  
                }
               
                var nameDriver = PrCommon.GetString("nameDriver", frmData);
                if (!String.IsNullOrEmpty(nameDriver))
                {
                }            

                var amountSeat = PrCommon.GetString("amountSeat", frmData);
                if (!String.IsNullOrEmpty(amountSeat))
                {
                }
               
                var liscenseplate = PrCommon.GetString("liscenseplate", frmData);
                if (!String.IsNullOrEmpty(liscenseplate))
                {
                }
          
           
                var phone = PrCommon.GetString("Phone", frmData);
                if (!String.IsNullOrEmpty(phone))
                {
                }      
                
                var status = PrCommon.GetString("status", frmData);
                if (!String.IsNullOrEmpty(status))
                {
                }

                //if (isUpdate)
                //{
                //    UpdateRoleViewModel objUpdate = new UpdateRoleViewModel();
                //    objUpdate.IdRole = int.Parse(idRole);
                //    objUpdate.NameRole = nameRole;
                //    objUpdate.Description = description;
                //    return JsonSerializer.Serialize(objUpdate);
                //}

                CreateCarViewModel objCreate = new CreateCarViewModel();
                //objCreate.IdCar = Guid.Parse(idCar);
                objCreate.NameDriver = nameDriver;
                objCreate.AmountSeat =  int.Parse(amountSeat);
                objCreate.Status = 0;
                objCreate.LiscensePlate = liscenseplate;
                objCreate.Phone = phone;
                return JsonSerializer.Serialize(objCreate);

            }
            catch (Exception e)
            {
                message.DateTime = DateTime.Now;
                message.Description = e.Message;
                message.Messenge = "Có lỗi xảy ra !";
                message.Type = "Error";

                _message = message;
                return string.Empty;
            }
        }


        public Response Gets()
        {
            try
            {
                var listCar = (from x in _db.Cars select x).ToList();
                var result = Mapper.MapCar(listCar);
                if (result.Count() > 0)
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

        //public Response GetsDelete()
        //{
        //    try
        //    {

        //        var listCar = (from x in _db.Cars select x).ToList();
        //        var result = Mapper.MapCreateCar(listCar);

        //        if (listCar.Count() > 0)
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

        public Response Create(CreateCarViewModel input)
        {
            try
            {
                Car car = new Car();
                car = Mapper.MapCreateCar(input);
                _db.Cars.Add(car);
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

        public Response StatisticCar()
        {
            try
            {
                var lsCarFree = (from x in _db.Cars
                                 where x.Status == (int)Enums.StatusCar.Free
                                 select x).ToList();
                var lsCarBusy = (from x in _db.Cars
                                 where x.Status == (int)Enums.StatusCar.Busy
                                 select x).ToList();

                var lsCarFull = (from x in _db.Cars
                                   where x.Status == (int)Enums.StatusCar.Busy
                                   select x).ToList();

                var lsResult = lsCarFree.Concat(lsCarBusy).Concat(lsCarFull);
                if (lsResult.Count() > 0)
                {
                    res.Content = lsResult;
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
