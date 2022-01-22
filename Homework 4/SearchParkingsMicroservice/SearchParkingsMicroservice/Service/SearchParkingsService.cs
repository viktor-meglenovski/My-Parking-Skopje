using SearchParkingsMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace SearchParkingsMicroservice.Service
{
    //Класа во која се наоѓа бизнис логиката за SearchParkingsController - имплементира Singleton Design Pattern
    public class SearchParkingsService
    {
        private static SearchParkingsService searchParkingsService { get; set; }

        //HTTPClient објект преку кој испраќаме барања со Parking микросервисот
        private HttpClient parkingServiceClient { get; set; }
        private AppDbContext _context { get; set; }

        private SearchParkingsService()
        {
            this._context = new AppDbContext();
            this.parkingServiceClient = new HttpClient();
            this.parkingServiceClient.BaseAddress= new Uri("https://parkingmicroservice.azurewebsites.net/api/");
        }
        public static SearchParkingsService SearchParkingsServiceInstance()
        {
            if (searchParkingsService == null)
                searchParkingsService = new SearchParkingsService();
            return searchParkingsService;

        }
        //Метод кој враќа листа од сите општини внесени во базата
        public List<Municipality> getAllMunicipalities()
        {
            return _context.Municipalities.ToList();
        }
        //Метод кој ги враќа 10те највисоко оценети паркинзи со нивните детали
        public List<ParkingDetailsWithReviews> getTopTenParkingsByRating(string userId)
        {
            //Ги земаме деталите за сите паркинзи и ги сортираме според рејтингот
            var allParkingsWithDetails= getAllParkingsDetails(userId).OrderBy(x => x.rating).Reverse().ToList();
            //Ги враќаме првите 10 паркинзи со највисок рејтинг
            return allParkingsWithDetails.GetRange(0, 10);
        }
        //Метод кој го припрема моделот за главната страница за пребарување
        public SearchParkingsIndexViewModel getSearchParkingsIndexViewModel(string userId)
        {
            return new SearchParkingsIndexViewModel(getAllMunicipalities(), getTopTenParkingsByRating(userId));
        }
        //Метод кој ги враќа сите детали за паркинзите кои во своето име го содржат стрингот испратен како прв параметар
        public List<ParkingDetailsWithReviews> searchParkingsByNameContaining(string name, string userId)
        {
            return getAllParkingsDetails(userId).Where(x => x.parking.Name.Contains(name)).OrderBy(x => x.rating).ToList();
        }
        //Метод кој ги враќа сите детали за паркинзите кои се во општината испратена како прв параметар
        public List<ParkingDetailsWithReviews> searchParkingInMunicipality(string municipality, string userId)
        {
            return getAllParkingsDetails(userId).Where(x => x.parking.Municipality==municipality).OrderBy(x => x.distance).ToList();
        }
        //Метод кој ги враќа сите детали за сите паркинзи сортирани според оддалеченоста од корисникот во растечки редослед
        public List<ParkingDetailsWithReviews> sortAllParkingsByDistance(string userId)
        {
            return getAllParkingsDetails(userId).OrderBy(x => x.distance).ToList();
        }
        //Метод кој ги враќа сите детали за паркинзите кои се Bookmarked од корисникот со ID пратено како параметар
        public List<ParkingDetailsWithReviews> getAllBookmarkedParkingsForUser(string userId)
        {
            //Ги земаме ID на паркинзите кои се Bookmarked од дадениот корисник
            var parkingIds = getIdsOfBookmarkedParkingsForUser(userId);
            return getAllParkingsDetails(userId).Where(x => parkingIds.Contains(x.parking.ParkingId)).OrderBy(x => x.distance).ToList();
        }
        //Метод кој ги враќа ID на сите Bookmarked паркинзи за корисникот со ID пратено како аргумент
        public List<int> getIdsOfBookmarkedParkingsForUser(string userId)
        {
            return _context.Bookmarks.Where(x => x.UserId == userId).Select(x => x.ParkingId).ToList();
        }

        //Метод кој прави API повик до Parking микросервисот и ги враќа деталите за сите паркинзи за даден корисник
        public List<ParkingDetailsWithReviews> getAllParkingsDetails(string userId)
        {
            //Правиме АПИ повик до микросервисот за Parkings и ги земаме сите детали за сите паркинзи за дадениот корисник
            var responseTask = parkingServiceClient.GetAsync("parking/all?userId=" + userId);
            responseTask.Wait();
            var result = responseTask.Result;
            var readTaskReview = result.Content.ReadAsAsync<List<ParkingDetailsWithReviews>>();
            readTaskReview.Wait();
            var reviews = readTaskReview.Result;
            return reviews;
        }
    }
}