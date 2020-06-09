using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TimeApp.Model.DbModels
{
    public class Raports : Entity
    {
        public string Month { get; set; }
        public int HoursInMonth { get; set; } = 120;
        public int WorkedHours { get; set; } = 0;
        public bool IsClosed { get; set; } = false;
        public bool IsAccepted { get; set; } = false;
        [JsonIgnore]
        public List<Week> Weeks { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
