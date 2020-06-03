using System;
using System.Collections.Generic;
using System.Text;

namespace TimeApp.Model.DbModels
{
    public class Raports : Entity
    {
        public int HoursInMonth { get; set; } = 120;
        public int WorkedHours { get; set; } = 0;
        public bool IsClosed { get; set; } = false;
        public bool IsAccepted { get; set; } = false;
        public List<Week> Weeks { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
