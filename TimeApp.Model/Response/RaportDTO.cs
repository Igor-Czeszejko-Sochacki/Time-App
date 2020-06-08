using System;
using System.Collections.Generic;
using System.Text;
using TimeApp.Model.DbModels;

namespace TimeApp.Model.Response
{
    public class RaportDTO
    {
        public int Id { get; set; }
        public string Emali { get; set; }
        public string User { get; set; }
        public string Month { get; set; }
        public string HoursInMonth { get; set; }
        public string WorkedHours { get; set; }
        public List<Project> ProjetList { get; set; }
        public List<Week> WeekList { get; set; }
        public bool IsClosed { get; set; }
        public bool IsAccepted { get; set; }
        
    }
}
