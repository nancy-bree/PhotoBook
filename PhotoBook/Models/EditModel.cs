using System.ComponentModel.DataAnnotations;

namespace PhotoBook.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class EditModel
    {
        public int ID { get; set; }

        public string Photo { get; set; }

        public string Description { get; set; }

        public string Tags { get; set; }

        [Required]
        public Effect Effect { get; set; }
    }
}