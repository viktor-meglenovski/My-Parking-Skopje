using Microsoft.AspNet.Identity;
using MyParkingSkopje.Service;
using MyParkingSkopje.ViewModels;
using System.IO;
using System.Web.Mvc;

namespace MyParkingSkopje.Controllers
{
    //Контролер кој менаџира со корисничкиот профил
    [Authorize]
    public class ProfileController : Controller
    {
        //Класа во која се наоѓа бизнис логиката за овој контролер
        private ProfileService profileService { get; set; }
        public ProfileController()
        {
            this.profileService = ProfileService.ProfileServiceInstance();
        }
        //GET акција која враќа HTML страна преку која се ажурира корисничкиот профил
        public ActionResult EditProfile()
        {
            //Правиме инстанца од EditProfileViewModel врз основа на тековно најавениот корисник
            var model =profileService.getEditProfileViewModel(getUserId());
            return View(model);
        }
     
        //POST акција преку која се добиваат податоци за ажурирање на корисничкиот профил
        [HttpPost]
        public ActionResult EditProfile(EditProfileViewModel model)
        {
            //Ако сме добиле валидни податоци
            if (ModelState.IsValid)
            {
                //Го ажурираме корисникот
                profileService.editUserProfile(model,getUserId(), Server.MapPath("~/UserUploads"),Request.Files);
                //Редиректираме кон почетната страница на апликацијата
                return RedirectToAction("Index","Home");
            }
            //Ако сме добиле невалидни податоци, се враќаме на истата форма
            return View(model);
        }

        //JSON POST акција преку која привремено се зачувува фајл кој се испраќа до серверот
        [HttpPost]
        public ActionResult ProfileImageUpload()
        {
            //Ако во барањето има прикачен фајл:
            if (Request.Files.Count != 0)
            {
                var file = Request.Files[0];
                //Фајлот се зачувува на предодредена привремена локација
                var newPath = profileService.saveImageToTempFolder(file, Server.MapPath("~/UserUploads/Temp"));
                //Се враќа успешен JSON одговор и параметар newImagePath кој ја означува патеката на ново поставениот фајл
                return Json(new { success = true, newImagePath = newPath }, JsonRequestBehavior.AllowGet);
            }
            //Ако во барањето не се прикачени фајлови се враќа неуспешен JSON одговор
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
        //Метод кој го враќа ID на тековно најавениот корисник
        public string getUserId()
        {
            return User.Identity.GetUserId();
        }
    }
}