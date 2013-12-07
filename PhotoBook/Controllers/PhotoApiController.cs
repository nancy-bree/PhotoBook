using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PhotoBook.DAL;
using PhotoBook.Entities;

namespace PhotoBook.Controllers
{
    public class PhotoApiController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public PhotoApiController(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }

        // GET api/photoapi
        public IEnumerable<Photo> Get()
        {
            return unitOfWork.PhotoRepository.Get();
        }

        // GET api/photoapi/5
        public Photo Get(int id)
        {
            var photo = unitOfWork.PhotoRepository.GetByID(id);
            if (photo == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return photo;
        }
    }
}
