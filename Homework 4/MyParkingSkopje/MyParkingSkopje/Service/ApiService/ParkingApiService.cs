using MyParkingSkopje.Models;
using MyParkingSkopje.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace MyParkingSkopje.Service
{
    //Класа преку која се испраќаат повици до Parking микросервисот - имплементира Singleton Design Pattern
    public class ParkingApiService
    {
        private static ParkingApiService parkingApiService { get; set; }
        //Http клиент преку кој се испраќаат барања до Parking микросервисот
        private HttpClient parkingServiceClient { get; set; }

        private ParkingApiService()
        {
            this.parkingServiceClient = new HttpClient();
            this.parkingServiceClient.BaseAddress = new Uri("https://parkingmicroservice.azurewebsites.net/api");
        }
        public static ParkingApiService ParkingApiServiceInstance()
        {
            if (parkingApiService == null)
                parkingApiService = new ParkingApiService();
            return parkingApiService;
        }
        //Метод кој прави АПИ повик кој ја враќа тековно зачуваната локација за дадениот корисник
        public UserLocation getUserLocation(string userId)
        {
            var responseTask = parkingServiceClient.GetAsync("parking/userlocation?userId=" + userId);
            responseTask.Wait();
            var result = responseTask.Result;
            var readTaskReview = result.Content.ReadAsStringAsync();
            readTaskReview.Wait();
            return JsonConvert.DeserializeObject<UserLocation>(readTaskReview.Result);
        }
        //Метод кој прави АПИ повик кој ги враќа деталите за дадениот паркинг во однос на дадениот корисник
        public ParkingDetailsWithReviews GetParkingDetails(int parkingId, string userId)
        {
            var responseTask = parkingServiceClient.GetAsync("parking/?id=" + parkingId+"&userId="+userId);
            responseTask.Wait();
            var result = responseTask.Result;
            var readTaskReview = result.Content.ReadAsStringAsync();
            readTaskReview.Wait();
            return JsonConvert.DeserializeObject<ParkingDetailsWithReviews>(readTaskReview.Result);
        }
        //Метод кој прави АПИ повик за зачувување/бришење на соодветниот паркинг од листата на Bookmarks на дадениот корисник
        public Boolean bookmarkParking(string userId, int parkingId)
        {
            var responseTask = parkingServiceClient.GetAsync("parking/bookmark?userId=" + userId + "&id=" + parkingId);
            responseTask.Wait();
            var result = responseTask.Result;
            var readTaskReview = result.Content.ReadAsStringAsync();
            readTaskReview.Wait();
            return JsonConvert.DeserializeObject<Boolean>(readTaskReview.Result);

        }

    }
}