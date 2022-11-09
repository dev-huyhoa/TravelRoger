using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Shared.ViewModels
{
    public class Keywords
    {
        public bool IsDelete { get; set; }
        public string Keyword { get; set; }
        public string KwName { get; set; }
        public string KwId { get; set; }
        public string KwPhone { get; set; }
        public string KwEmail { get; set; }
        public List<int> KwIdRole { get; set; }
        public List<string> KwIdProvince { get; set; }
        public List<string> KwIdDistrict { get; set; }
        public List<string> KwIdWard { get; set; }
        public string KwStatus { get; set; }
        public bool KwIsActive { get; set; }
        public string KwDescription { get; set; }

        public List<int> KwStar{ get; set; }
        public string KwAddress { get; set; }
        public string KwPriceTicket { get; set; }

        public string KwComboPrice{ get; set; }
    }
}
