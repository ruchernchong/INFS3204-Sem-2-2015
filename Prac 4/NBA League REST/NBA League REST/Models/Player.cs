using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBA_League_REST.Models
{
    public class Player
    {
        public string RegistrationID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Team_Name { get; set; }
        public DateTime DOB { get; set; }
    }
}