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
using Travel.Shared.ViewModels.Travel.TourVM;

namespace Travel.Data.Repositories
{
    public class TourRes : ITour
    {
        private readonly TravelContext _db;
        private Notification message;
        private Response res;
        public TourRes(TravelContext db)
        {
            _db = db;
            message = new Notification();
            res = new Response();
        }
        public string CheckBeforSave(JObject frmData, ref Notification _message, bool isUpdate)
        {
            try
            {

                var tourName = PrCommon.GetString("nameTour", frmData);
                if (String.IsNullOrEmpty(tourName))
                {
                }
                if (isExistName(tourName))
                {
                }
                var thumbSnail = PrCommon.GetString("thumbsnail", frmData);
                if (String.IsNullOrEmpty(thumbSnail))
                {
                }
                var fromPlace = PrCommon.GetString("fromPlace", frmData);
                if (String.IsNullOrEmpty(thumbSnail))
                {

                }
                var toPlace = PrCommon.GetString("toPlace", frmData);
                if (String.IsNullOrEmpty(thumbSnail))
                {
                }
                var description = PrCommon.GetString("description", frmData);
                if (String.IsNullOrEmpty(description))
                {
                }
                var vat = PrCommon.GetString("vat", frmData) ?? "0";
                if (isUpdate)
                {
                    // map data
                    UpdateTourViewModel objUpdate = new UpdateTourViewModel();
                    objUpdate.NameTour = "tentoatuspa";
                    objUpdate.Thumbsnail = thumbSnail;
                    objUpdate.FromPlace = fromPlace;
                    objUpdate.ToPlace = toPlace;
                    objUpdate.Description = description;
                    objUpdate.VAT = Convert.ToInt16(vat);
                    // generate ID
                    objUpdate.IdTour = Ultility.GenerateId(tourName);
                    return JsonSerializer.Serialize(objUpdate);
                }
                // map data
                CreateTourViewModel obj = new CreateTourViewModel();

                obj.NameTour = tourName;
                obj.Thumbsnail = thumbSnail;
                obj.FromPlace = fromPlace;
                obj.ToPlace = toPlace;
                obj.Description = description;
                obj.VAT = Convert.ToInt16(vat);
                // generate ID
                obj.IdTour = Ultility.GenerateId(tourName);

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
        public Response Create(CreateTourViewModel input)
        {
            try
            {
                Tour tour =
                tour = Mapper.MapCreateTour(input);
                TourDetail tourDetail = Mapper.MapCreateTourDetails(input);
                tour.TourDetail = tourDetail;
                tour.IsDelete = false;
                _db.Tour.Add(tour);
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

        public Response Delete(string idTour)
        {
            try
            {
                var tour = _db.Tour.Find(idTour);
                if (tour != null)
                {
                    tour.IsDelete = true;
                    _db.SaveChanges();

                    res.Notification.DateTime = DateTime.Now;
                    res.Notification.Messenge = "Xóa thành công !";
                    res.Notification.Type = "Success";
                }
                else
                {
                    res.Notification.DateTime = DateTime.Now;
                    res.Notification.Messenge = "Không tìm thấy !";
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

        public Response Get()
        {
            try
            {
                var list = (from x in _db.Tour where x.IsDelete == false 
                            where x.ApproveStatus == Convert.ToInt16(Enums.ApproveStatus.Approved) select x).ToList();
                var result = Mapper.MapTour(list);
                if (list.Count()>0)
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

        public Response GetWaiting()
        {
            try
            {
             
                var list = (from x in _db.Tour where(x.IsDelete == false)
                            where(x.ApproveStatus == Convert.ToInt16(Enums.ApproveStatus.Waiting)) select x ).ToList();
                var result = Mapper.MapTour(list);
                if (list.Count() > 0)
                {
                    res.Content = result;
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

        public Response Approve(JObject frmData)
        {
            try
            {
                var id = PrCommon.GetString("idTour", frmData);
                if (String.IsNullOrEmpty(id))
                {
                }
                var typeApprove = PrCommon.GetString("typeApprove", frmData);

                if (String.IsNullOrEmpty(typeApprove))
                {
                }
                var tour = (from x in _db.Tour where x.IdTour == id select x).First();
                tour.ApproveStatus = Convert.ToInt16(typeApprove);
                _db.SaveChanges();
                res.Content = $"Thao tác {tour.NameTour} thành công";
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








        public Response RestoreTour(string idTour)
        {
            try
            {
                var tour = _db.Tour.Find(idTour);
                if (tour != null)
                {
                    tour.IsDelete = false;
                    _db.SaveChanges();

                    res.Notification.DateTime = DateTime.Now;
                    res.Notification.Messenge = "Khôi phục thành công !";
                    res.Notification.Type = "Success";
                }
                else
                {
                    res.Notification.DateTime = DateTime.Now;
                    res.Notification.Messenge = "Không tìm thấy !";
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

        private bool isExistName(string input)
        {
            try
            {
                var tour =
                    (from x in _db.Tour
                     where x.NameTour.ToLower() == input.ToLower()
                     select x).FirstOrDefault();
                if (tour != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
