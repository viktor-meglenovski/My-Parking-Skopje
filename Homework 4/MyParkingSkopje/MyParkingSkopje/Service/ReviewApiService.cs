using MyParkingSkopje.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace MyParkingSkopje.Service
{
    //Класа преку која се испраќаат повици до Review микросервисот - имплементира Singleton Design Pattern
    public class ReviewApiService
    {
        private static ReviewApiService reviewApiService { get; set; }
        private HttpClient reviewServiceClient { get; set; }

        private ReviewApiService()
        {
            this.reviewServiceClient = new HttpClient();
            this.reviewServiceClient.BaseAddress = new Uri("https://reviewsmicroservice.azurewebsites.net/api");
        }
        public static ReviewApiService ReviewApiServiceInstance()
        {
            if (reviewApiService == null)
                reviewApiService = new ReviewApiService();
            return reviewApiService;
        }

        public ReviewDetails getReviewDetails(int id)
        {
            var responseTask = reviewServiceClient.GetAsync("review/details?id=" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            var readTaskReview = result.Content.ReadAsStringAsync();
            readTaskReview.Wait();
            return JsonConvert.DeserializeObject<ReviewDetails>(readTaskReview.Result);
        }

        public void addOrEditReview(string userId, int parkingId, int stars, string reviewText)
        {
            AddOrEditReviewViewModel model = new AddOrEditReviewViewModel(userId, parkingId, stars, reviewText);
            var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
            var responseTask = reviewServiceClient.PostAsync("/review",content);
        }

        public void deleteReview(string userId, int parkingId)
        {
            var responseTask = reviewServiceClient.GetAsync("review/delete?userId=" + userId+"&parkingId="+parkingId);
            responseTask.Wait();
        }
    }
}