using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TimeApp.Model.DbModels
{
    public class Project : Entity
    {
        public string Name { get; set; }
        public int WorkedHours { get; set; }
        public int WeekId { get; set; }
        [JsonIgnore]
        public Week Week { get; set; }
    }
}
