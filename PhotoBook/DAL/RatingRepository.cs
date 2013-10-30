using System;
using System.Collections.Generic;
using System.Linq;
using PhotoBook.Models;

namespace PhotoBook.DAL
{
    public class RatingRepository : Repository<RatingRepository>
    {
        public RatingRepository(PhotoBookContext context) : base(context) { }
    }
}
