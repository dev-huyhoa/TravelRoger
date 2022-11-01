﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.Context.Models
{
    public class Promotion
    {

        public int IdPromotion { get; set; }
        public int Value { get; set; }
        public long ToDate { get; set; }
        public long FromDate { get; set; }
        public int Approve { get; set; }
        public  ICollection<Schedule> Schedules { get; set; }
    }
}
