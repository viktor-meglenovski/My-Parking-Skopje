using MyParkingSkopje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyParkingSkopje.ViewModels
{
    public class ParkingDetails
    {
        public Parking parking { get; set; }
        public float rating { get; set; }
        public int numberOfReviews { get; set; }
        public Double distance { get; set; }
        public ParkingDetails() { }
        public ParkingDetails(Parking parking, float rating, int numberOfReviews, Double distance)
        {
            this.parking = parking;
            this.rating = rating;
            this.numberOfReviews = numberOfReviews;
            this.distance = distance;
        }
    }
}