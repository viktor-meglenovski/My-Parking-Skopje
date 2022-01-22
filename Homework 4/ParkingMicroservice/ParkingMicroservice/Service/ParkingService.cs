using Newtonsoft.Json;
using ParkingMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace ParkingMicroservice.Service
{
    public class ParkingService
    {
        //Класа во која се наоѓа бизнис логиката за ParkingController - имплементира Singleton Design Pattern
        private static ParkingService parkingService { get; set; }

        //HTTPClient објект преку кој испраќаме барања со Review микросервисот
        private HttpClient reviewServiceClient { get; set; }
        private AppDbContext _context { get; set; }
        private ParkingService()
        {
            this._context = new AppDbContext();
            this.reviewServiceClient = new HttpClient();
            this.reviewServiceClient.BaseAddress = new Uri("https://reviewsmicroservice.azurewebsites.net/api/");
        }
        public static ParkingService ParkingServiceInstance()
        {
            if (parkingService == null)
                parkingService = new ParkingService();
            return parkingService;

        }
        //Метод кој враќа листа од сите паркинзи зачувани во базата
        public List<Parking> getAllParkings()
        {
            return _context.Parkings.ToList();
        }
        //Метод кој ги враќа сите детали за паркингот што е пуштен како параметар
        public ParkingDetailsWithReviews GetParkingDetails(int parkingId, string userId)
        {
            Parking p = _context.Parkings.Find(parkingId);

            //Правиме АПИ повик до микросервисот за Reviews и ги земаме сите Reviews за дадениот паркинг
            var responseTask = reviewServiceClient.GetAsync("review?id=" + parkingId);
            responseTask.Wait();
            var result = responseTask.Result;
            var readTaskReview = result.Content.ReadAsAsync<List<Review>>();
            readTaskReview.Wait();
            var reviews = readTaskReview.Result;

            //Ги серијализираме податоците во JSON формат со цел да ги испратиме до АПИто повторно
            var content = new StringContent(JsonConvert.SerializeObject(reviews), System.Text.Encoding.UTF8, "application/json");

            //Правиме АПИ повик до микросервисот за Review и ги добиваме деталите за сите Reviews за тој паркинг.
            responseTask = reviewServiceClient.PostAsync("review/allDetails", content);
            responseTask.Wait();
            result = responseTask.Result;
            var readTaskReviewDetails = result.Content.ReadAsAsync<List<ReviewDetails>>();
            readTaskReviewDetails.Wait();
            var reviewsDetails = readTaskReviewDetails.Result;

            //Вкупен број на Reviews за тој паркинг
            int numberOfReviews = reviews.Count();

            //Пресметка на просечна оценка на паркингот
            float rating = calculateAverageRating(reviews);

            //Пресметување на растојание помеѓу тековниот корисник и паркингот
            var userLocation = _context.UserLocations.Where(x => x.UserId == userId).First();
            var distance = DistanceBetweenTwoCoordinates(userLocation.Lattitude, userLocation.Longitude, p.Lattitude, p.Longitude);

            //Проверка дали тековниот корисник го има дадениот паркинг во својата листа на зачувани паркинзи
            Boolean bookmarked = checkIfBookmarked(userId, p.ParkingId);

            //Добивање на веќе постоечко Review за дадениот паркинг од тековно најавениот корисник
            //Податоците од веќе постоечкото Review се прикажуваат кога корисник ќе отвори страна на паркинг за која веќе има внесено Review
            //Правиме АПИ повик до микросервисот за Reviews и ги праќаме соодветните атрибути
            responseTask = reviewServiceClient.GetAsync("review/existing?id="+parkingId+"&userId="+userId);
            responseTask.Wait();
            result = responseTask.Result;
            var readTaskExistingReview= result.Content.ReadAsAsync<Review>();
            readTaskReviewDetails.Wait();
            var existingReview = readTaskExistingReview.Result;

            //Враќање на сите детали за паркингот
            return new ParkingDetailsWithReviews(p, rating, numberOfReviews, distance, reviewsDetails, bookmarked, existingReview);
        }

        //Метод кој ги враќа деталите за сите паркинзи во однос на даден корисник
        public List<ParkingDetailsWithReviews> GetAllParkingDetails(string userId)
        {
            var allParkings = _context.Parkings.ToList();
            return GetParkingsDetails(allParkings, userId);
        }

        //Метод кој ги враќа деталите за листа од паркинзи
        public List<ParkingDetailsWithReviews> GetParkingsDetails(List<Parking> parkings, string userId)
        {
            var result = new List<ParkingDetailsWithReviews>();
            foreach (Parking p in parkings)
                result.Add(GetParkingDetails(p.ParkingId, userId));
            return result;
        }
        //Метод кој пресметува просечен рејтинг врз основа на листа од Review објекти
        public float calculateAverageRating(List<Review> reviews)
        {
            int numberOfReviews = reviews.Count();
            float rating = 0;
            if (numberOfReviews == 0)
                //Ако нема Reviews, просечната оценка е 0
                return 0;
            //Во спротивно, се пресметува просек на сите Reviews
            foreach (Review r in reviews)
                //Stars е атрибутот во Review кој ја означува оценката
                rating += r.Stars;
            return ((float)rating) / numberOfReviews;
        }

        //Метод кој проверува дали тековниот корисник го има зачувано паркингот со ID пуштено како аргумент во својата листа на заувани паркинзи
        public Boolean checkIfBookmarked(string userId, int parkingId)
        {
            return _context.Bookmarks.Where(x => x.ParkingId == parkingId && x.UserId == userId).ToList().Count == 0 ? false : true;
        }
        //Метод кој зачувува/бриши ставка од Bookmark табелата во зависност од тоа дали веќе постои соодветен запис (врз основа на userId и parkingId) или не.
        public Boolean bookmarkParking(string userId, int parkingId)
        {
            //Проверуваме дали корисникот веќе го има зачувано паркингот во својата листа
            var exists = _context.Bookmarks.Where(x => x.UserId == userId && x.ParkingId == parkingId).ToList();
            //Новата состојба - true ако после извршувањето на методот паркингот е зачуван во листата на паркинзи на корисникот, false во спротива
            Boolean newState;
            if (exists.Count() == 0)//Доколку го нема зачувано:
            {
                //Во табелата Bookmarks (која претставува ManyToMany врска помеѓу Паркинг и Корисник)
                //зачувуваме дека корисникот го ставил соодветиот паркинг во својата листа.
                _context.Bookmarks.Add(new Bookmark(parkingId, userId));
                newState = true;
            }
            else //Доколку го нема зачувано:
            {
                //Ја бараме соодветната инстанца од базата и ја бришиме
                var toRemove = exists[0];
                _context.Bookmarks.Remove(toRemove);
                newState = false;
            }
            //Ги зачувуваме промените во базата
            _context.SaveChanges();
            return newState;
        }
        //Метод кој враќа објект од UserLocation класата за соодветното userId (доколку не постои враќа null)
        public UserLocation getUserLocation(string userId)
        {
            var location = _context.UserLocations.Where(x => x.UserId == userId).ToList();
            if (location.Count() > 0)
                return location.First();
            return null;
        }

        //Статички метод кој пресметува растојание помеѓу 2 координати
        public static double DistanceBetweenTwoCoordinates(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371;   //Радиус на земјата во км
            var dLat = DegreesToRadians(lat2 - lat1);
            var dLon = DegreesToRadians(lon2 - lon1);
            var a =
              Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
              Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
              Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c; //Растојание во км
            return d;
        }
        //Статички метод кој претвора од степени во радиани - се користи во DistanceBetweenTwoCoordinates
        public static double DegreesToRadians(double deg)
        {
            return deg * (Math.PI / 180);
        }
    }
}