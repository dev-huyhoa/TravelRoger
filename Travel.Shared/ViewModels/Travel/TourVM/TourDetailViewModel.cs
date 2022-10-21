using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Shared.ViewModels.Travel.TourVM
{
    public class TourDetailViewModel
    {
        private string idTourDetail ;
        private string tourId ;
        private string description ;
        private int    quantityBooked ;
        private float  totalCostTour ;
        private int    profit ;
        private float  vat ;
        private float  finalPrice ;
        private float  finalPriceHoliday;

        public string IdTourDetail { get => idTourDetail; set => idTourDetail = value; }
        public string TourId { get => tourId; set => tourId = value; }
        public string Description { get => description; set => description = value; }
        public int QuantityBooked { get => quantityBooked; set => quantityBooked = value; }
        public float TotalCostTour { get => totalCostTour; set => totalCostTour = value; }
        public int Profit { get => profit; set => profit = value; }
        public float Vat { get => vat; set => vat = value; }
        public float FinalPrice { get => finalPrice; set => finalPrice = value; }
        public float FinalPriceHoliday { get => finalPriceHoliday; set => finalPriceHoliday = value; }
    }
}
