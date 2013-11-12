using PhotoBook.Entities;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using PhotoBook.Properties;

namespace PhotoBook.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class MainPageModel
    {
        public IOrderedEnumerable<Tag> TagCloud { get; set; }

        public IPagedList<Photo> PopularPhotos { get; set; }

        public MainPageModel(IOrderedEnumerable<Tag> tags, List<Photo> popularPhotos, int page = 1)
        {
            this.TagCloud = tags;
            this.PopularPhotos = popularPhotos.ToPagedList(page, Settings.Default.PhotosPerPage);
        }
    }
}