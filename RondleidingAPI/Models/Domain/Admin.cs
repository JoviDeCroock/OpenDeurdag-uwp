using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RondleidingAPI.Models
{
    public class Admin : User
    {
        public int AdminId { get; set; }
        public string wachtwoord { get; set; }
    }
}