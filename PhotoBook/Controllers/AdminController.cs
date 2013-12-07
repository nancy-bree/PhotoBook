using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PhotoBook.DAL;
using PagedList;
using PhotoBook.Properties;
using WebMatrix.WebData;

namespace PhotoBook.Controllers
{
    [HandleError]
    [Authorize(Users = "Admin")]
    public class AdminController : Controller
    {
        private IUnitOfWork unitOfWork;

        public AdminController(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }

        //
        // GET: /Admin/Users

        public ActionResult Users(int page = 1)
        {
            var list = unitOfWork.UserRepository.Get();
            return View(list.ToPagedList(page, 30));
        }

        //
        // GET: /Admin/Photos

        public ActionResult Photos(int id, int page = 1)
        {
            ViewBag.Author = unitOfWork.UserRepository.GetByID(id).UserName;
            var list = unitOfWork.UserRepository.GetByID(id).Photos;
            return View(list.ToPagedList(page, 30));
        }

        //
        // GET: /Admin/Tags

        public ActionResult Tags(int page = 1)
        {
            var list = unitOfWork.TagRepository.Get();
            return View(list.ToPagedList(page, 30));
        }

        //
        // POST: /Admin/DeleteUser

        [HttpPost]
        public JsonResult DeleteUser(string username)
        {
            //var userVotes = unitOfWork.RatingRepository.Get().Where(x => x.UserID == id);
            //foreach (var item in userVotes)
            //{
            //    unitOfWork.RatingRepository.Delete(item.ID);
            //}
            var membership = (SimpleMembershipProvider)Membership.Provider;
            Roles.RemoveUserFromRoles(username, Roles.GetRolesForUser(username));
            bool wasDeleted = membership.DeleteAccount(username);
            wasDeleted = membership.DeleteUser(username, true);
            return Json("User has been deleted successfully!");
        }

        //
        // POST: /Admin/DeletePhoto

        [HttpPost]
        public JsonResult DeletePhoto(int id)
        {
            //var photoVotes = unitOfWork.RatingRepository.Get().Where(x => x.PhotoID == id);
            //foreach (var item in photoVotes)
            //{
            //    unitOfWork.RatingRepository.Delete(item.ID);
            //}
            unitOfWork.PhotoRepository.Delete(id);
            unitOfWork.Save();
            return Json("Photo has been deleted successfully!");
        }

        //
        // POST: /Admin/DeleteTag

        [HttpPost]
        public JsonResult DeleteTag(int id)
        {
            unitOfWork.TagRepository.Delete(id);
            unitOfWork.Save();
            return Json("Tag has been deleted successfully!");
        }

    }
}
