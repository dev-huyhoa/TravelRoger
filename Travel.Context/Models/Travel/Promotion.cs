﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.Context.Models
{
    public class Promotion
    {

        private Guid id;
        private int value;
        private string idSchedule;
        private long toDate;
        private long FromDate;

        public Guid Id { get => id; set => id = value; }
        public int Value { get => value; set => this.value = value; }
        public string IdSchedule { get => idSchedule; set => idSchedule = value; }
        public long ToDate { get => toDate; set => toDate = value; }
        public long FromDate1 { get => FromDate; set => FromDate = value; }
    }
}
