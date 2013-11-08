using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhotoBook.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Tag
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Name { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }
}