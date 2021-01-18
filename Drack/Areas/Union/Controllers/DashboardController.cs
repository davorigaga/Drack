using Drack.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Drack.Areas.Union.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private DrackContext db = new DrackContext();

        public ActionResult Index()
        {
            ViewBag.Veryfied = db.Drivers.Where(c => c.Verified == true).Count();
            ViewBag.Active = db.Drivers.Where(c => c.DriverStatus == DriverStatus.Active).Count();
            ViewBag.Inactive = db.Drivers.Where(c => c.DriverStatus == DriverStatus.Inactive).Count();
            ViewBag.Suspended = db.Drivers.Where(c => c.DriverStatus == DriverStatus.Suspended).Count();
            return View();
        }
    }
}