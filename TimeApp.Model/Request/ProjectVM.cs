using System;
using System.Collections.Generic;
using System.Text;

namespace TimeApp.Model.Request
{
    public class ProjectVM
    {
        public string Name { get; set; }
        public int WorkedHours { get; set; }
        public int WeekId { get; set; }
    }
}
