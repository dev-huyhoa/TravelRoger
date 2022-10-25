using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel.TourVM;
using static Travel.Shared.Ultilities.Enums;

namespace Travel.Data.Interfaces
{
   public  interface ITour
    {
        string CheckBeforSave(JObject frmData, ref Notification _message, bool isUpdate = false);
        Response Create(CreateTourViewModel input);
        Response Get();
        Response GetWaiting();
        Response GetTour(string idTour);
        Response Delete(string idTour);
        Response RestoreTour(string idTour);
        Response Approve(JObject frmData);
        Task<Response> GetsTourWithSchedule();

    }
}
