using Microsoft.AspNet.Identity;
using MyParkingSkopje.Models;
using MyParkingSkopje.Service;
using MyParkingSkopje.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyParkingSkopje.Controllers
{
    //Контролер за листање и добивање на детални информации за паркинзите
    [Authorize]
    public class ParkingController : Controller
    {
        //Инстанца од контекстот на базата на податоци која се користи
        private ApplicationDbContext _context { get; set; }
        //Класа во која се наоѓа бизнис логиката за овој контролер
        private ParkingService parkingService { get; set; }
        public ParkingController()
        {
            this._context = new ApplicationDbContext();
            this.parkingService = new ParkingService();
        }
        //GET акција која ги листа сите паркинзи без дополнителни детали
        public ActionResult ListParkings()
        {
            var model = _context.Parkings.ToList();
            return View(model);
        }
        //GET акција која ги враќа сите информации и детали за паркингот со ID пратено како параметар
        public ActionResult Details(int id)
        {
            //Го земаме тековниот најавен корисник
            var userId = getUserId();

            //Проверка дали во базата имаме зачувано локација за тековниот корисник
            var userLocation = parkingService.getUserLocation(userId);
            //Доколку во базата нема зачувано локација за тековниот корисник, правиме редирекција кон почетната страна на апликацијата
            if (userLocation == null)
                return RedirectToAction("Index", "Home");

            //Во ViewBag ги зачувуваме координатите на локацијата на тековниот корисник
            ViewBag.userLatitude = userLocation.Lattitude;
            ViewBag.userLongitude = userLocation.Longitude;

            //Во моделот поставуваме објект од класата GetParkingDetails во кој ги имаме сите детали за паркингот
            var model = parkingService.GetParkingsDetails(id, userId);

            return View(model);
        }
        
        //JSON Акција со која го зачувуваме паркингот со ID пратено како параметар во листата на зачувани паркинзи за тековниот корисник
        public ActionResult BookmarkParking(int id)
        {
            //Го земаме тековно најавениот корисник
            var userId = getUserId();

            //Го повикуваме соодветниот метод од ParkingService
            var newState = parkingService.bookmarkParking(userId, id);

            //Враќаме успешен JSON одговор во кој параметарот newState ја одредува новата состојба (дали сега корисникот го има или го нема зачувано паркингот во својата листа)
            //Овој параметар се користи на front за да се одреди која икона (празно или полно срце) треба да се прикаже на страната.
            return Json(new { success = true, newState = newState }, JsonRequestBehavior.AllowGet);
        }
        //Метод кој го враќа ID на тековно најавениот корисник
        public string getUserId()
        {
            return User.Identity.GetUserId();
        }
    }
}