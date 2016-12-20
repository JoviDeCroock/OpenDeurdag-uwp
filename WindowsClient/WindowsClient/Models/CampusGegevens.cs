using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsClient.Models
{
    class CampusGegevens
    {
        public string name { get; set; }
        public List<string> campussen { get; set; }
        public List<List<string>> aantallen { get; set; }
        public List<Training> trainings { get; set; }
        public List<string> provincies { get; set; }
        public List<string> aantalStuds { get; set; }
    }
}
