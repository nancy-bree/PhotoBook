using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PhotoBook.Entities;
using PhotoBook.DAL;
using System.Data;
using PagedList;
using PhotoBook.Properties;

namespace PhotoBook.Controllers
{
    public class TagController : Controller
    {
        private IUnitOfWork unitOfWork = null;

        public TagController(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }

        //
        // GET: /Tag/

        public ActionResult Index(int id = 1, int page = 1)
        {
            //ViewBag.TagID = id;
            ViewBag.Tag = unitOfWork.TagRepository.GetByID(id);
            return View(unitOfWork.PhotoRepository.Get().Where(x => x.Tags.Any(y => y.ID == id)).ToPagedList(page, Settings.Default.PhotosPerPage));
        }

        public JsonResult GetTags(string term)
        {
            var tags = unitOfWork.TagRepository.Get().Where(x => x.Name.ToUpper().Contains(term.ToUpper())).Select(x => x.Name).ToArray();
            return Json(tags, JsonRequestBehavior.AllowGet);
        }

    }
}
