using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Context.Models;
using Travel.Shared.ViewModels.Travel.TourVM;

namespace Travel.Shared.ViewModels.Travel
{
    public class TourViewModel
    {
        private string idTour;
        private string tourName;
        private double rating;
        private string thumbsnail;
        private string fromPlace;
        private string toPlace;

        private string approveStatus;
        private int status;

        private long createDate;

        private string modifyBy;
        private long modifyDate;

        private bool isDelete;
        private bool isActive;


        // client
        private float priceAdult;
        private float priceAdultPromotion;

        private TourDetailViewModel tourDetail;
        private ICollection<ScheduleViewModel> schedules;

        public string TourName { get => tourName; set => tourName = value; }
        public double Rating { get => rating; set => rating = value; }
        public string Thumbsnail { get => thumbsnail; set => thumbsnail = value; }
        public string FromPlace { get => fromPlace; set => fromPlace = value; }
        public string ToPlace { get => toPlace; set => toPlace = value; }
        public string ApproveStatus { get => approveStatus; set => approveStatus = value; }
        public int Status { get => status; set => status = value; }
        public long CreateDate { get => createDate; set => createDate = value; }
        public string ModifyBy { get => modifyBy; set => modifyBy = value; }
        public long ModifyDate { get => modifyDate; set => modifyDate = value; }
        public bool IsDelete { get => isDelete; set => isDelete = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public string IdTour { get => idTour; set => idTour = value; }
        public float PriceAdult { get => priceAdult; set => priceAdult = value; }
        public float PriceAdultPromotion { get => priceAdultPromotion; set => priceAdultPromotion = value; }
        public TourDetailViewModel TourDetail { get => tourDetail; set => tourDetail = value; }
        public ICollection<ScheduleViewModel> Schedules { get => schedules; set => schedules = value; }
    }
}
