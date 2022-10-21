using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Context.Models;

namespace Travel.Shared.ViewModels.Travel
{
    public class UpdateScheduleViewModel : CreateScheduleViewModel
    {

    }
    public class CreateScheduleViewModel
    {
        private string idSchedule;
        private long departureDate;
        private long returnDate;
        private long beginDate;
        private long endDate;
        private long timePromotion;
        private int minCapacity;
        private int maxCapacity;
        private float quantityChild;
        private int status;

        private string tourId;
        private Guid carId;
        private Guid employeeId;
        private int promotionId;

        public string IdSchedule { get => idSchedule; set => idSchedule = value; }
        public long DepartureDate { get => departureDate; set => departureDate = value; }
        public long BeginDate { get => beginDate; set => beginDate = value; }
        public long EndDate { get => endDate; set => endDate = value; }
        public long TimePromotion { get => timePromotion; set => timePromotion = value; }
        public float QuantityChild { get => quantityChild; set => quantityChild = value; }
        public int Status { get => status; set => status = value; }
     
        public int MinCapacity { get => minCapacity; set => minCapacity = value; }
        public int MaxCapacity { get => maxCapacity; set => maxCapacity = value; }
        public string TourId { get => tourId; set => tourId = value; }
        public Guid CarId { get => carId; set => carId = value; }
        public Guid EmployeeId { get => employeeId; set => employeeId = value; }
        public int PromotionId { get => promotionId; set => promotionId = value; }
        public long ReturnDate { get => returnDate; set => returnDate = value; }
    }
}
