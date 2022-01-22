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

        //GET акција која ги враќа деталите за паркингот со соодвентото ID за корисникот со соодветното ID
        [HttpGet]
        [Route("api/parking")]
        public ParkingDetailsWithReviews getDetails(int id,string userId)
        {
            return parkingService.GetParkingDetails(id, userId);
        }

        //GET акција која ја враќа тековно зачуваната локација на корисникот со ID испратено како параметар
        [HttpGet]
        [Route("api/parking/userlocation")]
        public UserLocation getUserLocation(string userId)
        {
            return parkingService.getUserLocation(userId);
        }

        //GET акција која се справува со зачувување и бришење на паркинзи од листата на зачувани паркинзи за секој корисник
        [HttpGet]
        [Route("api/parking/bookmark")]
        public Boolean bookmarkParking(string userId,int id)
        {
            //Ја враќаме новата состојба - дали паркингот после оваа акција е зачуван или не во листата на bookmarks за соодветниот корисник
            return parkingService.bookmarkParking(userId,id);
        }
    }
}
