using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TimeApp.Model.DbModels;

namespace TimeApp.Model
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }  
        public bool IsActive { get; set; }
        [JsonIgnore]
        public string Token { get; set; }
        [JsonIgnore]
        public List<Raports> Raports { get; set; }
    }
}
