﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Shared.ViewModels
{
    public class Response: Pagination
    {
        public string Content { get; set; }
        public int TotalResult { get; set; }
        public Notification Notification { get; set; } = new Notification();
        
    }
}
