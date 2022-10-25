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
        CreateCarViewModel CheckBeforeSave(JObject frmData, ref Notification _message);
        Response Gets();
        Response Create(CreateCarViewModel input);

        Response GetCarStatus();

    }
}
