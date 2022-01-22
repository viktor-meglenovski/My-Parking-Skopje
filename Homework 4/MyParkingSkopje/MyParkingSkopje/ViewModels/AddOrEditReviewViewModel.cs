using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyParkingSkopje.ViewModels
{
    public class AddOrEditReviewViewModel
    {
        public string userId { get; set; }
        public int parkingId { get; set; }
        public int stars { get; set; }
        public string reviewText { get; set; }
        
        public AddOrEditReviewViewModel() { }
        public AddOrEditReviewViewModel(string userId, int parkingId, int stars, string reviewText)
        {
            this.userId = userId;
            this.parkingId = parkingId;
            this.stars = stars;
            this.reviewText = reviewText;
        }
    }
}