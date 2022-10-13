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

        public CreateCarViewModel CheckBeforeSave(JObject frmData, ref Notification _message) // hàm đăng nhập  sử cho create update delete
        {
            CreateCarViewModel car = new CreateCarViewModel();
            try
            {
                int error = 0;
                var id = PrCommon.GetString("nameCar", frmData);
                if (!String.IsNullOrEmpty(id))
                {
                    car.IdCar = Guid.Parse(id);
                }
                else
                {
                    error++;
                }

                var name = PrCommon.GetString("nameCar", frmData);
                if (!String.IsNullOrEmpty(name))
                {
                    car.NameDriver = name.Trim();
                }
                else
                {
                    error++;
                }

                {
                    error++;
                }

                var liscenseplate = PrCommon.GetString("nameCar", frmData);
                if (!String.IsNullOrEmpty(name))
                {
                    car.LiscensePlate = name.Trim();
                }
                else
                {
                    error++;
                }

                {
                    error++;
                }

                var phone = PrCommon.GetString("Phone", frmData);
                if (!String.IsNullOrEmpty(phone))
                {
                    car.Phone = phone.Trim();
                }
                else
                {
                    error++;
                }
                return car;
            }
            catch (Exception e)
            {
                message.DateTime = DateTime.Now;
                message.Description = e.Message;
                message.Messenge = "Có lỗi xảy ra !";
                message.Type = "Error";

                _message = message;
                return car;
            }
        }

        //public Response Restore(CreateCarViewModel input)
        //{

        //    try
        //    {
        //        Car car = new Car();
        //        car = Mapper.MapCreateCar(input);

        //        var check = _db.Cars.Find(car.IdCar);
        //        if (check != null)
        //        {
                    
        //            _db.SaveChanges();

        //            res.Notification.DateTime = DateTime.Now;
        //            res.Notification.Messenge = "Restore thành công !";
        //            res.Notification.Type = "Success";
        //        }
        //        else
        //        {
        //            res.Notification.DateTime = DateTime.Now;
        //            res.Notification.Messenge = "Không tìm thấy !";
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

        //public CreateCarViewModel CheckBeforSave(JObject frmData, ref Notification _message)
        //{
        //    CreateCarViewModel car = new CreateCarViewModel();

        //    try
        //    {
        //        var idCar = PrCommon.GetString("idRole", frmData);
        //        if (!String.IsNullOrEmpty(idCar))
        //        {
        //            car.IdCar = Int32.Parse(idCar);
        //        }

        //        var nameDriver = PrCommon.GetString("nameDriver", frmData);
        //        if (!String.IsNullOrEmpty(nameDriver))
        //        {
        //            car.NameDriver = nameDriver;
        //        }

        //        var phone = PrCommon.GetString("phone", frmData);
        //        if (!String.IsNullOrEmpty(phone))
        //        {
        //            car.Phone = phone;
        //        }
        //        return car;
        //    }
        //    catch (Exception e)
        //    {
        //        message.DateTime = DateTime.Now;
        //        message.Description = e.Message;
        //        message.Messenge = "Có lỗi xảy ra !";
        //        message.Type = "Error";

        //        _message = message;
        //        return car;
        //    }
        //}

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
    }
}
