using MyParkingSkopje.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyParkingSkopje.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context { get; set; }
        public HomeController()
        {
            this._context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public void PopulateMunicipalities()
        {
            using (var reader = new StreamReader(@"C:\Users\Viktor Meglenovski\Documents\GitHub\My-Parking-Skopje\Homework 3\MyParkingSkopje\MyParkingSkopje\Content\Data\outputDataNew.txt", System.Text.Encoding.UTF8))
            {
                var line = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine();
                    var values = line.Split(',');
                    var temp = values[5];
                    var exists = _context.Municipalities.Where(x => x.MunicipalityName == temp).ToList();
                    if (exists.Count()== 0)
                    {
                        _context.Municipalities.Add(new Municipality(values[5]));
                    }
                    _context.SaveChanges();
                }
            }
        }
    }
}