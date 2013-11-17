using PagedList;
using PhotoBook.DAL;
using PhotoBook.Properties;
using System.Linq;
using System.Web.Mvc;

namespace PhotoBook.Controllers
{
    [HandleError]
    public class TagController : Controller
    {
        private IUnitOfWork unitOfWork;

        public TagController(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }

        //
        // GET: /Tag/

        public ActionResult Index(int id = 1, int page = 1)
        {
            ViewBag.Tag = unitOfWork.TagRepository.GetByID(id);
            return View(unitOfWork.PhotoRepository.Get().Where(x => x.Tags.Any(y => y.ID == id)).ToPagedList(page, Settings.Default.PhotosPerPage));
        }

        //
        // GET: /Tag/GetTags

        public JsonResult GetTags(string term)
        {
            var tags = unitOfWork.TagRepository.Get().Where(x => x.Name.ToUpper().Contains(term.ToUpper())).Select(x => x.Name).ToArray();
            return Json(tags, JsonRequestBehavior.AllowGet);
        }

    }
}
