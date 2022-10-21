using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Context.Models
{
    public class TourDetail
    {  
        public string IdTourDetail { get; set; }
        public string TourId { get; set; }
        public string Description { get; set; }
        public int QuantityBooked { get; set; }
        public float TotalCostTour { get; set; }
        public int Profit { get; set; }
        public float Vat { get; set; }
        public float FinalPrice { get; set; }
        public float FinalPriceHoliday { get; set; }

        public CostTour CostTour { get; set; }
        public Tour Tour { get; set; }

    }
}
