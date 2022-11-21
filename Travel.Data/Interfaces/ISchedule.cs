﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Context.Models;
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
        Response Delete(string idSchedule, Guid idUser);
        Response Refused(string idSchedule);
        Response Approve(string idSchedule);
        Response RestoreShedule(string idSchedule, Guid idUser);
        Response UpdatePromotion(string idSchedule, int idPromotion);
        Task UpdateCapacity(string idSchedule, int adult = 1, int child = 0, int baby = 0);
        Response GetsSchedulebyIdTour(string idTour, bool isDelete);

        Response CusGetsSchedulebyIdTour(string idTour);
        Response GetSchedulebyIdTourWaiting(string idTour, Guid idUser, int pageIndex, int pageSize);
        Task<Response> Get(string idSchedule);
        Task<Response> SearchTour(string from, string to, DateTime? departureDate, DateTime? returnDate);


        Task<Response> GetsSchedule();
        Task<Response> GetsScheduleFlashSale();
        Task<Response> GetsSchedulePromotion();
        Task<Response> GetsRelatedSchedule(string idSchedule);
        Response SearchSchedule(JObject frmData, string idTour);

        Response SearchScheduleWaiting(JObject frmData, string idTour);
        Task<Schedule> GetScheduleByIdForPayPal(string idSchedule);
        Task<Response> AutomaticUpdatePromotionForSchedule();
        Task<Response> SearchTourFilter(JObject frmData);
    }
}
