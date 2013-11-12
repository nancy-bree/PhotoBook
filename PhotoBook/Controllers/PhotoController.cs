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
    [HandleError]
    public class PhotoController : Controller
    {
        private IUnitOfWork unitOfWork = null;

        public PhotoController(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }

        public ActionResult Slideshow(string type, int id = 1)
        {
            List<Photo> list;
            if (type == "user")
            {
                list = unitOfWork.UserRepository.GetByID(id).Photos.ToList();
            }
            if (type == "tag")
            {
                list = unitOfWork.PhotoRepository.Get().Where(x => x.Tags.Any(y => y.ID == id)).ToList();
            }
            else
            {
                list = RatingService.GetPopularPhotosList(); 
            }
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
                    if (model.Tags != null)
                    {
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
                    }
                    unitOfWork.PhotoRepository.Insert(photo);
                    unitOfWork.Save();
                    return RedirectToAction("Photos", "PhotoBook", new { id = WebSecurity.CurrentUserId});
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
            if (photo.UserID == WebSecurity.CurrentUserId)
            {
                List<string> tags = new List<string>();
                var model = new EditModel()
                {
                    ID = photo.ID,
                    Description = photo.Description,
                    Photo = photo.Filename,
                    Effect = (Effect)photo.Effect
                };
                foreach (var item in photo.Tags)
                {
                    tags.Add(item.Name);
                }
                model.Tags = String.Join(", ", tags);
                return View(model);
            }
            else
            {
                return RedirectToAction("Http403", "Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = unitOfWork.UserRepository.GetByID(WebSecurity.CurrentUserId);
                    var oldPhoto = unitOfWork.PhotoRepository.GetByID(model.ID);
                    oldPhoto.Description = model.Description;
                    oldPhoto.User = user;
                    if (oldPhoto.Effect != (int)model.Effect)
                    {
                        PhotoService.ApplyEffect(model.Effect, model.Photo);
                    }
                    oldPhoto.Effect = (int)model.Effect;
                    if ((model.Tags != null) && !String.IsNullOrWhiteSpace(model.Tags))
                    {
                        IEnumerable<string> tags = TagService.SplitTags(model.Tags).Distinct();
                        oldPhoto.Tags = new List<Tag>();
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
                            oldPhoto.Tags.Add(tmp);
                        }
                    }
                    else
                    {
                        unitOfWork.PhotoRepository.DeletePhotoTag(oldPhoto.ID);
                    }
                    unitOfWork.PhotoRepository.Update(oldPhoto);
                    unitOfWork.Save();
                    return RedirectToAction("Details", "Photo", new { id = model.ID });
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
            if (photoToDelete.UserID == WebSecurity.CurrentUserId)
            {
                try
                {
                    unitOfWork.PhotoRepository.Delete(id);
                    unitOfWork.RatingRepository.DeletePhotoRating(id);
                    unitOfWork.Save();
                    string[] photos = System.IO.Directory.GetFiles(Server.MapPath(Settings.Default.UserUploads), "*" + photoToDelete.Filename);
                    foreach (var photo in photos)
                    {
                        if (System.IO.File.Exists(photo))
                        {
                            System.IO.File.Delete(photo);
                        }
                    }
                    return View("~/Views/Shared/_DeleteSuccessful.cshtml");
                    //throw new System.IO.IOException();    // only to test IOExeption
                }
                catch (System.IO.IOException)
                {
                    ViewBag.PhotoID = photoToDelete.ID;
                    return View("~/Views/Shared/_DeleteUnsuccessful.cshtml");
                }
            }
            else
            {
                return RedirectToAction("Http403", "Error");
            }
        }
    }
}
