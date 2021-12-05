using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyParkingSkopje.Models;
using MyParkingSkopje.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyParkingSkopje.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        protected ApplicationDbContext _context { get; set; }
        protected UserManager<ApplicationUser> _userManager;
        public ProfileController()
        {
            this._context = new ApplicationDbContext();
            this._userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this._context));

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult EditProfile()
        {
            var user = _userManager.FindById(User.Identity.GetUserId());
            var model = new EditProfileViewModel(user.Id, user.FirstName, user.LastName, user.ProfilePictureUrl);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditProfile(EditProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var toUpdate = _context.Users.Find(model.UserId);
                if (toUpdate != null)
                {
                    if (toUpdate.Id != model.UserId)
                        return Content("no permissions");

                    toUpdate.FirstName = model.FirstName;
                    toUpdate.LastName = model.LastName;

                    if (Request.Files.Count != 0)
                    {
                        var file = Request.Files[0];
                        var fileName = Path.GetFileName(file.FileName);
                        if (fileName != "")
                        {
                            var path = Path.Combine(Server.MapPath("~/UserUploads"), toUpdate.Id, fileName);
                            file.SaveAs(path);
                            toUpdate.ProfilePictureUrl = Path.Combine(@"/UserUploads", toUpdate.Id, fileName);
                        }
                    }
                    _context.SaveChanges();
                    return RedirectToAction("Index","Home");
                }
                return new HttpNotFoundResult();
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult ProfileImageUpload()
        {
            if (Request.Files.Count != 0)
            {
                var file = Request.Files[0];
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/UserUploads/Temp"), fileName);
                file.SaveAs(path);
                var newPath = Path.Combine(@"/UserUploads/Temp", Path.GetFileName(fileName));
                return Json(new { success = true, newImagePath = newPath }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }
    }
}