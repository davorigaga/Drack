using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Drack.Models;

namespace Drack.Controllers
{
    public class DriverNextOfKinsController : Controller
    {
        private DrackContext db = new DrackContext();

        // GET: Union/DriverNextOfKins
        public ActionResult Index()
        {
            var driverNextOfKins = db.DriverNextOfKins.Include(d => d.Driver);
            return View(driverNextOfKins.ToList());
        }

        // GET: Union/DriverNextOfKins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DriverNextOfKin driverNextOfKin = db.DriverNextOfKins.Find(id);
            if (driverNextOfKin == null)
            {
                return HttpNotFound();
            }
            return View(driverNextOfKin);
        }

        // GET: Union/DriverNextOfKins/Edit/5

        // GET: Union/DriverNextOfKins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DriverNextOfKin driverNextOfKin = db.DriverNextOfKins.Find(id);
            if (driverNextOfKin == null)
            {
                return HttpNotFound();
            }
            return View(driverNextOfKin);
        }

        // POST: Union/DriverNextOfKins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DriverNextOfKin driverNextOfKin = db.DriverNextOfKins.Find(id);
            db.DriverNextOfKins.Remove(driverNextOfKin);
            db.SaveChanges();
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
