using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyParkingSkopje.Models
{
    public class UserLocation
    {
        [Key]
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