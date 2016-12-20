using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpendeurdagAPI.Models.Domain
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public Post() { }
    }
}