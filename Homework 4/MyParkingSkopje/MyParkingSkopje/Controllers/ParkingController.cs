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
    [Authorize]
    public class ParkingController : Controller
    {
        private ApplicationDbContext _context { get; set; }
        public ParkingController()
        {
            this._context = new ApplicationDbContext();
        }
        public ActionResult ListParkings()
        {
            var model = _context.Parkings.ToList();
            return View(model);
        }
        public ActionResult Details(int id)
        {
            var userId = User.Identity.GetUserId();
            var userLocation = _context.UserLocations.Where(x => x.UserId == userId).First();
            if (userLocation == null)
                return View("Index", "Home");
            var parking = _context.Parkings.Find(id);
            var model = GetParkingsDetails(parking);
            ViewBag.userLatitude = userLocation.Lattitude;
            ViewBag.userLongitude = userLocation.Longitude;
            return View(model);
        }
        public ParkingDetailsWithReviews GetParkingsDetails(Parking p)
        {
            var reviews = _context.Reviews.Where(x => x.ParkingId == p.ParkingId).ToList();
            var reviewsDetails = new List<ReviewDetails>();
            foreach(Review r in reviews)
            {
                var user = _context.Users.Where(x => x.Id == r.UserId).First();
                var temp = new ReviewDetails(r, user.FirstName, user.LastName);
                reviewsDetails.Add(temp);
            }
            int numberOfReviews = reviews.Count();
            float rating = 0;
            if (numberOfReviews == 0)
               rating = 0;
            else
            {
                foreach (Review r in reviews)
                    rating += r.Stars;
                rating = ((float)rating) / numberOfReviews;
            }
            var userId = User.Identity.GetUserId();
            var userLocation = _context.UserLocations.Where(x => x.UserId == userId).First();
            var distance = SearchParkingsController.DistanceBetweenTwoCoordinates(userLocation.Lattitude, userLocation.Longitude, p.Lattitude, p.Longitude);
            Boolean bookmarked = _context.Bookmarks.Where(x => x.ParkingId == p.ParkingId && x.UserId == userId).ToList().Count == 0 ? false : true;
            
            var rev = _context.Reviews.Where(x => x.ParkingId == p.ParkingId && x.UserId == userId).ToList();
            Review existingReview = null;
            if (rev.Count > 0)
                existingReview = rev.First();
            var result = new ParkingDetailsWithReviews(p,rating,numberOfReviews,distance,reviewsDetails,bookmarked, existingReview);
            return result;
        }
        public ActionResult BookmarkParking(int id)
        {
            var userId = User.Identity.GetUserId();
            var exists = _context.Bookmarks.Where(x => x.UserId == userId && x.ParkingId == id).ToList();
            if(exists.Count==0)
            {
                _context.Bookmarks.Add(new Bookmark(id, userId));
            }
            else
            {
                var toRemove = exists[0];
                _context.Bookmarks.Remove(toRemove);
            }
            _context.SaveChanges();
            return Json(new { success= true, newState=exists.Count==0?true:false }, JsonRequestBehavior.AllowGet);
        }
    }
}