﻿using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace PhotoBook.Models
{
    public class UploadModel
    {
        [Required]
        public HttpPostedFileBase Photo { get; set; }

        public string Description { get; set; }

        public string Tags { get; set; }
    }
}