﻿using Microsoft.AspNet.Identity;
using MyParkingSkopje.Models;
using MyParkingSkopje.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyParkingSkopje.Controllers
{
    [Authorize]
    public class SearchParkingsController : Controller
    {
        private ApplicationDbContext _context { get; set; }
        public SearchParkingsController()
        {
            this._context = new ApplicationDbContext();
        }
        //glavnata strana kade ima kopcinja za razlicni nacini na search
        public ActionResult Index()
        {
            var municipalities = _context.Municipalities.ToList();

            //5te parkinzi so najdobar rejting shto se prikazuvaat na glavnata stranica
            var topFiveParkings = GetParkingsDetails(_context.Parkings.ToList());
            topFiveParkings.OrderBy(x => x.rating).Reverse();
            topFiveParkings.GetRange(0, 5);

            var model = new SearchParkingsIndexViewModel(municipalities, topFiveParkings);

            return View(model);
        }
        //search spored ime
        public ActionResult ByName(string name)
        {
            var parkings = _context.Parkings.Where(x => x.Name.Contains(name));
            var model = GetParkingsDetails(parkings.ToList());
            model.OrderBy(x => x.rating);
            ViewBag.searchText = name;
            return View(model);
        }
        //search spored opstina
        public ActionResult ByMunicipality(string municipality)
        {
            var parkings = _context.Parkings.Where(x => x.Municipality == municipality);
            var model = GetParkingsDetails(parkings.ToList());
            model.OrderBy(x => x.distance);
            ViewBag.municipality = municipality;
            return View(model);
        }
        //search spored lokacija
        public ActionResult ByLocation()
        {
            var parkings = _context.Parkings;
            var model = GetParkingsDetails(parkings.ToList());
            model.OrderBy(x => x.distance);
            return View(model);
        }
        public ActionResult Bookmarks()
        {
            var userId = User.Identity.GetUserId();
            var parkingIds = _context.Bookmarks.Where(x => x.UserId == userId).Select(x=>x.ParkingId);
            var parkings = _context.Parkings.Where(x => parkingIds.Contains(x.ParkingId));
            var model = GetParkingsDetails(parkings.ToList());
            return View(model);
        }
        public List<ParkingDetails> GetParkingsDetails(List<Parking> parkings)
        {
            var result = new List<ParkingDetails>();
            foreach (Parking p in parkings)
            {
                var reviews = _context.Reviews.Where(x => x.ParkingId == p.ParkingId);
                int numberOfReviews = reviews.Count();
                float rating = 0;
                if (numberOfReviews == 0)
                    rating = 0;
                else
                {
                    foreach (Review r in reviews)
                        rating += r.Stars;
                    rating = ((float)rating) / numberOfReviews;
                }
                var userId = User.Identity.GetUserId();
                var userLocation = _context.UserLocations.Where(x => x.UserId == userId).First();
                var distance = DistanceBetweenTwoCoordinates(userLocation.Lattitude, userLocation.Longitude, p.Lattitude, p.Longitude);
                result.Add(new ParkingDetails(p, rating, numberOfReviews,distance));
            }
            return result;
        }
        public void UpdateUserLocation(double lattitude, double longitude)
        {
            var userId = User.Identity.GetUserId();
            var existing = _context.UserLocations.Where(x => x.UserId == userId);
            if (existing.Count() == 0)
            {
                _context.UserLocations.Add(new UserLocation(userId, lattitude, longitude));
            }
            else
            {
                var existingLocation = existing.First();
                existingLocation.Lattitude = lattitude;
                existingLocation.Longitude = longitude;
                _context.SaveChanges();
            }
        }
        public double DistanceBetweenTwoCoordinates(double lat1,double lon1,double lat2, double lon2)
        {
            var R = 6371; // Radius of the earth in km
            var dLat = DegreesToRadians(lat2 - lat1);  // deg2rad below
            var dLon = DegreesToRadians(lon2 - lon1);
            var a =
              Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
              Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
              Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var d = R * c; // Distance in km
            return d;
        }
        public double DegreesToRadians(double deg)
        {
            return deg * (Math.PI / 180);
        }
    }
}