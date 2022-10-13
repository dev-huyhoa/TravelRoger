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
    public interface IPayment
    {
        CreateUpdatePaymentViewModel CheckBeforSave(JObject frmData, ref Notification _message);
        Response Gets();
        Response Create(CreateUpdatePaymentViewModel input);
        Response Update(CreateUpdatePaymentViewModel input);
    }
}
