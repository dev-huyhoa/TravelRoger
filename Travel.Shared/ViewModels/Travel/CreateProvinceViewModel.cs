using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Shared.ViewModels.Travel
{
    public class CreateProvinceViewModel
    {
        private Guid idProvince;
        private string nameProvince;
        public string NameProvince { get => nameProvince; set => nameProvince = value; }
        public Guid IdProvince { get => idProvince; set => idProvince = value; }
    }
}
