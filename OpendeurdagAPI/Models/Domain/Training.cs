using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpendeurdagAPI.Models.Domain
{
    public class Training
    {
        public int TrainingId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public virtual IList<Campus> Campussen { get; set; }
        public string Feed { get; set; }
        [JsonIgnore]
        public virtual IList<Student> Studenten { get; set; }
        public Training() { Campussen = new List<Campus>(); Studenten = new List<Student>();}

        public Training(string name, string description, string feed)
        {
            Name = name;
            Description = description;
            Feed = feed;
            Campussen = new List<Campus>();
            Studenten = new List<Student>();
        }

        public void addToCampus(Campus t)
        {
            Campussen.Add(t);
        }
        public void addToStudents(Student t)
        {
            Studenten.Add(t);
        }
    }
}