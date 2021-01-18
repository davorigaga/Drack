using Drack.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Drack.Areas.Union.Controllers
{
    [Authorize(Roles = "Admin")]

    public class AppUsersController : Controller
    {
        private DrackContext db = new DrackContext();

        ApplicationDbContext AspDb;
        public AppUsersController()
        {
            AspDb = new ApplicationDbContext();
        }

        // GET: SystemSettings
        public ActionResult Index()
        {
            var Users = AspDb.Users.OrderByDescending(g => g.Id).ToList();
            return View(Users);
        }

        // GET: User
        [HttpGet]
        public ActionResult Search(string s)
        {
            if (s != null)
            {

                ViewBag.SearchParam = s;
                var appUser = (from x in AspDb.Users where x.UserName.Contains(s) || x.Email.Contains(s) || x.FullName.Contains(s) select x).OrderByDescending(f => f.Id).ToList();

                return View("Index", appUser);

            }
            return View("Index");
        }

        [HttpGet]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = AspDb.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(AspDb));

            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(AspDb));

            var roles = AspDb.Roles.ToList();

            foreach (var role in roles)
            {
                if (!UserManager.IsInRole(user.Id, "Developer"))
                {
                    if (UserManager.IsInRole(user.Id, role.Name))
                    {
                        UserManager.RemoveFromRole(user.Id, role.Name);
                    }

                }
            }
            if (!UserManager.IsInRole(user.Id, "Developer"))
            {
                UserManager.Delete(user);
                db.Drivers.Remove(db.Drivers.FirstOrDefault(c => c.CreatedBy == user.Id));
            }
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}