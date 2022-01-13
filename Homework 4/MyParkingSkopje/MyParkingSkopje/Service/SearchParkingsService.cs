using MyParkingSkopje.Models;
using MyParkingSkopje.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyParkingSkopje.Service
{
    //Класа во која се наоѓа бизнис логиката за SearchParkingsController - имплементира Singleton Design Pattern
    public class SearchParkingsService
    {
        private static SearchParkingsService searchParkingsService { get; set; }
        private ParkingService parkingService { get; set; }
        private ApplicationDbContext _context { get; set; }

        private SearchParkingsService()
        {
            this._context = new ApplicationDbContext();
            this.parkingService = ParkingService.ParkingServiceInstance();
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
            var allParkingsWithDetails = parkingService.GetParkingsDetails(parkingService.getAllParkings(),userId).OrderBy(x => x.rating).Reverse().ToList();
            //Ги враќаме првите 10 паркинзи со највисок рејтинг
            return allParkingsWithDetails.GetRange(0, 10);
        }
        //Метод кој го припрема моделот за главната страница за пребарување
        public SearchParkingsIndexViewModel getSearchParkingsIndexViewModel(string userId)
        {
            return new SearchParkingsIndexViewModel(getAllMunicipalities(), getTopTenParkingsByRating(userId));
        }
        //Метод кој ги враќа сите детали за паркинзите кои во своето име го содржат стрингот испратен како прв параметар
        public List<ParkingDetailsWithReviews> searchParkingsByNameContaining(string name,string userId)
        {
            var parkings = parkingService.getAllParkings().Where(x => x.Name.Contains(name)).ToList();
            return parkingService.GetParkingsDetails(parkings,userId).OrderBy(x => x.rating).ToList();
        }
        //Метод кој ги враќа сите детали за паркинзите кои се во општината испратена како прв параметар
        public List<ParkingDetailsWithReviews> searchParkingInMunicipality(string municipality, string userId)
        {
            var parkings = parkingService.getAllParkings().Where(x => x.Municipality.Equals(municipality)).ToList();
            return parkingService.GetParkingsDetails(parkings, userId).OrderBy(x => x.distance).ToList();
        }
        //Метод кој ги враќа сите детали за сите паркинзи сортирани според оддалеченоста од корисникот во растечки редослед
        public List<ParkingDetailsWithReviews> sortAllParkingsByDistance(string userId)
        {
            var parkings = parkingService.getAllParkings().ToList();
            return parkingService.GetParkingsDetails(parkings, userId).OrderBy(x => x.distance).ToList();
        }
        //Метод кој ги враќа сите детали за паркинзите кои се Bookmarked од корисникот со ID пратено како параметар
        public List<ParkingDetailsWithReviews> getAllBookmarkedParkingsForUser(string userId)
        {
            //Ги земаме ID на паркинзите кои се Bookmarked од дадениот корисник
            var parkingIds = getIdsOfBookmarkedParkingsForUser(userId);
            var parkings = parkingService.getAllParkings().Where(x => parkingIds.Contains(x.ParkingId)).ToList();
            var model = parkingService.GetParkingsDetails(parkings,userId);
            return parkingService.GetParkingsDetails(parkings.ToList(), userId).OrderBy(x => x.distance).ToList();
        }
        //Метод кој ги враќа ID на сите Bookmarked паркинзи за корисникот со ID пратено како аргумент
        public List<int> getIdsOfBookmarkedParkingsForUser(string userId)
        {
            return _context.Bookmarks.Where(x => x.UserId == userId).Select(x => x.ParkingId).ToList();
        }
    }
}