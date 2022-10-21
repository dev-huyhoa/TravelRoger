﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.Context.Models
{
    public class Schedule
    {
        public string IdSchedule { get; set; }
        public string Alias { get; set; }
        public long DepartureDate { get; set; }
        public long BeginDate { get; set; }
        public long EndDate { get; set; }
        public long TimePromotion { get; set; }
        public int Status { get; set; }
        public int Approve { get; set; }
        public float QuantityAdult { get; set; }
        public float QuantityBaby { get; set; }
        public int MinCapacity { get; set; }
        public int MaxCapacity { get; set; }
        public float QuantityChild { get; set; }
        public bool Isdelete { get; set; }
        public bool IsHoliday { get; set; }

        public string TourId { get; set; }
        public Guid CarId { get; set; }
        public int PromotionId { get; set; }
        public Guid EmployeeId { get; set; }
        public Car Car { get; set; }
        public Promotion Promotions { get; set; }
        public Tour Tour { get; set; }
        public ICollection<Timeline> Timelines { get; set; }
        public ICollection<Tourbooking> TourBookings { get; set; }
        public Employee Employee { get; set; }
        
    }
}
