using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RondleidingAPI.Models
{
    public class Training
    {
        public int TrainingId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual IList<Campus> Campussen { get; set; }
        public Training() { Campussen = new List<Campus>(); }
        public Training(string name, string description)
        {
            Name = name;
            Description = description;
            Campussen = new List<Campus>();
        }
    }
}