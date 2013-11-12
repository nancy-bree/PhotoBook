using System;
using System.Collections.Generic;
using System.Linq;
using PhotoBook.Entities;

namespace PhotoBook.DAL
{
    /// <summary>
    /// Defines data access operations for Tag entity.
    /// </summary>
    public class TagRepository : Repository<Tag>
    {
        public TagRepository(PhotoBookContext context) : base(context) { }

        /// <summary>
        /// Returns tag by its name.
        /// </summary>
        /// <param name="name">Tag name.</param>
        /// <returns>Tag.</returns>
        public Tag GetTagByName(string name)
        {
            return _context.Tag.Where(x => x.Name == name).FirstOrDefault();
        }

        /// <summary>
        /// Get list of photos tagged <typeparamref name="tagId"/>.
        /// </summary>
        /// <param name="tagId">Tag ID.</param>
        /// <returns>List of photos.</returns>
        public IEnumerable<Photo> GetPhotosByTag(int tagId)
        {
            var query = _context.Photo.Where(x => x.Tags.Any(y => y.ID == tagId));
            return query;
        }
    }
}
