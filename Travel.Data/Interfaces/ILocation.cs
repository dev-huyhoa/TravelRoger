using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Context.Models;
using Travel.Shared.ViewModels;

namespace Travel.Data.Interfaces
{
    public interface ILocation
    {
        Province CheckBeforeSaveProvince(JObject frmData, ref Notification _message);
        District CheckBeforeSaveDistrict(JObject frmData, ref Notification _message);
        Ward CheckBeforeSaveWard(JObject frmData, ref Notification _message);
        Response GetsProvince();
        Response GetsDistrict();
        Response GetsWard();
        Response SearchProvince(JObject frmData);
        Response SearchDistrict(JObject frmData);
        Response SearchWard(JObject frmData);
        Response CreateProvince(Province province);
        Response CreateDistrict(District district);
        Response CreateWard(Ward ward);
        Response UpdateProvince(Province province);
        Response UpdateDistrict(District district);
        Response UpdateWard(Ward ward);
        Response DeleteProvince(Province province);
        Response DeleteDistrict(District district);
        Response DeleteWard(Ward ward);
    }
}
