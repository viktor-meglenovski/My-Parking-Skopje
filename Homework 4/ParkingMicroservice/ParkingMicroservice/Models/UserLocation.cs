using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ParkingMicroservice.Models
{
    public class UserLocation
    {
        public int UserLocationId { get; set; }
        public string UserId { get; set; }
        public Double Lattitude { get; set; }
        public Double Longitude { get; set; }
        public UserLocation() { }
        public UserLocation(string UserId, Double Lattitude, Double Longitude)
        {
            this.UserId = UserId;
            this.Lattitude = Lattitude;
            this.Longitude = Longitude;
        }
    }
}