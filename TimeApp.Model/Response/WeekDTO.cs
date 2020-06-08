using System;
using System.Collections.Generic;
using System.Text;
using TimeApp.Model.DbModels;

namespace TimeApp.Model.Response
{
    public class WeekDTO
    { 
        public int WeekNumber { get; set; }
        public int HoursInWeek { get; set; }
        public int WorkedHours { get; set; }
        public List<ProjectDTO> Projects { get; set; }
    }
}
