using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyParkingSkopje.Models
{
    public class Bookmark
    {
        [Key]
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