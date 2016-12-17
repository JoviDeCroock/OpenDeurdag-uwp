using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RondleidingAPI.Models.Domain
{
    public class StudentTraining
    {
        [Key, Column(Order = 0)]
        public int StudentId { get; set; }
        [Key, Column(Order = 1)]
        public int TrainingId { get; set; }
        public StudentTraining() { }
    }
}