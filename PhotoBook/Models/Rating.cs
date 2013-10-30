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
    public class Rating
    {
        [Key]
        public int ID { get; set; } // ???

        public int PhotoID { get; set; }

        public int UserID { get; set; }

        public byte Vote { get; set; }
    }
}