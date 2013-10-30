using System;
using System.Collections.Generic;
using System.Linq;
using PhotoBook.Models;

namespace PhotoBook.DAL
{
    public class TagRepository : Repository<Tag>
    {
        public TagRepository(PhotoBookContext context) : base(context) { }
    }
}
