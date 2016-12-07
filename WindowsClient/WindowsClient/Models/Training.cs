using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsClient.Models
{
    class Training
    {
        public Training() { }
        string name;
        IList<string> campus;
        /*TODO: add more attributes (e.g. descriptions, ...) */

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public IList<string> Campus
        {
            get { return campus; }
            set { campus = value; }
        }
    }
}
