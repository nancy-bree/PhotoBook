using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PhotoBook.Models;
using PhotoBook.Entities;
using PhotoBook.DAL;
using System.Data;
using PagedList;
using PhotoBook.Services;
using PhotoBook.Properties;
using WebMatrix.WebData;

namespace PhotoBook.Controllers
{
    public class PhotoController : Controller
    {
        private IUnitOfWork unitOfWork = new UnitOfWork();
        //
        // GET: /Photo/

        public ActionResult Slideshow(int id = 1)
        {
            //switch (type)
            //{
            //    case "user":
            //        {
            //            var list = unitOfWork.UserRepository.GetByID(id).Photos;
            //            ViewBag.FirstPhoto = list.First();
            //            View(list);
            //            break;
            //        }
            //    case "tag":
            //        {
            //            var list = unitOfWork.TagRepository.GetByID(id).Photos;
            //            ViewBag.FirstPhoto = list.First();
            //            View(list);
            //            break;
            //        }
            //}
            var list = unitOfWork.UserRepository.GetByID(id).Photos;
            ViewBag.FirstPhoto = list.First();
            return View(list);
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
                    photo.Filename = Services.PhotoService.SavePhoto(model.Photo);
                    photo.UserID = (int)Membership.GetUser().ProviderUserKey;
                    IEnumerable<string> tags = TagService.SplitTags(model.Tags).Distinct();
                    photo.Tags = new List<Tag>();
                    foreach (var tag in tags)
                    {
                        var tmp = unitOfWork.TagRepository.GetTagByName(tag.Trim());
                        if (tmp == null)
                        {
                            tmp = new Tag()
                            {
                                Name = tag.Trim()
                            };
                            unitOfWork.TagRepository.Insert(tmp);
                            unitOfWork.Save();
                        }
                        photo.Tags.Add(tmp);
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

        [Authorize]
        public ActionResult Edit(int id)
        {
            var photo = unitOfWork.PhotoRepository.GetByID(id);
            List<string> tags = new List<string>();
            var model = new EditModel()
            {
                ID = photo.ID,
                Description = photo.Description,
                Photo = photo.Filename,
            };
            foreach (var item in photo.Tags)
            {
                tags.Add(item.Name);
            }
            model.Tags = String.Join(", ", tags);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var user = unitOfWork.UserRepository.GetByID(WebSecurity.CurrentUserId);
                    //Photo photo = new Photo()
                    //{
                    //    ID = model.ID,
                    //    Description = model.Description,
                    //    Filename = model.Photo,
                    //    UserID = WebSecurity.CurrentUserId,
                    //    User = user
                    //};
                    //IEnumerable<string> tags = TagService.SplitTags(model.Tags).Distinct();
                    //photo.Tags = new List<Tag>();
                    //foreach (var tag in tags)
                    //{
                    //    photo.Tags.Add(unitOfWork.TagRepository.GetTagByName(tag));
                    //}
                    ////photo.Description = model.Description;
                    ////photo.Filename = Services.PhotoUploadService.SavePhoto(model.Photo);
                    ////photo.UserID = (int)Membership.GetUser().ProviderUserKey;
                    ////unitOfWork.PhotoRepository.Insert(photo);
                    ////unitOfWork.Save();
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to edit photo. Try again, and if the problem persists, see your system administrator.");
            }
            return View(model);
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            var photoToDelete = unitOfWork.PhotoRepository.GetByID(id);
            unitOfWork.PhotoRepository.Delete(id);
            unitOfWork.Save();
            string[] photos = System.IO.Directory.GetFiles(Server.MapPath(Settings.Default.UserUploads), "*" + photoToDelete.Filename);
            foreach (var photo in photos)
            {
                System.IO.File.Delete(photo);
            }
            return RedirectToAction("Photos", "PhotoBook");
        }
    }
}
