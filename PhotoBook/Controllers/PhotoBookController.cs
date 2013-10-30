using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using PhotoBook.Models;
using PhotoBook.DAL;
using PhotoBook.Web.Models;
using System.Data;
using PagedList;

namespace PhotoBook.Web.Controllers
{
    public class PhotoBookController : Controller
    {
        private IUnitOfWork unitOfWork = new UnitOfWork();

        //
        // GET: /PhotoBook/

        public ActionResult Index()
        {
            return View();
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

        public ActionResult Photos(int id = 1)
        {
            User user = unitOfWork.UserRepository.GetByID(id);
            return View(user);
        }

    }
}
