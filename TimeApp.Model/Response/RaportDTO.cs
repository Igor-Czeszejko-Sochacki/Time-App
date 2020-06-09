using System;
using System.Collections.Generic;
using System.Text;
using TimeApp.Model.DbModels;
namespace TimeApp.Model.Response
{
    public class RaportDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string User { get; set; }
        public string Month { get; set; }
        public int HoursInMonth { get; set; }
        public int WorkedHours { get; set; }
        public List<ProjectDTO> ProjetList { get; set; }
        public List<WeekDTO> WeekList { get; set; }
        public bool IsClosed { get; set; }
        public bool IsAccepted { get; set; }
        
    }
}
