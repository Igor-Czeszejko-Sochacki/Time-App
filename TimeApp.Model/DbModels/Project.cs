using System;
using System.Collections.Generic;
using System.Text;

namespace TimeApp.Model.DbModels
{
    public class Project : Entity
    {
        public string Name { get; set; }
        public int WorkedHours { get; set; }
        public int WeekId { get; set; }
        public Week Week { get; set; }
    }
}
