using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoBook.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ActionResult Http404(string aspxerrorpath)
        {
            ViewData["error_path"] = aspxerrorpath;
            return View();
        }

        public ActionResult Http403()
        {
            return View();
        }
    }
}
