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
    //Контролер преку кој се пребаруваат паркинзи според различни филтри
    [Authorize]
    public class SearchParkingsController : Controller
    {
        private SearchParkingsService searchParkingsService { get; set; }
        public SearchParkingsController()
        {
            this.searchParkingsService = SearchParkingsService.SearchParkingsServiceInstance();
        }
        //GET акција која ја враќа главната страна за пребарување со сите потребни детали опишани во самата акција
        public ActionResult Index()
        {
            var model = searchParkingsService.getSearchParkingsIndexViewModel(getUserId());
            return View(model);
        }
        //GET акција која ги филтрира паркинзите според тоа дали нивното име го содржи стрингот испратен како параметар
        public ActionResult ByName(string name)
        {
            var model = searchParkingsService.searchParkingsByNameContaining(name, getUserId());
            ViewBag.message = "Search results for: " + name;
            ViewBag.errorMessage = "No results found for name: "+name+" :(";
            return View("SortableSearchResults", model);
        }
        //GET акција која ги филтрира паркинзите според тоа дали тие се наоѓаат во општината пуштена како параметар
        public ActionResult ByMunicipality(string municipality)
        {
            var model = searchParkingsService.searchParkingInMunicipality(municipality, getUserId());
            ViewBag.message = "Parkings in " + municipality+" municipality";
            ViewBag.errorMessage = "No parkings found in "+municipality+" municipality :(";
            return View("SortableSearchResults", model);
        }
        //GET акција која ги дава деталите за сите паркинзи сортирани според оддалеченоста од корисникот во растечки редослед
        public ActionResult ByLocation()
        {
            var model = searchParkingsService.sortAllParkingsByDistance(getUserId());
            return View(model);
        }
        //GET акција која ги дава деталите за сите паркинзи кои се Bookmarked од тековно најавениот корисник
        public ActionResult Bookmarks()
        {
            var model = searchParkingsService.getAllBookmarkedParkingsForUser(getUserId());
            ViewBag.message = "Your Bookmarked Parkings";
            ViewBag.errorMessage = "You do not have any bookmarked parkings yet :(";
            return View("SortableSearchResults",model);
        }
        //Метод кој го враќа ID на тековно најавениот корисник
        public string getUserId()
        {
            return User.Identity.GetUserId();
        }
    }
}