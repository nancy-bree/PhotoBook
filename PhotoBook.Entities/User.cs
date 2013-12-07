using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotoBook.Entities
{
    /// <summary>
    /// Describes a User entity.
    /// </summary>
    //[Serializable]
    public class User
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }

        public virtual ICollection<Rating> Votes { get; set; }
    }
}