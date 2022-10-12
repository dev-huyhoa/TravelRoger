using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel.TourVM;

namespace Travel.Data.Interfaces
{
   public  interface ITour
    {
        CreateTourViewModel CheckBeforSave(JObject frmData, ref Notification _message);
        Response Create(CreateTourViewModel input);

    }
}
