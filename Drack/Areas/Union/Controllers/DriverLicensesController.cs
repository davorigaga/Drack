using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Drack.Models;

namespace Drack.Areas.Union.Controllers
{
    public class DriverLicensesController : Controller
    {
        private DrackContext db = new DrackContext();

        // GET: Union/DriverLicenses
        public ActionResult Index()
        {
            var driverLicenses = db.DriverLicenses.Include(d => d.Driver);
            return View(driverLicenses.ToList());
        }

        // GET: Union/DriverLicenses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DriverLicense driverLicense = db.DriverLicenses.Find(id);
            if (driverLicense == null)
            {
                return HttpNotFound();
            }
            return View(driverLicense);
        }

        // GET: Union/DriverLicenses/Create
        public ActionResult Create()
        {
            ViewBag.DriverId = new SelectList(db.Drivers, "DriverId", "FirstName");
            return View();
        }

        // POST: Union/DriverLicenses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DriverLicenseId,DriverId,DriverLicenseImagePath,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] DriverLicense driverLicense)
        {
            if (ModelState.IsValid)
            {
                db.DriverLicenses.Add(driverLicense);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DriverId = new SelectList(db.Drivers, "DriverId", "FirstName", driverLicense.DriverId);
            return View(driverLicense);
        }

        // GET: Union/DriverLicenses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DriverLicense driverLicense = db.DriverLicenses.Find(id);
            if (driverLicense == null)
            {
                return HttpNotFound();
            }
            ViewBag.DriverId = new SelectList(db.Drivers, "DriverId", "FirstName", driverLicense.DriverId);
            return View(driverLicense);
        }

        // POST: Union/DriverLicenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DriverLicenseId,DriverId,DriverLicenseImagePath,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] DriverLicense driverLicense)
        {
            if (ModelState.IsValid)
            {
                db.Entry(driverLicense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DriverId = new SelectList(db.Drivers, "DriverId", "FirstName", driverLicense.DriverId);
            return View(driverLicense);
        }

        // GET: Union/DriverLicenses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DriverLicense driverLicense = db.DriverLicenses.Find(id);
            if (driverLicense == null)
            {
                return HttpNotFound();
            }
            return View(driverLicense);
        }

        // POST: Union/DriverLicenses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DriverLicense driverLicense = db.DriverLicenses.Find(id);
            db.DriverLicenses.Remove(driverLicense);
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
