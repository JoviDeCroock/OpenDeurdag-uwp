using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RondleidingAPI.Models.Domain
{
    public class StudentCampus
    {
        [Key, Column(Order = 0)]
        public int StudentId { get; set; }
        [Key, Column(Order = 1)]
        public int CampusId { get; set; }
        public virtual Student Student { get; set; }
        public virtual Campus Campus { get; set; }
    }
}