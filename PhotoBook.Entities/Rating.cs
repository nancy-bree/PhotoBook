﻿using System.ComponentModel.DataAnnotations;

namespace PhotoBook.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Rating
    {
        [Key]
        public int ID { get; set; }

        public int PhotoID { get; set; }

        public int UserID { get; set; }

        public byte Like { get; set; }

        public byte Dislike { get; set; }
    }
}