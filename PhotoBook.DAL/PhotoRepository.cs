using System;
using System.Collections.Generic;
using System.Linq;
using PhotoBook.Entities;

namespace PhotoBook.DAL
{
    /// <summary>
    /// Defines data access operations for Photo entity.
    /// </summary>
    public class PhotoRepository : Repository<Photo>
    {
        public PhotoRepository(PhotoBookContext context) : base(context) { }

        /// <summary>
        /// Gets all photo tags.
        /// </summary>
        /// <param name="photoId">Photo ID.</param>
        /// <returns>List of tags.</returns>
        public IEnumerable<Tag> GetPhotoTags(int photoId)
        {
            var query = _context.Tag.Where(x => x.Photos.Any(y => y.ID == photoId));
            return query;
        }

        /// <summary>
        /// Removes all photo tags.
        /// </summary>
        /// <param name="photoId">Photo ID.</param>
        public void DeletePhotoTag(int photoId)
        {
            var photo = this.GetByID(photoId);
            if (photo.Tags != null)
            {
                var query = GetPhotoTags(photoId);
                foreach (var item in query)
                {
                    photo.Tags.Remove(item);
                }
            }
        }
    }
}
