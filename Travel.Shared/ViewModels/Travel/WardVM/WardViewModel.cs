using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Shared.ViewModels.Travel
{
    public class WardViewModel
    {
        private Guid idWard;
        private string nameWard;

        private Guid idDistrict;
        private string districtName;

        public Guid IdWard { get => idWard; set => idWard = value; }
        public string NameWard { get => nameWard; set => nameWard = value; }
        public Guid IdDistrict { get => idDistrict; set => idDistrict = value; }
        public string DistrictName { get => districtName; set => districtName = value; }
    }
}
