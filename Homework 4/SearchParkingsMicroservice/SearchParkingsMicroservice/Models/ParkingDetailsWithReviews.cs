using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SearchParkingsMicroservice.Models;

namespace SearchParkingsMicroservice.Models
{
    public class ParkingDetailsWithReviews
    {
        public Parking parking { get; set; }
        public float rating { get; set; }
        public int numberOfReviews { get; set; }
        public Double distance { get; set; }
        public List<ReviewDetails> reviewsDetails { get; set; }
        public Review existingReview { get; set; }
        public Boolean bookmarked { get; set; }
        public ParkingDetailsWithReviews()
        {
            this.reviewsDetails = new List<ReviewDetails>();
        }
        public ParkingDetailsWithReviews(Parking parking, float rating, int numberOfReviews, Double distance, List<ReviewDetails> reviewsDetails, Boolean bookmarked, Review existingReview)
        {
            this.parking = parking;
            this.rating = rating;
            this.numberOfReviews = numberOfReviews;
            this.distance = distance;
            this.reviewsDetails = new List<ReviewDetails>();
            this.reviewsDetails = reviewsDetails;
            this.bookmarked = bookmarked;
            this.existingReview = existingReview;
        }
    }
}