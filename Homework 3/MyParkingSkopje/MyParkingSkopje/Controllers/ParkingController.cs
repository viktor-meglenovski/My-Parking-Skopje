using MyParkingSkopje.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyParkingSkopje.Controllers
{
    public class ParkingController : Controller
    {
        private ApplicationDbContext _context { get; set; }
        public ParkingController()
        {
            this._context = new ApplicationDbContext();
        }
        public ActionResult ListParkings()
        {
            var model = _context.Parkings.ToList();
            return View(model);
        }
        public ActionResult Details(int id)
        {
            var model = _context.Parkings.Find(id);
            return View(model);
        }
    }
}