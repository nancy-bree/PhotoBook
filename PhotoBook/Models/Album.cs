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
    public class Album
    {
        //[Key]
        public int ID { get; set; }

        //[Required]
        //[StringLength(256, MinimumLength = 1)]
        public string Name { get; set; }

        //[Required]
        public int UserID { get; set; }

        public ICollection<Photo> Photos { get; set; }
    }
}