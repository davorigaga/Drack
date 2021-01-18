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
    public class DriversController : Controller
    {
        private DrackContext db = new DrackContext();

        // GET: Union/Drivers
        public ActionResult Index()
        {
            var drivers = db.Drivers.Include(d => d.Garage);
            return View(drivers.ToList());
        }

        // GET: Union/Drivers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = db.Drivers.Find(id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            return View(driver);
        }

        // GET: Union/Drivers/Create
        public ActionResult Create()
        {
            ViewBag.GarageId = new SelectList(db.Garages, "GarageId", "GarageName");
            return View();
        }

        // POST: Union/Drivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DriverId,FirstName,LastName,OtherNames,DateOfBirth,MaritalStatus,Gender,Email,PhoneNumber,State,City,Address,ImagePath,GarageId,DriverStatus,Verified,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] Driver driver)
        {
            if (ModelState.IsValid)
            {
                db.Drivers.Add(driver);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GarageId = new SelectList(db.Garages, "GarageId", "GarageName", driver.GarageId);
            return View(driver);
        }

        // GET: Union/Drivers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = db.Drivers.Find(id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            ViewBag.GarageId = new SelectList(db.Garages, "GarageId", "GarageName", driver.GarageId);
            return View(driver);
        }

        // POST: Union/Drivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DriverId,FirstName,LastName,OtherNames,DateOfBirth,MaritalStatus,Gender,Email,PhoneNumber,State,City,Address,ImagePath,GarageId,DriverStatus,Verified,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] Driver driver)
        {
            if (ModelState.IsValid)
            {
                db.Entry(driver).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GarageId = new SelectList(db.Garages, "GarageId", "GarageName", driver.GarageId);
            return View(driver);
        }

        // GET: Union/Drivers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = db.Drivers.Find(id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            return View(driver);
        }

        // POST: Union/Drivers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Driver driver = db.Drivers.Find(id);
            db.Drivers.Remove(driver);
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
