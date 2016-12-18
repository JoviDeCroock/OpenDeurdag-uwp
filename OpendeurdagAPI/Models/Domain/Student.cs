using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpendeurdagAPI.Models.Domain
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
        public Student() { PrefCampus = new List<Campus>(); PrefTraining = new List<Training>(); }
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
        public void removeCampus(int id)
        {
            /*Beter uitwerken*/
            Campus d = PrefCampus.Where(campus => campus.CampusId == id).FirstOrDefault();
            if (d != null)
                PrefCampus.Remove(d);
        }

        public void addTraining(Training t)
        {
            PrefTraining.Add(t);
        }
        public void removeTraining(int id)
        {
            /*Beter uitwerken*/
            Training t = PrefTraining.Where(training => training.TrainingId == id).FirstOrDefault();
            if (t != null)
                PrefTraining.Remove(t);
        }
    }
}