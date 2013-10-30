using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoBook.Models
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

        public ICollection<Photo> Photos { get; set; }
    }
}