using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PhotoBook.Entities;

namespace PhotoBook.Models
{
    public class PhotoViewModel
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Filename { get; set; }

        public string Description { get; set; }

        public int Rating { get; set; }

        [Required]
        public int UserID { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}