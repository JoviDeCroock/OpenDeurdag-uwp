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
        public virtual IList<Campus> PrefCampus { get; set; }
        public virtual IList<Training> PrefTraining { get; set; }
        public Student() { }
        public Student(string street, string houseNumber, string province, string city, string name, string email)
        {
            Street = street;
            HouseNumber = houseNumber;
            Province = province;
            City = city;
            Name = name;
            Email = email;
            PrefCampus = new List<Campus>();
            PrefTraining = new List<Training>();
        }

        public void addCampus(Campus c)
        {
            PrefCampus.Add(c);
        }
        public void removeCampus(string name)
        {
            Campus c = PrefCampus.Where(campus => campus.Name == name).FirstOrDefault();
            if (c != null)
                PrefCampus.Remove(c);
        }

        public void addTraining(Training t)
        {
            PrefTraining.Add(t);
        }
        public void removeTraining(string name)
        {
            Training t = PrefTraining.Where(training => training.Name == name).FirstOrDefault();
            if (t != null)
                PrefTraining.Remove(t);
        }

    }
}