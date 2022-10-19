using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Context.Models;

namespace Travel.Shared.ViewModels.Travel
{
    public class ScheduleViewModel
    {
        private string idSchedule;
        private long departureDate;
        private long beginDate;
        private long endDate;
        private long timePromotion;

        private int status;
        private float finalPrice;
        private float quantityAdult;
        private float quantityBaby;
        private int minCapacity;
        private int maxCapacity;
        private float quantityChild;

        private string tourId;
        private string nameTour;

        private Guid carId;
        private string liscensePlate;
        private string nameDriver;

        private Guid employeeId;
        private string nameEmployee;

        private int promotionId;
        private int valuePromotion;

        private Tour tour;
        private ICollection<Timeline> timelines;

        public string IdSchedule { get => idSchedule; set => idSchedule = value; }
        public long DepartureDate { get => departureDate; set => departureDate = value; }
        public long BeginDate { get => beginDate; set => beginDate = value; }
        public long EndDate { get => endDate; set => endDate = value; }
        public long TimePromotion { get => timePromotion; set => timePromotion = value; }
        public int Status { get => status; set => status = value; }
        public float FinalPrice { get => finalPrice; set => finalPrice = value; }
        public float QuantityAdult { get => quantityAdult; set => quantityAdult = value; }
        public float QuantityBaby { get => quantityBaby; set => quantityBaby = value; }
        public int MinCapacity { get => minCapacity; set => minCapacity = value; }
        public int MaxCapacity { get => maxCapacity; set => maxCapacity = value; }
        public float QuantityChild { get => quantityChild; set => quantityChild = value; }
        public string TourId { get => tourId; set => tourId = value; }
        public string NameTour { get => nameTour; set => nameTour = value; }
        public Guid CarId { get => carId; set => carId = value; }
        public string LiscensePlate { get => liscensePlate; set => liscensePlate = value; }
        public string NameDriver { get => nameDriver; set => nameDriver = value; }
        public Guid EmployeeId { get => employeeId; set => employeeId = value; }
        public string NameEmployee { get => nameEmployee; set => nameEmployee = value; }
        public int PromotionId { get => promotionId; set => promotionId = value; }
        public ICollection<Timeline> Timelines { get => timelines; set => timelines = value; }
        public int ValuePromotion { get => valuePromotion; set => valuePromotion = value; }
        public Tour Tour { get => tour; set => tour = value; }
    }
}
