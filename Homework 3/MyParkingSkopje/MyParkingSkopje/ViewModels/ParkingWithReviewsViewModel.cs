using MyParkingSkopje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyParkingSkopje.ViewModels
{
    public class ParkingWithReviewsViewModel
    {
        public Parking parking { get; set; }
        public float rating { get; set; }
        public int numberOfReviews { get; set; }
        public ParkingWithReviewsViewModel() { }
        public ParkingWithReviewsViewModel(Parking parking, float rating, int numberOfReviews) 
        {
            this.parking = parking;
            this.rating = rating;
            this.numberOfReviews = numberOfReviews;
        }
    }
}