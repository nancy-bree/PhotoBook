using System;
using System.Collections.Generic;
using System.Linq;
using PhotoBook.Entities;

namespace PhotoBook.DAL
{
    /// <summary>
    /// Defines data access operations for Rating entity.
    /// </summary>
    public class RatingRepository : Repository<Rating>
    {
        public RatingRepository(PhotoBookContext context) : base(context) { }

        /// <summary>
        /// Counting all the votes for photo.
        /// </summary>
        /// <param name="photoId">Photo ID.</param>
        /// <returns>Photo rating.</returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="photoId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Rating GetRatingInfo(int photoId, int userId)
        {
            var query = _context.Rating.Where(x => x.PhotoID == photoId && x.UserID == userId).FirstOrDefault();
            return query;
        }

        public List<int> GetPopularPhotosIDs()
        {
            /*var query2 = from x in _context.Rating
                        where (x.Like - x.Dislike) >= 0
                        orderby x.Like - x.Dislike descending
                        select x.PhotoID;*/
            var query = _context.Database
                        .SqlQuery<int>("SELECT PhotoID FROM Ratings GROUP BY PhotoID ORDER BY SUM([Like] - Dislike) DESC");
            return query.ToList();
        }

        /// <summary>
        /// Removes all the votes for photo.
        /// </summary>
        /// <param name="photoId">Photo ID.</param>
        public void DeletePhotoRating(int photoId)
        {
            var photo = this.GetByID(photoId);
            var query = _context.Rating.Where(x => x.PhotoID == photoId);
            foreach (var item in query)
            {
                this.Delete(item);
            }
        }
    }
}
