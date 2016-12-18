using System;
using System.Collections.Generic;
using System.Linq;


namespace Windowsclient.Models
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public Post() { }
    }
}