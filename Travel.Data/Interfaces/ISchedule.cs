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
        Response Get();
        Response Create(CreateScheduleViewModel input);
        Response Delete(string idSchedule);
        Response RestoreShedule(string idSchedule);
        Response UpdatePromotion(string idSchedule, int idPromotion);
        Task<string> UpdateCapacity(string idSchedule, int adult = 1, int child = 0, int baby = 0);
    }
}
