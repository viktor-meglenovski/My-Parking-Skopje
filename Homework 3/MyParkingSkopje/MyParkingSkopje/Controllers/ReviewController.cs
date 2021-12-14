using Microsoft.AspNet.Identity;
using MyParkingSkopje.Models;
using MyParkingSkopje.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyParkingSkopje.Controllers
{
    public class ReviewController : Controller
    {
        private ApplicationDbContext _context { get; set; }
        public ReviewController()
        {
            this._context = new ApplicationDbContext();
        }
        [HttpPost]
        public ActionResult AddReview(int parkingId, int stars, string reviewText)
        {
            var userId = User.Identity.GetUserId();
            var review = new Review(userId,parkingId,stars,reviewText);
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetReviewDetails(int id)
        {
            var review = _context.Reviews.Find(id);
            var user = _context.Users.Where(x => x.Id == review.UserId).First();
            var model = new ReviewDetails(review,user.FirstName,user.LastName);
            return PartialView(model);
        }
        [HttpPost]
        public ActionResult EditReview(int parkingId, int stars, string reviewText)
        {
            var userId = User.Identity.GetUserId();
            var review = _context.Reviews.Where(x => x.UserId == userId && x.ParkingId == parkingId).ToList();
            if (review.Count > 0)
            {
                var toEdit = review[0];
                toEdit.ReviewText = reviewText;
                toEdit.Stars = stars;
                toEdit.datetime = DateTime.Now;
                _context.SaveChanges();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteReview(int parkingId)
        {
            var userId = User.Identity.GetUserId();
            var review = _context.Reviews.Where(x => x.UserId == userId && x.ParkingId == parkingId).ToList();
            if (review.Count > 0)
            {
                var toDelete = review[0];
                _context.Reviews.Remove(toDelete);
                _context.SaveChanges();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}