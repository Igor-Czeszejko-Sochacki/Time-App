﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TimeApp.Model.DbModels
{
    public class Week : Entity
    {
        public int WeekNumber { get; set; }
        public int HoursInWeek { get; set; }
        public int WorkedHours { get; set; }
        public List<Project> Projects { get; set; }
        public int RaportId { get; set; }
        public Raports Raport { get; set; }
    }
}
