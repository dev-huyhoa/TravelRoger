using Microsoft.AspNetCore.Http;
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
        string CheckBeforSave(IFormCollection frmdata, IFormFile file, ref Notification _message, bool isUpdate = false);
        Response Create(CreateTourViewModel input);
        Response Update(UpdateTourViewModel input);
        Response Get(bool isDelete);
        Response GetWaiting(Guid idUser);
        Response GetTour(string idTour);
        Response Delete(string idTour,Guid idUser);
        Response RestoreTour(string idTour, Guid idUser);
        Response Approve(string id);
        Response Refused(string id);
        Task<Response> GetsTourWithSchedule();
        Task<Response> GetTourById(string idTour);

    }
}
