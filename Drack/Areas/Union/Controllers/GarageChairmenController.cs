
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
    public class GarageChairmenController : Controller
    {
        private DrackContext db = new DrackContext();

        // GET: Union/GarageChairmen
        public ActionResult Index()
        {
            var garageChairmen = db.GarageChairmen.Include(g => g.Garage);
            return View(garageChairmen.ToList());
        }

        // GET: Union/GarageChairmen/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GarageChairman garageChairman = db.GarageChairmen.Find(id);
            if (garageChairman == null)
            {
                return HttpNotFound();
            }
            return View(garageChairman);
        }

        // GET: Union/GarageChairmen/Create
        public ActionResult Create()
        {
            ViewBag.GarageId = new SelectList(db.Garages, "GarageId", "GarageName");
            return View();
        }

        // POST: Union/GarageChairmen/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GarageChairmanId,FullName,PhoneNumber,Address,GarageImagePath,GarageId")] GarageChairman garageChairman)
        {
            if (ModelState.IsValid)
            {
                db.GarageChairmen.Add(garageChairman);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GarageId = new SelectList(db.Garages, "GarageId", "GarageName", garageChairman.GarageId);
            return View(garageChairman);
        }

        // GET: Union/GarageChairmen/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GarageChairman garageChairman = db.GarageChairmen.Find(id);
            if (garageChairman == null)
            {
                return HttpNotFound();
            }
            ViewBag.GarageId = new SelectList(db.Garages, "GarageId", "GarageName", garageChairman.GarageId);
            return View(garageChairman);
        }

        // POST: Union/GarageChairmen/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GarageChairmanId,FullName,PhoneNumber,Address,GarageImagePath,GarageId")] GarageChairman garageChairman)
        {
            if (ModelState.IsValid)
            {
                db.Entry(garageChairman).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GarageId = new SelectList(db.Garages, "GarageId", "GarageName", garageChairman.GarageId);
            return View(garageChairman);
        }

        // GET: Union/GarageChairmen/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GarageChairman garageChairman = db.GarageChairmen.Find(id);
            if (garageChairman == null)
            {
                return HttpNotFound();
            }
            return View(garageChairman);
        }

        // POST: Union/GarageChairmen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GarageChairman garageChairman = db.GarageChairmen.Find(id);
            db.GarageChairmen.Remove(garageChairman);
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
