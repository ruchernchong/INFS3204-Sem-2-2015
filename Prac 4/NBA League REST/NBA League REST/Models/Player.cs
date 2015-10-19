using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NBA_League_REST.Models
{
    public class Player
    {
        public String RegistrationID { get; set; }
        public String First_Name { get; set; }
        public String Last_Name { get; set; }
        public String Team_Name { get; set; }
        public DateTime DOB { get; set; }
    }
}