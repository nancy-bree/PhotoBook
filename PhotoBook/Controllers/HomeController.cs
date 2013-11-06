using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoBook.Entities;
using PhotoBook.Properties;
using PhotoBook.DAL;

namespace PhotoBook.Web.Controllers
{
    public class HomeController : Controller
    {
        private IUnitOfWork unitOfWork = new UnitOfWork();


        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            ViewBag.path = Server.MapPath(Settings.Default.UserUploads);
            ViewData["TagCloud"] = unitOfWork.TagRepository.Get();

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
