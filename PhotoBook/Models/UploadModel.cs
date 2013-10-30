using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PhotoBook.Web.Models
{
    public class UploadModel
    {
        [Required]
        public HttpPostedFileBase Photo { get; set; }

        public string Description { get; set; }
    }
}