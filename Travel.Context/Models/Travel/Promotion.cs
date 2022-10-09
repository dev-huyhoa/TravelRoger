﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.Context.Models
{
    public class Promotion
    {

        public Guid IdPromotion { get; set; }
        public int Value { get; set; }
        public string IdSchedule { get; set; }
        public long ToDate { get; set; }
        public long FromDate { get; set; }
        public virtual Schedule Schedules { get; set; }
    }
}
