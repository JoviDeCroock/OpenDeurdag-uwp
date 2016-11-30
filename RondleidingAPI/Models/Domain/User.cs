using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RondleidingAPI.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
