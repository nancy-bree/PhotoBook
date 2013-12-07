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
    public class TagApiController : ApiController
    {
        private readonly IUnitOfWork unitOfWork;

        public TagApiController(IUnitOfWork _unitOfWork)
        {
            this.unitOfWork = _unitOfWork;
        }

        // GET api/tagapi
        public IEnumerable<Tag> Get()
        {
            return unitOfWork.TagRepository.Get();
        }

        // GET api/tagapi/5
        public Tag Get(int id)
        {
            var tag = unitOfWork.TagRepository.GetByID(id);
            if (tag == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return tag;
        }

        // POST api/tagapi
        public HttpResponseMessage Post([FromBody] Tag genre)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.TagRepository.Insert(genre);
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.InternalServerError, "Invalid Model");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        // PUT api/tagapi/5
        public HttpResponseMessage Put(int id, Tag genre)
        {
            try
            {
                var genreToUpdate = unitOfWork.TagRepository.GetByID(id);
                genreToUpdate.Name = genre.Name;
                unitOfWork.TagRepository.Update(genreToUpdate);
                unitOfWork.Save();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
