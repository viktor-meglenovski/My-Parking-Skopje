using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchParkingsMicroservice.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string UserId { get; set; }
        public int ParkingId { get; set; }
        public int Stars { get; set; }
        public string ReviewText { get; set; }
        public DateTime datetime { get; set; }
        public Review() { }
        public Review(string UserId, int ParkingId, int Stars, string ReviewText)
        {
            this.UserId = UserId;
            this.ParkingId = ParkingId;
            this.Stars = Stars;
            this.ReviewText = ReviewText;
            this.datetime = DateTime.Now;
        }

    }
}