using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using PhotoBook.Models;
using PhotoBook.Entities;
using PhotoBook.DAL;
using System.Data;
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

        //
        // GET: /Photo/Slideshow

        public ActionResult Slideshow(string type, int id = 1)
        {
            List<Photo> list;
            switch (type)
            {
                case "user":
                    list = unitOfWork.UserRepository.GetByID(id).Photos.ToList();
                    break;
                case "tag":
                    list = unitOfWork.PhotoRepository.Get().Where(x => x.Tags.Any(y => y.ID == id)).ToList();
                    break;
                default:
                    list = RatingService.GetPopularPhotosList();
                    break;
            }
            ViewBag.FirstPhoto = list.First();
            return View(list);
        }

        //
        // GET: /Photo/Upload

        [Authorize]
        public ActionResult Upload()
        {
            return View();
        }

        //
        // POST: /Photo/Upload

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(UploadModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var photo = new Photo
                    {
                        Description = model.Description,
                        Filename = PhotoService.SavePhoto(model.Photo),
                        UserID = (int) Membership.GetUser().ProviderUserKey
                    };
                    if (model.Tags != null)
                    {
                        IEnumerable<string> tags = TagService.SplitTags(model.Tags).Distinct();
                        photo.Tags = new List<Tag>();
                        foreach (var tag in tags)
                        {
                            var tmp = unitOfWork.TagRepository.GetTagByName(tag.Trim());
                            if (tmp == null)
                            {
                                tmp = new Tag
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

        //
        // GET: /Photo/Details

        public ActionResult Details(int id = 1)
        {
            Photo photo = unitOfWork.PhotoRepository.GetByID(id);
            return View(photo);
        }

        //
        // GET: /Photo/Edit

        [Authorize]
        public ActionResult Edit(int id)
        {
            var photo = unitOfWork.PhotoRepository.GetByID(id);
            if (photo.UserID == WebSecurity.CurrentUserId)
            {
                var model = new EditModel
                {
                    ID = photo.ID,
                    Description = photo.Description,
                    Photo = photo.Filename,
                    Effect = (Effect)photo.Effect
                };
                var tags = photo.Tags.Select(item => item.Name).ToList();
                model.Tags = String.Join(", ", tags);
                return View(model);
            }
            return RedirectToAction("Http403", "Error");
        }

        //
        // POST: /Photo/Edit

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
                        oldPhoto.Tags.Clear();
                        foreach (var tag in tags)
                        {
                            var tmp = unitOfWork.TagRepository.GetTagByName(tag.Trim());
                            if (tmp == null)
                            {
                                tmp = new Tag
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

        //
        // GET: /Photo/Delete

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
                }
                catch (System.IO.IOException)
                {
                    ViewBag.PhotoID = photoToDelete.ID;
                    return View("~/Views/Shared/_DeleteUnsuccessful.cshtml");
                }
            }
            return RedirectToAction("Http403", "Error");
        }
    }
}
