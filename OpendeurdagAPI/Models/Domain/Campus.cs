﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpendeurdagAPI.Models.Domain
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
        public virtual IList<Training> Trainingen { get; set; }
        [JsonIgnore]
        public virtual IList<Student> Studenten { get; set; }
        public Campus() { Trainingen = new List<Training>(); Studenten = new List<Student>();}
        public Campus(string name, string city, string street, string houseNumber, string telephone, string feed)
        {
            Feed = feed;
            Studenten = new List<Student>();
            Trainingen = new List<Training>();
            Name = name;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            Telephone = telephone;
        }
        public void addToTraining(Training t)
        {
            Trainingen.Add(t);
        }
        public void addToStudents(Student t)
        {
            Studenten.Add(t);
        }
    }
}