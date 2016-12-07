using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RondleidingAPI.Models.Domain
{
    public class TrainingCampus
    {
        [Key, Column(Order=0)]
        public int TrainingsId { get; set; }
        [Key, Column(Order = 1)]
        public int CampusId { get; set; }
        public virtual Training Training { get; set; }
        public virtual Campus Campus { get; set; }

        public TrainingCampus()
        {

        }
    }
}