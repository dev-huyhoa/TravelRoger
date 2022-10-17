﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.Context.Models
{
    public class Tour
    {     
        public string IdTour { get; set; }
        public string NameTour { get; set; }
        public string Alias { get; set; }
        public double Rating { get; set; }
        public string FromPlace { get; set; }
        public string ToPlace { get; set; }
        public int ApproveStatus { get; set; }
        public int Status { get; set; }
        public long CreateDate { get; set; }
        public string ModifyBy { get; set; }
        public long ModifyDate { get; set; }
        public bool IsDelete { get; set; }
        public bool IsActive { get; set; }
        public string Thumbsnail { get; set; }
        public float PriceAdult { get; set; }
        public float PriceAdultPromotion { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual TourDetail TourDetail { get; set; }

    }
}
