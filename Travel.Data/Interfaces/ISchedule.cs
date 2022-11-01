using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel;

namespace Travel.Data.Interfaces
{
    public interface ISchedule
    {
        string CheckBeforSave(JObject frmData, ref Notification _message, bool isUpdate = false);
        Response Gets();
        Response Create(CreateScheduleViewModel input);
        Response Update(UpdateScheduleViewModel input);
        Response Delete(string idSchedule);
        Response RestoreShedule(string idSchedule);
        Response UpdatePromotion(string idSchedule, int idPromotion);
        Task<string> UpdateCapacity(string idSchedule, int adult = 1, int child = 0, int baby = 0);
        Response GetsSchedulebyIdTour(string idTour, bool isDelete);

        Response CusGetsSchedulebyIdTour(string idTour);
        Response GetSchedulebyIdTourWaiting(string idTour);
        Task<Response> Get(string idSchedule);
        Task<Response> SearchTour(string? from, string? to, DateTime? departureDate, DateTime? returnDate);


        Task<Response> GetsScheduleFlashSale();
        Task<Response> GetsRelatedSchedule(string idSchedule);
    }
}
