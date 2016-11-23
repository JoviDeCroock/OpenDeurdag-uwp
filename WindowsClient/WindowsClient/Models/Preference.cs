using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsClient.Models
{
    class Preference
    {
        string training;
        string campus;

        public string Training
        {
            get { return training; }
            set { training = value; }
        }
        public string Campus
        {
            get { return campus; }
            set { campus = value; }
        }
    }
}
