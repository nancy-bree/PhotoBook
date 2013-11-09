using System;
using System.Collections.Generic;
using System.Linq;
using PhotoBook.Entities;

namespace PhotoBook.DAL
{
    public class PhotoRepository : Repository<Photo>
    {
        public PhotoRepository(PhotoBookContext context) : base(context) { }

        public IEnumerable<Tag> GetPhotoTags(int photoId)
        {
            var query = _context.Tag.Where(x => x.Photos.Any(y => y.ID == photoId));
            return query;
        }

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
