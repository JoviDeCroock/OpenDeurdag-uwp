using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Windowsclient.Models
{
    public class Admin : User
    {
        public int AdminId { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public Admin() { }
    }
}