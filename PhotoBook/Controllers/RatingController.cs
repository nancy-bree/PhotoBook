using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhotoBook.DAL;
using PhotoBook.Entities;

namespace PhotoBook.Controllers
{
    public class RatingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public RatingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public JsonResult HasVoted(int userId, int photoId)
        {
            var result = _unitOfWork.RatingRepository.HasVoted(userId, photoId);
            return Json(new { result = result });
        }

        [HttpPost]
        public int AddRemoveVote(int userId, int photoId, string action)
        {
            var vote = _unitOfWork.RatingRepository.Get()
                .FirstOrDefault(x => x.UserID == userId && x.PhotoID == photoId);
            switch (action)
            {
                case "up":
                    if (vote == null)
                    {
                        _unitOfWork.RatingRepository.Insert(new Rating {UserID = userId, PhotoID = photoId, Vote = 1});
                    }
                    else
                    {
                        vote.Vote = 1;
                        _unitOfWork.RatingRepository.Update(vote);
                    }
                    break;
                case "down":
                    if (vote == null)
                    {
                        _unitOfWork.RatingRepository.Insert(new Rating { UserID = userId, PhotoID = photoId, Vote = -1 });
                    }
                    else
                    {
                        vote.Vote = -1;
                        _unitOfWork.RatingRepository.Update(vote);
                    }
                    break;
                case "remove":
                    _unitOfWork.RatingRepository.Delete(vote);
                    break;
            }
            _unitOfWork.Save();
            var photoRating = _unitOfWork.PhotoRepository.GetByID(photoId).Votes.Sum(x => x.Vote);
            return photoRating;
        }

    }
}
