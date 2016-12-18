using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsClient.Utils
{
    public class RouteImage
    {
        public RouteImage() { }

        public RouteImage(string uri)
        {
            Path = uri;
        }

        public string Path { get; set; }
    }
}
