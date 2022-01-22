using MyParkingSkopje.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace MyParkingSkopje.Service
{
    //Класа преку која се испраќаат повици до SearchParkings микросервисот - имплементира Singleton Design Pattern
    public class SearchParkingsApiService
    {
        private static SearchParkingsApiService searchParkingsApiService { get; set; }
        //Http клиент преку кој се испраќаат барања до SearchParkings микросервисот
        private HttpClient searchParkingsClient { get; set; }

        private SearchParkingsApiService()
        {
            this.searchParkingsClient = new HttpClient();
            this.searchParkingsClient.BaseAddress = new Uri("https://searchparkingsmicroservice.azurewebsites.net/api/");
        }
        public static SearchParkingsApiService SearchParkingsApiServiceInstance()
        {
            if (searchParkingsApiService == null)
                searchParkingsApiService = new SearchParkingsApiService();
            return searchParkingsApiService;
        }
        //Метод кој прави АПИ повик за добивање на податоците за почетната страна за пребарување во однос на дадениот корисник
        public SearchParkingsIndexViewModel getSearchParkingsIndexViewModel(string userId)
        {
            var responseTask = searchParkingsClient.GetAsync("search/index?id="+userId);
            responseTask.Wait();
            var result = responseTask.Result;
            var readTaskReview = result.Content.ReadAsStringAsync();
            readTaskReview.Wait();
            return JsonConvert.DeserializeObject<SearchParkingsIndexViewModel>(readTaskReview.Result);
        }
        //Метод кој прави АПИ повик за добивање на детали за паркинзи според нивното име
        public List<ParkingDetailsWithReviews> searchParkingsByNameContaining(string name, string userId)
        {
            var responseTask = searchParkingsClient.GetAsync("search/name?name=" + name+"&id="+userId);
            responseTask.Wait();
            var result = responseTask.Result;
            var readTaskReview = result.Content.ReadAsStringAsync();
            readTaskReview.Wait();
            return JsonConvert.DeserializeObject<List<ParkingDetailsWithReviews>>(readTaskReview.Result);
        }
        //Метод кој прави АПИ повик за добивање на детали за паркнинзи според општина
        public List<ParkingDetailsWithReviews> searchParkingInMunicipality(string municipality, string userId)
        {
            var responseTask = searchParkingsClient.GetAsync("search/municipality?municipality=" + municipality + "&id=" + userId);
            responseTask.Wait();
            var result = responseTask.Result;
            var readTaskReview = result.Content.ReadAsStringAsync();
            readTaskReview.Wait();
            return JsonConvert.DeserializeObject<List<ParkingDetailsWithReviews>>(readTaskReview.Result);
        }
        //Метод кој прави АПИ повик за добивање на детали за сите паркинзи сортирани според растојанието од корисникот
        public List<ParkingDetailsWithReviews> sortAllParkingsByDistance(string userId)
        {
            var responseTask = searchParkingsClient.GetAsync("search/location?id="+userId);
            responseTask.Wait();
            var result = responseTask.Result;
            var readTaskReview = result.Content.ReadAsStringAsync();
            readTaskReview.Wait();
            return JsonConvert.DeserializeObject<List<ParkingDetailsWithReviews>>(readTaskReview.Result);
        }
        //Метод кој прави АПИ повик за добивање на детали за сите Bookmarked паркинзи на дадениот корисник
        public List<ParkingDetailsWithReviews> getAllBookmarkedParkingsForUser(string userId)
        {
            var responseTask = searchParkingsClient.GetAsync("search/bookmarks?id=" + userId);
            responseTask.Wait();
            var result = responseTask.Result;
            var readTaskReview = result.Content.ReadAsStringAsync();
            readTaskReview.Wait();
            return JsonConvert.DeserializeObject<List<ParkingDetailsWithReviews>>(readTaskReview.Result);
        }
    }
}