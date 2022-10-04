﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Travel.Context.Models
{
    
    public class Car
    {
        private Guid id;
        private string liscensePlate;
        private int status;
        private int amountSeat;
        private string nameDriver;
        private string phone;
        private Guid idEmployee;

        public Employee Employees { get; set; }
        private ICollection<Schedule> schedules;

        public Guid Id { get => id; set => id = value; }
        public string LiscensePlate { get => liscensePlate; set => liscensePlate = value; }
        public int Status { get => status; set => status = value; }
        public int AmountSeat { get => amountSeat; set => amountSeat = value; }
        public ICollection<Schedule> Schedules { get => schedules; set => schedules = value; }
        public string NameDriver { get => nameDriver; set => nameDriver = value; }
        public string Phone { get => phone; set => phone = value; }
        public Guid IdEmployee { get => idEmployee; set => idEmployee = value; }
    }
}
