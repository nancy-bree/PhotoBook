using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoBook.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Photo
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Filename { get; set; }

        public string Description { get; set; }

        [Required]
        public int UserID { get; set; }

        public int Effect { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}