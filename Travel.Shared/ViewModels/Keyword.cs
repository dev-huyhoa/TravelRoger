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
        public List<string> KwTypeActions { get; set; }
        public string KwAddress { get; set; }
        public string KwPriceTicket { get; set; }
        public string KwComboPrice{ get; set; }
        public string KwPincode { get; set; }
        public bool kwIsCalled { get; set; }
        public long KwDate { get; set; }
        public long KwFromDate { get; set; }
        public long KwToDate { get; set; }
        public string KwToPlace { get; set; }
        public double KwRating { get; set; }
        public string KwTypeAction { get; set; }
        public int KwAprroveStatus { get; set; }

        public float KwTotalCostTourNotService { get; set; }
        public float KwFinalPrice { get; set; }
        public float KwFinalPriceHoliday { get; set; }

        public string KwIdTour { get; set; }
    }
}
