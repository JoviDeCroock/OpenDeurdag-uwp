using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RondleidingAPI.Models
{
    public class Student : User
    {
        public int StudentId { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public IList<Campus> PrefCampus { get; set; }
        public IList<Training> PrefTraining { get; set; }
    }
}