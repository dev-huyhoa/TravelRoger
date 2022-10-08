﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Context.Models;

namespace Travel.Shared.ViewModels.Travel
{
     public class TimeLineViewModel
    {
        private string id;
        private string description;
        private long fromTime;
        private long toTime;
        private string modifyBy;
        private long modifyDate;

        private bool isDelete;

        private string idSchedule;

        private Schedule schedule;
        private long beginDate;
        private long endDate;

        private string idTour;
        private string nameTour;

        public string Id { get => id; set => id = value; }
        public string Description { get => description; set => description = value; }
        public long FromTime { get => fromTime; set => fromTime = value; }
        public long ToTime { get => toTime; set => toTime = value; }
        public string ModifyBy { get => modifyBy; set => modifyBy = value; }
        public long ModifyDate { get => modifyDate; set => modifyDate = value; }
        public bool IsDelete { get => isDelete; set => isDelete = value; }
        public Schedule Schedule { get => schedule; set => schedule = value; }
        public string IdSchedule { get => idSchedule; set => idSchedule = value; }
        public long BeginDate { get => beginDate; set => beginDate = value; }
        public long EndDate { get => endDate; set => endDate = value; }
        public string IdTour { get => idTour; set => idTour = value; }
        public string NameTour { get => nameTour; set => nameTour = value; }
    }
}
