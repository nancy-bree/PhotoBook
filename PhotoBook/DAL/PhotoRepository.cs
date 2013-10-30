using System;
using System.Collections.Generic;
using System.Linq;
using PhotoBook.Models;

namespace PhotoBook.DAL
{
    public class PhotoRepository : Repository<Photo>
    {
        public PhotoRepository(PhotoBookContext context) : base(context) { }
    }
}
