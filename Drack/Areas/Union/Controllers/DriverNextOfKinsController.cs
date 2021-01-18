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

        // GET: Union/DriverNextOfKins/Create
        public ActionResult Create()
        {
            ViewBag.DriverId = new SelectList(db.Drivers, "DriverId", "FirstName");
            return View();
        }

        // POST: Union/DriverNextOfKins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DriverNextOfKinId,DriverId,FirstName,LastName,OtherNames,DateOfBirth,MaritalStatus,Gender,Email,PhoneNumber,State,City,Address,ImagePath,Relationship,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] DriverNextOfKin driverNextOfKin)
        {
            if (ModelState.IsValid)
            {
                db.DriverNextOfKins.Add(driverNextOfKin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DriverId = new SelectList(db.Drivers, "DriverId", "FirstName", driverNextOfKin.DriverId);
            return View(driverNextOfKin);
        }

        // GET: Union/DriverNextOfKins/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.DriverId = new SelectList(db.Drivers, "DriverId", "FirstName", driverNextOfKin.DriverId);
            return View(driverNextOfKin);
        }

        // POST: Union/DriverNextOfKins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DriverNextOfKinId,DriverId,FirstName,LastName,OtherNames,DateOfBirth,MaritalStatus,Gender,Email,PhoneNumber,State,City,Address,ImagePath,Relationship,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] DriverNextOfKin driverNextOfKin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(driverNextOfKin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DriverId = new SelectList(db.Drivers, "DriverId", "FirstName", driverNextOfKin.DriverId);
            return View(driverNextOfKin);
        }

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
