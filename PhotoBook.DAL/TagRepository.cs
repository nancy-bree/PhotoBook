using System;
using System.Collections.Generic;
using System.Linq;
using PhotoBook.Entities;

namespace PhotoBook.DAL
{
    public class TagRepository : Repository<Tag>
    {
        public TagRepository(PhotoBookContext context) : base(context) { }

        public Tag GetTagByName(string name)
        {
            return _context.Tag.Where(x => x.Name == name).FirstOrDefault();
        }

        public IEnumerable<Photo> GetPhotosByTag(int tagId)
        {
            var query = _context.Photo.Where(x => x.Tags.Any(y => y.ID == tagId));
            return query;
        }
    }
}
