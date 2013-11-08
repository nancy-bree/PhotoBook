using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoBook.Models
{
    public class AlbumViewModel
    {
        public int ID { get; set; }

        public string Username { get; set; }

        public string Cover { get; set; }

        public int Count { get; set; }
    }
}