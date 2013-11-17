using PhotoBook.DAL;
using PhotoBook.Entities;
using System.Collections.Generic;
using System.Linq;

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
            return popularIDs.Select(item => unitOfWork.PhotoRepository.GetByID(item)).ToList();
        }
    }
}