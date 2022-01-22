using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SearchParkingsMicroservice.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Review> Reviews { get; set; }

        public AppDbContext() : base("DefaultConnection", throwIfV1Schema: false) { }
        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }
}