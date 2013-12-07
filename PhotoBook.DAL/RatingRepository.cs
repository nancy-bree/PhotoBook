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
            var query = _context.Rating.Where(x => x.PhotoID == photoId);
            if (!query.Any())
            {
                return 0;
            }
            int rating = query.Sum(x => x.Vote);

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
            var query = _context.Rating.FirstOrDefault(x => x.PhotoID == photoId && x.UserID == userId);
            return query;
        }

        public List<int> GetPopularPhotosIDs()
        {
            var query = _context.Database
                        .SqlQuery<int>("SELECT PhotoID FROM Ratings GROUP BY PhotoID ORDER BY SUM(Vote) DESC");
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
