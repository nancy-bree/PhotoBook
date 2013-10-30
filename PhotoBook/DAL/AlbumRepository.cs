using System;
using System.Collections.Generic;
using System.Linq;
using PhotoBook.Models;

namespace PhotoBook.DAL
{
    public class AlbumRepository : Repository<Album>
    {
        public AlbumRepository(PhotoBookContext context) : base(context) { }

        /*public Album GetAlbumByUserId(int userId)
        {
            var query = _context.Album.Where(x => x.UserID == userId).First();
            return query;
        }*/
    }
}
