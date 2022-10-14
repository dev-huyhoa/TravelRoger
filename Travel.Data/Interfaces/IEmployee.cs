using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
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
    public interface IEmployee
    {
        string CheckBeforeSave(IFormCollection frmdata, IFormFile file, ref Notification _message, bool isUpdate);
        Response GetsEmployee(JObject frmData);
        Response UpdateEmployee(UpdateEmployeeViewModel input);
        Response CreateEmployee(CreateEmployeeViewModel input);
        Response SearchEmployee(JObject frmData);
        Response RestoreEmployee(JObject frmData);
        Response DeleteEmployee(JObject frmData);

    }
}
