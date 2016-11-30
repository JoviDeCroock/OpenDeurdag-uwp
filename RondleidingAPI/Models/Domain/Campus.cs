using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RondleidingAPI.Models
{
    public class Campus
    {
        public int CampusId { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string Telephone { get; set; }
        public virtual IList<Training> Trainingen { get; set; }
        public Campus() { Trainingen = new List<Training>(); }
        public Campus(string name, string city, string street, string houseNumber, string telephone)
        {
            Trainingen = new List<Training>();
            Name = name;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            Telephone = telephone;
        }
    }
}