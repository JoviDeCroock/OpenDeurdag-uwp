using RondleidingAPI.Models.Domain;
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
        public string Feed { get; set; }
        public virtual IList<TrainingCampus> Trainingen { get; set; }
        public virtual IList<StudentCampus> Studenten { get; set; }
        public Campus() { Trainingen = new List<TrainingCampus>(); Studenten = new List<StudentCampus>(); }
        public Campus(string name, string city, string street, string houseNumber, string telephone, string feed)
        {
            Feed = feed;
            Studenten = new List<StudentCampus>();
            Trainingen = new List<TrainingCampus>();
            Name = name;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            Telephone = telephone;
        }
        public void addToTraining(TrainingCampus t)
        {
            Trainingen.Add(t);
        }
        public void addToStudents(StudentCampus t)
        {
            Studenten.Add(t);
        }
    }
}