using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchParkingsMicroservice.Models
{
    public class Bookmark
    {
        public int BookmarkId { get; set; }
        public int ParkingId { get; set; }
        public string UserId { get; set; }
        public Bookmark() { }
        public Bookmark(int ParkingId, string UserId)
        {
            this.ParkingId = ParkingId;
            this.UserId = UserId;
        }
    }
}