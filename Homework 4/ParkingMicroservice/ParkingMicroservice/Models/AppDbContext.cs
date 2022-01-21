using Microsoft.AspNet.Identity.EntityFramework;
using ParkingMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ParkingMicroservice.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Parking> Parkings { get; set; }
        public DbSet<UserLocation> UserLocations { get; set; }
        public AppDbContext() : base("DefaultConnection", throwIfV1Schema: false) { }
        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }
}