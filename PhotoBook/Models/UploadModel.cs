using System.Web;
using System.ComponentModel.DataAnnotations;
using PhotoBook.Services;

namespace PhotoBook.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class UploadModel
    {
        [Required]
        [FileSize(10240000)]
        [FileTypes("jpg,jpeg")]
        public HttpPostedFileBase Photo { get; set; }

        public string Description { get; set; }

        public string Tags { get; set; }
    }
}