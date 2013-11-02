using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using PhotoBook.Models;
using PhotoBook.DAL;
using PhotoBook.Models;
using System.Data;
using PagedList;
using PhotoBook.Properties;

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

        public ActionResult Photos(int id = 1, int page = 1)
        {
            ViewBag.UserID = id;
            var photos = unitOfWork.UserRepository.GetByID(id).Photos;
            return View(photos.ToPagedList(page, Settings.Default.PhotosPerPage));
        }

    }
}
