using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RondleidingAPI.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        /*public String self
        {
            get {
                return string.Format(CultureInfo.CurrentCulture,
               "api/contacts/{0}", this.ContactId);
            }
        }*/
    }
}
