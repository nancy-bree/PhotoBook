using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using PhotoBook.Entities;
using PhotoBook.DAL;
using System.Data;
using PagedList;
using PhotoBook.Properties;
using WebMatrix.WebData;
using PhotoBook.Models;
using PhotoBook.Services;

namespace PhotoBook.Web.Controllers
{
    //[HandleError]
    public class PhotoBookController : Controller
    {
        private IUnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /PhotoBook/

        public ActionResult Index(int page = 1)
        {
            var tags = unitOfWork.TagRepository.Get().OrderBy(x => x.Name);
            ViewBag.TotalPhotosCount = unitOfWork.PhotoRepository.Get().Count();
            var popularPhotos = RatingService.GetPopularPhotosList();
            var model = new MainPageModel(tags, popularPhotos, page);
            return View(model);
        }

        public ActionResult Photos(int id = 1, int page = 1)
        {
            ViewBag.UserID = id;
            var photos = unitOfWork.UserRepository.GetByID(id).Photos;
            return View(photos.ToPagedList(page, Settings.Default.PhotosPerPage));
        }

        public ActionResult UserAlbum(int page = 1)
        {
            List<AlbumViewModel> albumList = new List<AlbumViewModel>();
            var users = unitOfWork.UserRepository.Get();
            foreach (var user in users)
            {
                Random random = new Random();
                int toSkip = random.Next(0, user.Photos.Count);
                var cover =user.Photos.Skip(toSkip).Take(1).First().Filename;
                albumList.Add(new AlbumViewModel() { ID = user.ID, Count = user.Photos.Count, Cover = cover, Username = user.UserName });
            }
            return View(albumList.ToPagedList(page, Settings.Default.PhotosPerPage));
        }

        [HttpPost]
        public int AjaxVote(int photoid, string action)
        {
            // check if already voted, if found then return
            //if () {return; }
            var photo = unitOfWork.PhotoRepository.GetByID(photoid);
            if (photo.UserID == WebSecurity.CurrentUserId) { return unitOfWork.RatingRepository.GetPhotoRating(photoid); }  // do not allow vote for own photo


            var rating = unitOfWork.RatingRepository.GetRatingInfo(photoid, WebSecurity.CurrentUserId);
            if (rating == null)
            {
                rating = new Rating()
                {
                    PhotoID = photoid,
                    UserID = WebSecurity.CurrentUserId
                };
                if (action == "up")
                {
                    rating.Like = 1;
                }
                if (action == "down")
                {
                    rating.Dislike = 1;
                }
                unitOfWork.RatingRepository.Insert(rating);
                unitOfWork.Save();
            }
            else 
            {
                // if clicked the same item then do nothing
                if ((rating.Like == 1 && action == "up") || (rating.Dislike == 1 && action == "down"))
                {
                    //unitOfWork.RatingRepository.Delete(rating);
                    //unitOfWork.Save();
                    return unitOfWork.RatingRepository.GetPhotoRating(photoid);
                }
                else
                {
                    if (action == "up") { rating.Like = 1; rating.Dislike = 0; }
                    if (action == "down") { rating.Dislike = 1; rating.Like = 0; }
                    unitOfWork.RatingRepository.Update(rating);
                    unitOfWork.Save();
                }
            }    // if already voted

            return unitOfWork.RatingRepository.GetPhotoRating(photoid);
        }
    }
}
