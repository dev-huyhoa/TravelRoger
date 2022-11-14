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
        private void UpdateDatabase<T>(T input)
        {
            _db.Entry(input).State = EntityState.Modified;
        }
        private void DeleteDatabase<T>(T input)
        {
            _db.Entry(input).State = EntityState.Deleted;
        }
        private void CreateDatabase<T>(T input)
        {
            _db.Entry(input).State = EntityState.Added;
        }
        private async Task SaveChangeAsync()
        {
            await _db.SaveChangesAsync();
        }
        private void SaveChange()
        {
            _db.SaveChanges();
        }

        public string CheckBeforeSave(JObject frmData, ref Notification _message, bool isUpdate) // hàm đăng nhập  
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


      
        public Response GetsSelectBoxCar(long fromDate , long toDate,string idTour)
        {
            try
            {
                var unixTimeOneDay = 86400000;

                var listCarShouldRemove1 = (from x in _db.Schedules.AsNoTracking()
                                         where x.TourId == idTour
                                         && (fromDate >= x.DepartureDate && fromDate <= (x.ReturnDate + 86400000))
                                         orderby x.ReturnDate ascending
                                         select x.CarId);

                var scheduleDepartDateLargerToDate = (from x in _db.Schedules.AsNoTracking()
                                           where x.TourId == idTour
                                           && x.DepartureDate >= fromDate
                                           orderby x.DepartureDate ascending
                                           select x);
                var listCarShouldRemove2 = (from x in scheduleDepartDateLargerToDate
                            where !(from s in listCarShouldRemove1 select s).Contains(x.CarId)
                            && (toDate + 86400000) > x.ReturnDate
                            select x.CarId).Distinct();

                var listShouldRemove = listCarShouldRemove1.Concat(listCarShouldRemove2);

                var listCar = (from x in _db.Cars.AsNoTracking()
                               where !listShouldRemove.Any(c => c == x.IdCar)
                               select x).ToList();
                if (listCar.Count() == 0)
                {
                    return Ultility.Responses("Ngày bạn chọn hiện tại không có xe !", Enums.TypeCRUD.Warning.ToString());
                }
                var result = Mapper.MapCar(listCar);
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }

        public Response Gets()
        {
            try
            {
          

                var listCar = (from x in _db.Cars.AsNoTracking() select x).ToList();
                var result = Mapper.MapCar(listCar);
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), result);
            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }
        public Response Create(CreateCarViewModel input)
        {
            try
            {
                Car car = new Car();
                car = Mapper.MapCreateCar(input);
                CreateDatabase<Car>(car);

                UpdateDatabase<Car>(car);
                SaveChange();
                return Ultility.Responses("Thêm thành công !", Enums.TypeCRUD.Success.ToString());
            }
            catch (Exception e)
            {

                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);
            }
        }

        public Response StatisticCar()
        {
            try
            {
                var lsCarFree = (from x in _db.Cars.AsNoTracking()
                                 where x.Status == (int)Enums.StatusCar.Free
                                 select x).ToList();
                var lsCarBusy = (from x in _db.Cars.AsNoTracking()
                                 where x.Status == (int)Enums.StatusCar.Busy
                                 select x).ToList();

                var lsCarFull = (from x in _db.Cars.AsNoTracking()
                                 where x.Status == (int)Enums.StatusCar.Busy
                                   select x).ToList();

                var lsResult = lsCarFree.Concat(lsCarBusy).Concat(lsCarFull);
                return Ultility.Responses("", Enums.TypeCRUD.Success.ToString(), lsResult);

            }
            catch (Exception e)
            {
                return Ultility.Responses("Có lỗi xảy ra !", Enums.TypeCRUD.Error.ToString(), description: e.Message);

            }
        }
    }
}
