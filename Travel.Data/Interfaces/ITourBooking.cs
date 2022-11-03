using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Data.Interfaces;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel.TourBookingVM;
namespace Travel.Data.Interfaces
{
   public interface ITourBooking
    {
        string CheckBeforSave(JObject frmData, ref Notification _message, bool isUpdate = false);
        Response Gets();
        Task<Response> Create(CreateTourBookingViewModel input);
        Task<Response> TourBookingById(string idTourbooking);
        Response GetTourBookingFromDateToDate(DateTime? fromDate, DateTime? toDate);
        Response DoPayment(string idTourBooking);
    }
}
