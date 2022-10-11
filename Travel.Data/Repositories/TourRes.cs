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
using Travel.Shared.ViewModels.Travel.TourVM;

namespace Travel.Data.Repositories
{
    public class TourRes : ITourRes
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
        public CreateTourViewModel CheckBeforSave(JObject frmData, ref Notification _message)
        {
            CreateTourViewModel tour = new CreateTourViewModel();

            try
            {

                var tourName = PrCommon.GetString("NameTour", frmData);
                if (String.IsNullOrEmpty(tourName))
                {
                }
                if (isExistName(tourName))
                {
                }
                var thumbSnail = PrCommon.GetString("Thumbsnail", frmData);
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
                // map data
                tour.NameTour = tourName;
                tour.Thumbsnail = thumbSnail;
                tour.FromPlace = fromPlace;
                tour.ToPlace = toPlace;
                // generate ID
                tour.IdTour = Ultility.GenerateId(tourName);


                return tour;
            }
            catch (Exception e)
            {
                message.DateTime = DateTime.Now;
                message.Description = e.Message;
                message.Messenge = "Có lỗi xảy ra !";
                message.Type = "Error";

                _message = message;
                return tour;
            }
        }
        public Response Create(CreateTourViewModel input)
        {
            try
            {
                Tour tour = new Tour();
                tour = Mapper.MapCreateTour(input);
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
        private bool isExistName(string input)
        {
            try
            {
                string nameTour = Ultility.removeVietnameseSign(input.ToLower());
                var tour =
                    (from x in _db.Tour
                     where Ultility.removeVietnameseSign(x.NameTour).ToLower() == nameTour select x).FirstOrDefault();
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
