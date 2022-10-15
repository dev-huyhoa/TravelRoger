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
        private Guid   costTourId ;
        private float  priceChild ;
        private float  priceBaby ;
        private float  priceAdult ;
        private float  priceChildPromotion ;
        private float  priceBabyPromotion ;
        private float  priceAdultPromotion ;
        private string description ;
        private int    quantityBooked ;
        private bool   isPromotion ;
        private float  totalCostTour ;
        private int    profit ;
        private float  vat ;
        private float  finalPrice ;

        public string IdTourDetail { get => idTourDetail; set => idTourDetail = value; }
        public string TourId { get => tourId; set => tourId = value; }
        public Guid CostTourId { get => costTourId; set => costTourId = value; }
        public float PriceChild { get => priceChild; set => priceChild = value; }
        public float PriceBaby { get => priceBaby; set => priceBaby = value; }
        public float PriceAdult { get => priceAdult; set => priceAdult = value; }
        public float PriceChildPromotion { get => priceChildPromotion; set => priceChildPromotion = value; }
        public float PriceBabyPromotion { get => priceBabyPromotion; set => priceBabyPromotion = value; }
        public float PriceAdultPromotion { get => priceAdultPromotion; set => priceAdultPromotion = value; }
        public string Description { get => description; set => description = value; }
        public int QuantityBooked { get => quantityBooked; set => quantityBooked = value; }
        public bool IsPromotion { get => isPromotion; set => isPromotion = value; }
        public float TotalCostTour { get => totalCostTour; set => totalCostTour = value; }
        public int Profit { get => profit; set => profit = value; }
        public float Vat { get => vat; set => vat = value; }
        public float FinalPrice { get => finalPrice; set => finalPrice = value; }
    }
}
