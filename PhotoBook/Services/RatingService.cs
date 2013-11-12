using PhotoBook.DAL;
using PhotoBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhotoBook.Services
{
    /// <summary>
    /// Defines all operations with rating.
    /// </summary>
    public static class RatingService
    {
        private readonly static IUnitOfWork unitOfWork = new UnitOfWork();

        /// <summary>
        /// Returns list of the most popular photos.
        /// </summary>
        /// <returns>List of photos.</returns>
        public static List<Photo> GetPopularPhotosList()
        {
            var popularIDs = unitOfWork.RatingRepository.GetPopularPhotosIDs();
            popularIDs = popularIDs.Distinct().ToList();
            var list = new List<Photo>();
            foreach (var item in popularIDs)
            {
                list.Add(unitOfWork.PhotoRepository.GetByID(item));
            }
            return list;
        }
    }
}