using MyParkingSkopje.Models;
using MyParkingSkopje.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyParkingSkopje.Service
{
    //Класа во која се наоѓа бизнис логиката за ReviewController - имплементира Singleton Design Pattern
    public class ReviewService
    {
        private static ReviewService reviewService { get; set; }
        private ApplicationDbContext _context { get; set; }

        private ReviewService()
        {
            this._context = new ApplicationDbContext();
        }
        public static ReviewService ReviewServiceInstance()
        {
            if (reviewService == null)
                reviewService = new ReviewService();
            return reviewService;

        }
        //Метод кој ги враќа сите детали за Review со даденото ID
        public ReviewDetails getReviewDetails(int id)
        {
            //Се зема соодветното Review од база
            var review = _context.Reviews.Find(id);
            //Се зема корисникот кој го напишал тоа Review од база
            var user = _context.Users.Where(x => x.Id == review.UserId).First();
            //Се враќа објект со сите потребни податоци
            return new ReviewDetails(review, user.FirstName, user.LastName);
        }

        //Метод кој додава нов Review со соодветните параметри
        public void addReview(string userId, int parkingId, int stars, string reviewText)
        {
            //Се инстанцира нов објект од класата Review со соодветните параметри и се зачувува во база.
            var review = new Review(userId, parkingId, stars, reviewText);
            _context.Reviews.Add(review);
            _context.SaveChanges();
        }

        //Метод кој ажурира веќе постоечко Review од даден корисник за даден паркинг - ако не соодветно Review не се прави ништо
        public void editReview(string userId, int parkingId, int stars, string reviewText)
        {
            //Се бара веќе постоечко Review за дадениот паркинг од дадениот корисник
            var review = _context.Reviews.Where(x => x.UserId == userId && x.ParkingId == parkingId).ToList();
            if (review.Count > 0)
            {
                //Ако такво Review постои, истото се ажурира и се зачувува во база
                var toEdit = review[0];
                toEdit.ReviewText = reviewText;
                toEdit.Stars = stars;
                toEdit.datetime = DateTime.Now;
                _context.SaveChanges();
            }
        }
        //Метод кој бриши веќе постоечко Review од даден корисник за даден паркинг - ако не соодветно Review не се прави ништо
        public void deleteReview(string userId, int parkingId)
        {
            //Се бара веќе постоечко Review за дадениот паркинг од дадениот корисник
            var review = _context.Reviews.Where(x => x.UserId == userId && x.ParkingId == parkingId).ToList();
            if (review.Count > 0)
            {
                //Ако такво Review постои, истото се ажурира и се зачувува во база
                var toDelete = review[0];
                _context.Reviews.Remove(toDelete);
                _context.SaveChanges();
            }
        }
        //Метод кој враќа детали за листа од Review објекти
        public List<ReviewDetails> getReviewsDetails(List<Review> reviews)
        {
            //За секој Review ги земаме деталите и ги зачувуваме во листа која ја враќаме како резултат
            var reviewsDetails = new List<ReviewDetails>();
            foreach (Review r in reviews)
                reviewsDetails.Add(getReviewDetails(r));
            return reviewsDetails;
        }
        //Метод кој враќа детали за еден Review објект
        public ReviewDetails getReviewDetails(Review review)
        {
            //Земаме кој корисник го напишал соодветното Review
            var user = _context.Users.Where(x => x.Id == review.UserId).First();
            //Покрај Review, ги враќаме и името и презимето на корисникот кој го напишал истото.
            return new ReviewDetails(review, user.FirstName, user.LastName);
        }

        //Метод кој ги враќа сите Reviews за паркинг со даденото ID
        public List<Review> getAllReviewsForParking(int id)
        {
            return _context.Reviews.Where(x => x.ParkingId == id).ToList();
        }

        //Метод кој проверува дали Review за соодветниот паркинг од соодветниот корисник постои во базата, и го враќа истото
        public Review getExistingReview(string userId, int parkingId)
        {
            var rev = _context.Reviews.Where(x => x.ParkingId == parkingId && x.UserId == userId).ToList();
            Review existingReview = null;
            if (rev.Count > 0)
                existingReview = rev.First();
            return existingReview;
        }
    }
}