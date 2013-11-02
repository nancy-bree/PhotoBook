using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PhotoBook.Models;
using PhotoBook.DAL;
using PhotoBook.Models;
using System.Data;
using PagedList;
using PhotoBook.Services;

namespace PhotoBook.Controllers
{
    public class PhotoController : Controller
    {
        private IUnitOfWork unitOfWork = new UnitOfWork();
        //
        // GET: /Photo/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UploadModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Photo photo = new Photo();
                    photo.Description = model.Description;
                    photo.Filename = Services.PhotoUploadService.SavePhoto(model.Photo);
                    photo.UserID = (int)Membership.GetUser().ProviderUserKey;
                    List<string> tags = TagService.SplitTags(model.Tags);
                    photo.Tags = new List<Tag>();
                    foreach (var tag in tags)
                    {
                        photo.Tags.Add(unitOfWork.TagRepository.GetTagByName(tag));
                    }
                    unitOfWork.PhotoRepository.Insert(photo);
                    unitOfWork.Save();
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(model);
        }

        [Authorize]
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(UploadModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Photo photo = new Photo();
                    photo.Description = model.Description;
                    photo.Filename = Services.PhotoUploadService.SavePhoto(model.Photo);
                    photo.UserID = (int)Membership.GetUser().ProviderUserKey;
                    List<string> tags = TagService.SplitTags(model.Tags);
                    photo.Tags = new List<Tag>();
                    foreach (var tag in tags)
                    {
                        photo.Tags.Add(unitOfWork.TagRepository.GetTagByName(tag));
                    }
                    unitOfWork.PhotoRepository.Insert(photo);
                    unitOfWork.Save();
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View(model);
        }

        public ActionResult Details(int id = 1)
        {
            Photo photo = unitOfWork.PhotoRepository.GetByID(id);
            return View(photo);
        }

        public ActionResult Edit(int id)
        {
            return View(unitOfWork.PhotoRepository.GetByID(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Photo photo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //Photo photo = new Photo();
                    //photo.Description = model.Description;
                    //photo.Filename = Services.PhotoUploadService.SavePhoto(model.Photo);
                    //photo.UserID = (int)Membership.GetUser().ProviderUserKey;
                    //unitOfWork.PhotoRepository.Insert(photo);
                    //unitOfWork.Save();
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to edit photo. Try again, and if the problem persists, see your system administrator.");
            }
            return View(photo);
        }
    }
}
