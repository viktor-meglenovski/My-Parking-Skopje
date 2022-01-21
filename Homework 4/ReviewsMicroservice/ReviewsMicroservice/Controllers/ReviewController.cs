using Microsoft.Ajax.Utilities;
using ReviewsMicroservice.Models;
using ReviewsMicroservice.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;

namespace ReviewsMicroservice.Controllers
{
    public class ReviewController : ApiController
    {
        private ReviewService reviewService { get; set; }
        public ReviewController()
        {
            this.reviewService = ReviewService.ReviewServiceInstance();
        }

        //GET акција која ги враќа сите детали за Review со ID зададено како параметар
        [HttpGet]
        [Route("api/review")]
        public ReviewDetails GetReviewDetails(int id)
        {
            return reviewService.getReviewDetails(id);
        }

        //POST акција за додавање на нов Review или ажурирање на постоечки Review за соодветниот паркинг со соодветните атрибути од тековно најавениот корисник
        [HttpPost]
        [Route("api/review")]
        public void AddOrEditReview(string userId, int parkingId, int stars, string reviewText)
        {
            //Се повикува соодветниот метод од сервисот
            reviewService.addOrEditReview(userId, parkingId, stars, reviewText);
        }

        //GET акција за бришење на веќе постоечко Review за паркинг со ID дадено како аргумент, напишано од тековно најавениот корисник
        [HttpGet]
        [Route("api/review/delete")]
        public void DeleteReview(string userId, int parkingId)
        {
            //Се повикува соодветниот метод од сервисот
            reviewService.deleteReview(userId, parkingId);
        }
    }
}
