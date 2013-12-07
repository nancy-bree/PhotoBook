using System;
using System.ComponentModel.DataAnnotations;

namespace PhotoBook.Entities
{
    /// <summary>
    /// Describes a Rating entity.
    /// </summary>
    //[Serializable]
    public class Rating
    {
        [Key]
        public int ID { get; set; }

        public int PhotoID { get; set; }

        public int UserID { get; set; }

        public int Vote { get; set; }

        public virtual Photo Photo { get; set; }

        public virtual User User { get; set; }
    }
}