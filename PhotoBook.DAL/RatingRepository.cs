using System;
using System.Collections.Generic;
using System.Linq;
using PhotoBook.Entities;

namespace PhotoBook.DAL
{
    public class RatingRepository : Repository<Rating>
    {
        public RatingRepository(PhotoBookContext context) : base(context) { }

        public int GetPhotoRating(int photoId)
        {
            int rating;
            var query = _context.Rating.Where(x => x.PhotoID == photoId);
            if (query.Count() == 0)
            {
                return 0;
            }
            rating = query.Sum(x => x.Like) - query.Sum(x => x.Dislike);

            return rating;
        }

        public Rating GetRatingInfo(int photoId, int userId)
        {
            var query = _context.Rating.Where(x => x.PhotoID == photoId && x.UserID == userId).FirstOrDefault();
            return query;
        }

        public List<int> GetPopularPhotosIDs()
        {
            var query = from x in _context.Rating
                        where (x.Like - x.Dislike) >= 0 
                        orderby x.Like - x.Dislike descending
                        select x.PhotoID;
            return query.ToList();
        }
    }
}
