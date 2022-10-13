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
    public interface ILocation
    {
        string CheckBeforeSaveProvince(JObject frmData, ref Notification _message, bool isUpdate);
        District CheckBeforeSaveDistrict(JObject frmData, ref Notification _message);
        Ward CheckBeforeSaveWard(JObject frmData, ref Notification _message);
        Response GetsProvince();
        Response GetsDistrict();
        Response GetsWard();
        Response SearchProvince(JObject frmData);
        Response SearchDistrict(JObject frmData);
        Response SearchWard(JObject frmData);
        Response CreateProvince(CreateProvinceViewModel province);
        Response CreateDistrict(District district);
        Response CreateWard(Ward ward);
        Response UpdateProvince(UpdateProvinceViewModel province);
        Response UpdateDistrict(District district);
        Response UpdateWard(Ward ward);
        Response DeleteProvince(JObject frmData);
        Response DeleteDistrict(District district);
        Response DeleteWard(Ward ward);
    }
}
