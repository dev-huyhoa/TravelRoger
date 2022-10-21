using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Shared.ViewModels.Travel.CostTourVM
{
    public class UpdateCostViewModel:CreateCostViewModel
    {

    }
    public class CreateCostViewModel
    {
        private string idCostTour;
        private float breakfast;
        private float water;
        private float feeGas;
        private float distance;
        private float sellCost;
        private float depreciation;
        private float otherPrice;
        private float tolls;
        private int cusExpected;
        private float insuranceFee;
        private float totalCostTour;
        private Guid hotelId;
        private Guid restaurantId;
        private Guid placeId;

        public string IdCostTour { get => idCostTour; set => idCostTour = value; }
        public float Breakfast { get => breakfast; set => breakfast = value; }
        public float Water { get => water; set => water = value; }
        public float FeeGas { get => feeGas; set => feeGas = value; }
        public float Distance { get => distance; set => distance = value; }
        public float SellCost { get => sellCost; set => sellCost = value; }
        public float Depreciation { get => depreciation; set => depreciation = value; }
        public float OtherPrice { get => otherPrice; set => otherPrice = value; }
        public float Tolls { get => tolls; set => tolls = value; }
        public int CusExpected { get => cusExpected; set => cusExpected = value; }
        public float InsuranceFee { get => insuranceFee; set => insuranceFee = value; }
        public float TotalCostTour { get => totalCostTour; set => totalCostTour = value; }
        public Guid HotelId { get => hotelId; set => hotelId = value; }
        public Guid RestaurantId { get => restaurantId; set => restaurantId = value; }
        public Guid PlaceId { get => placeId; set => placeId = value; }
    }
}
