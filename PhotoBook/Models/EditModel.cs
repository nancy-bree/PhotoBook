using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoBook.Models
{
    public class EditModel
    {
        [Required]
        public HttpPostedFileBase Photo { get; set; }

        public string Description { get; set; }

        public string Tags { get; set; }
    }
}