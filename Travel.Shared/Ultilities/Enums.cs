﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Shared.Ultilities
{
    public  class Enums
    {
        public enum StatusCar
        {
            Broken = -1, // xe đang hư
            Free = 0, // xe đang rảnh
            Busy = 1, // xe đã có tour
        }
        public enum TitleRole
        {
            Admin = -1,
            LocalManager = 1,
            ServiceManager = 2,
            TourManager = 3,
            TourBookingManager = 4,
        }
        public  enum StatusContract{
            Expired = 0,
            Valid = 1,
        }
        public enum ApproveStatus
        {
            Waiting = 0,
            Approved = 1,
            Refused = 2
        }
        public enum TourStatus
        {
            Normal = 0,
            Promotion = 1,
            Refused = 2
        }
    }
}
