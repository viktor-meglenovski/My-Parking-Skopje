using Microsoft.AspNet.Identity;
using MyParkingSkopje.Models;
using MyParkingSkopje.Service;
using MyParkingSkopje.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyParkingSkopje.Controllers
{
    //Контролер преку кој се креираат, ажурираат, бришат и добиваат информации за објекти од класата Review
    [Authorize]
    public class ReviewController : Controller
    {
        //private ReviewService reviewService { get; set; }
        private ReviewApiService reviewService { get; set; }
        public ReviewController()
        {
            //this.reviewService = ReviewService.ReviewServiceInstance();
            this.reviewService = ReviewApiService.ReviewApiServiceInstance();
        }
        //GET акција која ги враќа сите детали за Review со ID зададено како параметар
        public ActionResult GetReviewDetails(int id)
        {
            //Се земаат деталите за соодветното Review
            var model = reviewService.getReviewDetails(id);
            return PartialView(model);
        }
        //POST акција за додавање на нов Review или ажурирање на постоечки Review за соодветниот паркинг со соодветните атрибути од тековно најавениот корисник
        [HttpPost]
        public ActionResult AddOrEditReview(int parkingId, int stars, string reviewText)
        {
            //Се повикува соодветниот метод од сервисот и се враќа успешен JSON одговор
            reviewService.addOrEditReview(getUserId(), parkingId, stars, reviewText);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //JSON GET акција за бришење на веќе постоечко Review за паркинг со ID дадено како аргумент, напишано од тековно најавениот корисник
        public ActionResult DeleteReview(int parkingId)
        {
            //Се повикува соодветниот метод од сервисот и се враќа успешен JSON одговор
            reviewService.deleteReview(getUserId(), parkingId);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        //Метод кој го враќа ID на тековно најавениот корисник
        public string getUserId()
        {
            return User.Identity.GetUserId();
        }
    }
}