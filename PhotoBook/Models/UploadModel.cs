using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using PhotoBook.Services;

namespace PhotoBook.Models
{
    public class UploadModel
    {
        [Required]
        [FileSize(10240000)]
        [FileTypes("jpg,jpeg,png")]
        public HttpPostedFileBase Photo { get; set; }

        public string Description { get; set; }

        public string Tags { get; set; }
    }
}