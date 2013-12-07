using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PhotoBook.Entities;
using PhotoBook.DAL;
using PagedList;
using PhotoBook.Properties;
using WebMatrix.WebData;
using PhotoBook.Models;
using PhotoBook.Services;

namespace PhotoBook.Controllers
{
    [HandleError]
    public class PhotoBookController : Controller
    {
        private IUnitOfWork unitOfWork;

        public PhotoBookController(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }

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

        //
        // GET: /PhotoBook/Photos

        public ActionResult Photos(int id = 1, int page = 1)
        {
            ViewBag.UserID = id;
            var photos = unitOfWork.UserRepository.GetByID(id).Photos;
            return View(photos.ToPagedList(page, Settings.Default.PhotosPerPage));
        }

        //
        // GET: /PhotoBook/UserAlbums

        public ActionResult UserAlbum(int page = 1)
        {
            var albumList = new List<AlbumViewModel>();
            var users = unitOfWork.UserRepository.Get();
            FillAlbumModel(users, albumList);
            return View(albumList.ToPagedList(page, Settings.Default.PhotosPerPage));
        }

        private static void FillAlbumModel(IEnumerable<User> users, List<AlbumViewModel> albumList)
        {
            foreach (var user in users)
            {
                if (user.UserName == "Admin") continue;
                var cover = SetCover(user);
                albumList.Add(new AlbumViewModel
                {
                    ID = user.ID,
                    Count = user.Photos.Count,
                    Cover = cover,
                    Username = user.UserName
                });
            }
        }

        private static string SetCover(User user)
        {
            string cover;
            if (user.Photos.Count == 0)
            {
                cover = Settings.Default.NoCover;
            }
            else
            {
                var random = new Random();
                var toSkip = random.Next(0, user.Photos.Count);
                cover = user.Photos.Skip(toSkip).Take(1).First().Filename;
            }
            return cover;
        }
    }
}
