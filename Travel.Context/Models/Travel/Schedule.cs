using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.Context.Models
{
    public class Schedule
    {
        public string IdSchedule { get; set; }
        public long DepartureDate { get; set; }
        public long BeginDate { get; set; }
        public long EndDate { get; set; }
        public long TimePromotion { get; set; }
        public int Status { get; set; }
        public float FinalPrice { get; set; }
        public float QuantityAdult { get; set; }
        public float QuantityBaby { get; set; }
        public int MinCapacity { get; set; }
        public int MaxCapacity { get; set; }
        public float QuantityChild { get; set; }
        public string TourId { get; set; }
        public Guid CarId { get; set; }
        public int PromotionId { get; set; }
        public  Guid EmployeeId { get; set; }
        public virtual Car Car { get; set; }
        public virtual Promotion Promotions { get; set; }
        public virtual Tour Tour { get; set; }
        public virtual ICollection<Timeline> Timelines { get; set; }
        public virtual ICollection<Tourbooking> TourBookings { get; set; }
        public virtual Employee Employee { get; set; }
        
    }
}
