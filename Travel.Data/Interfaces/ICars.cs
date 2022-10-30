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
    public interface ICars
    {
        string CheckBeforeSave(JObject frmData, ref Notification _message, bool isUpdate);
        Response Gets();
        Response Create(CreateCarViewModel input);
        Response StatisticCar();
    }
}
