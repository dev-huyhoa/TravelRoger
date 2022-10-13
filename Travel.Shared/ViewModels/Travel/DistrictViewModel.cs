using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Shared.ViewModels.Travel
{
    public class DistrictViewModel
    {
        private Guid idDistrict;
        private string nameDistrict;

        private Guid idProvince;
        private string provinceName;

        public Guid IdDistrict { get => idDistrict; set => idDistrict = value; }
        public string NameDistrict { get => nameDistrict; set => nameDistrict = value; }
        public Guid IdProvince { get => idProvince; set => idProvince = value; }
        public string ProvinceName { get => provinceName; set => provinceName = value; }
    }
}
