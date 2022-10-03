﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Context.Models
{
    public class Banner
    {
        private Guid id;
        private Guid idImage;
        private bool isActive;

        public Guid Id { get => id; set => id = value; }
        public Guid IdImage { get => idImage; set => idImage = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
    }
}
