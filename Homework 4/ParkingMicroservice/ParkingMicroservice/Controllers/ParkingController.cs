using ParkingMicroservice.Models;
using ParkingMicroservice.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ParkingMicroservice.Controllers
{
    public class ParkingController : ApiController
    {
        private ParkingService parkingService { get; set; }
        public ParkingController()
        {
            this.parkingService = ParkingService.ParkingServiceInstance();
        }

        [HttpGet]
        [Route("api/parking")]
        public ParkingDetailsWithReviews getDetails(int id,string userId)
        {
            return parkingService.GetParkingDetails(id, userId);
        }

        [HttpGet]
        [Route("api/parking/userlocation")]
        public UserLocation getUserLocation(string userId)
        {
            return parkingService.getUserLocation(userId);
        }

        [HttpPost]
        [Route("api/parking/bookmark")]
        public Boolean bookmarkParking(string userId,int id)
        {
            //Ја враќаме новата состојба - дали паркингот после оваа акција е зачуван или не во листата на bookmarks за соодветниот корисник
            return parkingService.bookmarkParking(userId,id);
        }
    }
}
