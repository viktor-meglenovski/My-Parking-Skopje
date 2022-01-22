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
        //Http клиент преку кој се испраќаат барања до Review микросервисот
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

        //Метод кој прави АПИ повик кој ги враќа деталите за Review со даденото ID
        public ReviewDetails getReviewDetails(int id)
        {
            var responseTask = reviewServiceClient.GetAsync("review/details?id=" + id);
            responseTask.Wait();
            var result = responseTask.Result;
            var readTaskReview = result.Content.ReadAsStringAsync();
            readTaskReview.Wait();
            return JsonConvert.DeserializeObject<ReviewDetails>(readTaskReview.Result);
        }
        //Метод кој прави АПИ повик за додавање/ажурирање на Review врз основа на тоа дали постои Review за дадениот паркинг од дадениот корисник
        public void addOrEditReview(string userId, int parkingId, int stars, string reviewText)
        {
            AddOrEditReviewViewModel model = new AddOrEditReviewViewModel(userId, parkingId, stars, reviewText);
            var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
            var responseTask = reviewServiceClient.PostAsync("/review",content);
        }
        //Метод кој прави АПИ повик за бришење Review за дадениот паркинг од дадениот корисник
        public void deleteReview(string userId, int parkingId)
        {
            var responseTask = reviewServiceClient.GetAsync("review/delete?userId=" + userId+"&parkingId="+parkingId);
            responseTask.Wait();
        }
    }
}