using System;
using System.Collections.Generic;
using System.Text;

namespace TimeApp.Model.Request
{
    public class UserWithoutIdVM
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }

    }
}
