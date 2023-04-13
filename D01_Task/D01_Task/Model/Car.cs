using D01_Task.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_D02_Task2.Models
{
    public class Car
    {
        public int id { get; set; }
        public string Color { get; set; }
        public string Model { get; set; }
        [DateInPast]
        public DateTime ProductionDate { get; set; }
        public string Type { get; set; }

    }
}