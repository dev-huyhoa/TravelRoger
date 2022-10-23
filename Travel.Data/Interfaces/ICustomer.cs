using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Shared.ViewModels;
using Travel.Shared.ViewModels.Travel.CustomerVM;

namespace Travel.Data.Interfaces
{
    public interface ICustomer
    {
        string CheckBeforeSave(JObject frmdata, ref Notification _message, bool isUpdate);
        Response Gets();
        Response Create(CreateCustomerViewModel input);
        Response GetsHistory(Guid idCustomer);
        Response GetCustomer(Guid idCustomer);
        Response UpdateCustomer(UpdateCustomerViewModel input);
    }
}
