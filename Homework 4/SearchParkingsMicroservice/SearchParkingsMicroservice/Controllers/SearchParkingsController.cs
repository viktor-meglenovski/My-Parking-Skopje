using SearchParkingsMicroservice.Models;
using SearchParkingsMicroservice.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SearchParkingsMicroservice.Controllers
{
    //REST API контролер кој ги обработува барањата поврзани со пребарување на паркинзи
    public class SearchParkingsController : ApiController
    {
        private SearchParkingsService searchParkingsService { get; set; }
        public SearchParkingsController()
        {
            this.searchParkingsService = SearchParkingsService.SearchParkingsServiceInstance();
        }
        //GET акција која ги враќа потребните податоци за формирање на главната страница за пребарување (листа со општини и топ 10 паркинзи)
        [HttpGet]
        [Route("api/search/index")]
        public SearchParkingsIndexViewModel Index(string id)
        {
            return searchParkingsService.getSearchParkingsIndexViewModel(id);
        }

        //GET акција која ги филтрира паркинзите според тоа дали нивното име го содржи стрингот испратен како параметар
        [HttpGet]
        [Route("api/search/name")]
        public List<ParkingDetailsWithReviews> ByName(string name, string id)
        {
            return searchParkingsService.searchParkingsByNameContaining(name, id);
        }
        //GET акција која ги филтрира паркинзите според тоа дали тие се наоѓаат во општината пуштена како параметар
        [HttpGet]
        [Route("api/search/municipality")]
        public List<ParkingDetailsWithReviews> ByMunicipality(string municipality,string id)
        {
            return searchParkingsService.searchParkingInMunicipality(municipality, id);
        }
        //GET акција која ги дава деталите за сите паркинзи сортирани според оддалеченоста од корисникот во растечки редослед
        [HttpGet]
        [Route("api/search/location")]
        public List<ParkingDetailsWithReviews> ByLocation(string id)
        {
            return searchParkingsService.sortAllParkingsByDistance(id);
        }
        //GET акција која ги дава деталите за сите паркинзи кои се Bookmarked од тековно најавениот корисник
        [HttpGet]
        [Route("api/search/bookmarks")]
        public List<ParkingDetailsWithReviews> Bookmarks(string id)
        {
            return searchParkingsService .getAllBookmarkedParkingsForUser(id);
        }

    }
}
