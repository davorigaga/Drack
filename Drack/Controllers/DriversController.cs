using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Drack.Models;
using Drack.Extensions;

namespace Drack.Controllers
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

        public ActionResult Search(string s)
        {
            List<Driver> drivers = new List<Driver>();
            if (s != null)
            {

                ViewBag.SearchParam = s;
                drivers = (from x in db.Drivers.Include(d => d.Garage) where x.Email.Contains(s) || (x.FirstName + " " + x.LastName + " " + x.OtherNames).Contains(s) || x.Address.Contains(s) || x.State.Contains(s) || x.City.Contains(s) || x.PhoneNumber.Contains(s) || x.Garage.GarageName.Contains(s) || x.Garage.GarageNumber.Contains(s) select x).OrderByDescending(f => f.DriverId).ToList();
            }
            return View(drivers);
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

        // GET: Union/Drivers/About/5
        public ActionResult About()
        {
            var userId = User.Identity.GetId();
            if (string.IsNullOrEmpty(userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = db.Drivers.Include(c => c.Garage).FirstOrDefault(c => c.CreatedBy == userId);
            if (driver == null)
            {
                return HttpNotFound();
            }
            return View(driver);
        }

        // GET: Union/Drivers/Edit/5
        [Authorize]
        public ActionResult Update()
        {
            var userId = User.Identity.GetId();
            if (string.IsNullOrEmpty(userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = db.Drivers.Include(c => c.Garage).FirstOrDefault(c => c.CreatedBy == userId);
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
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Include = "DriverId,FirstName,LastName,OtherNames,DateOfBirth,MaritalStatus,Gender,Email,PhoneNumber,State,City,Address,ImagePath,GarageId,DriverStatus,Verified,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] Driver driver, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    string fileType = System.IO.Path.GetExtension(ImageFile.FileName);
                    string file = System.IO.Path.GetFileName(DateTime.Now.Ticks + fileType);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Resources/images/drivers"), file);

                    //file upload
                    ImageFile.SaveAs(path);
                    //updating imagepath
                    string fullPath = Request.MapPath("~/Repository/images/drivers/" + driver.ImagePath);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    driver.ImagePath = file;
                }
                var userId = User.Identity.GetId();
                driver.ModifiedDate = DateTime.Now;
                driver.ModifiedBy = userId;
                db.Entry(driver).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("About");
            }
            ViewBag.GarageId = new SelectList(db.Garages, "GarageId", "GarageName", driver.GarageId);
            return View(driver);
        }

        public ActionResult License()
        {
            var userId = User.Identity.GetId();
            if (string.IsNullOrEmpty(userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = db.Drivers.Include(c => c.Garage).FirstOrDefault(c => c.CreatedBy == userId);
            DriverLicense driverLicense = db.DriverLicenses.FirstOrDefault(d => d.DriverId == driver.DriverId);
            return View(driverLicense);
        }
        // GET: Union/DriverLicenses/Create
        public ActionResult AddLicense()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddLicense([Bind(Include = "DriverLicenseId,DriverId,DriverLicenseImagePath,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] DriverLicense driverLicense, HttpPostedFileBase LicenseImageFile)
        {
            if (ModelState.IsValid)
            {
                if (LicenseImageFile != null)
                {
                    string fileType = System.IO.Path.GetExtension(LicenseImageFile.FileName);
                    string file = System.IO.Path.GetFileName(DateTime.Now.Ticks + fileType);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Resources/images/drivers"), file);

                    //file upload
                    LicenseImageFile.SaveAs(path);
                    driverLicense.DriverLicenseImagePath = file;
                }
                var userId = User.Identity.GetId();
                driverLicense.CreatedDate = DateTime.Now;
                driverLicense.CreatedBy = userId;
                driverLicense.ModifiedDate = DateTime.Now;
                driverLicense.ModifiedBy = userId;

                if (string.IsNullOrEmpty(userId))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Driver driver = db.Drivers.Include(c => c.Garage).FirstOrDefault(c => c.CreatedBy == userId);
                driverLicense.DriverId = driver.DriverId;
                db.DriverLicenses.Add(driverLicense);
                db.SaveChanges();
                return RedirectToAction("License");
            }
            return View(driverLicense);
        }

        // GET: Union/DriverLicenses/Edit/5
        public ActionResult UpdateLicense()
        {
            var userId = User.Identity.GetId();
            if (string.IsNullOrEmpty(userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = db.Drivers.Include(c => c.Garage).FirstOrDefault(c => c.CreatedBy == userId);
            DriverLicense driverLicense = db.DriverLicenses.FirstOrDefault(c => c.DriverId == driver.DriverId);
            if (driverLicense == null)
            {
                return HttpNotFound();
            }
            return View(driverLicense);
        }

        // POST: Union/DriverLicenses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateLicense([Bind(Include = "DriverLicenseId,DriverId,DriverLicenseImagePath,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] DriverLicense driverLicense, HttpPostedFileBase LicenseImageFile)
        {
            if (ModelState.IsValid)
            {
                if (LicenseImageFile != null)
                {
                    string fileType = System.IO.Path.GetExtension(LicenseImageFile.FileName);
                    string file = System.IO.Path.GetFileName(DateTime.Now.Ticks + fileType);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Resources/images/drivers"), file);

                    //file upload
                    LicenseImageFile.SaveAs(path);
                    //updating imagepath
                    string fullPath = Request.MapPath("~/Repository/images/drivers/" + driverLicense.DriverLicenseImagePath);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    driverLicense.DriverLicenseImagePath = file;
                }
                var userId = User.Identity.GetId();
                driverLicense.ModifiedDate = DateTime.Now;
                driverLicense.ModifiedBy = userId;

                if (string.IsNullOrEmpty(userId))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Driver driver = db.Drivers.Include(c => c.Garage).FirstOrDefault(c => c.CreatedBy == userId);
                driverLicense.DriverId = driver.DriverId;
                db.Entry(driverLicense).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("License");
            }
            return View(driverLicense);
        }

        // GET: Union/DriverLicenses/Delete/5
        public ActionResult DeleteLicense()
        {
            var userId = User.Identity.GetId();
            if (string.IsNullOrEmpty(userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = db.Drivers.Include(c => c.Garage).FirstOrDefault(c => c.CreatedBy == userId);
            DriverLicense driverLicense = db.DriverLicenses.FirstOrDefault(d => d.DriverId == driver.DriverId);
            db.DriverLicenses.Remove(driverLicense);
            db.SaveChanges();
            return RedirectToAction("License");
        }


        public ActionResult NextOfKin()
        {
            var userId = User.Identity.GetId();
            if (string.IsNullOrEmpty(userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = db.Drivers.Include(c => c.Garage).FirstOrDefault(c => c.CreatedBy == userId);
            DriverNextOfKin driverNextOfKin = db.DriverNextOfKins.FirstOrDefault(d => d.DriverId == driver.DriverId);
            return View(driverNextOfKin);
        }
        // GET: Union/DriverNextOfKins/Create
        public ActionResult AddNextOfKin()
        {
            return View();
        }

        // POST: Union/DriverNextOfKins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddNextOfKin([Bind(Include = "DriverNextOfKinId,DriverId,FirstName,LastName,OtherNames,DateOfBirth,MaritalStatus,Gender,Email,PhoneNumber,State,City,Address,ImagePath,Relationship,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] DriverNextOfKin driverNextOfKin, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    string fileType = System.IO.Path.GetExtension(ImageFile.FileName);
                    string file = System.IO.Path.GetFileName(DateTime.Now.Ticks + fileType);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Resources/images/drivers"), file);

                    //file upload
                    ImageFile.SaveAs(path);
                    driverNextOfKin.ImagePath = file;
                }
                var userId = User.Identity.GetId();
                driverNextOfKin.CreatedDate = DateTime.Now;
                driverNextOfKin.CreatedBy = userId;
                driverNextOfKin.ModifiedDate = DateTime.Now;
                driverNextOfKin.ModifiedBy = userId;

                if (string.IsNullOrEmpty(userId))
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Driver driver = db.Drivers.Include(c => c.Garage).FirstOrDefault(c => c.CreatedBy == userId);
                driverNextOfKin.DriverId = driver.DriverId;

                db.DriverNextOfKins.Add(driverNextOfKin);
                db.SaveChanges();
                return RedirectToAction("NextOfKin");
            }

            return View(driverNextOfKin);
        }

        public ActionResult UpdateNextOfKin()
        {
            var userId = User.Identity.GetId();
            if (string.IsNullOrEmpty(userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = db.Drivers.Include(c => c.Garage).FirstOrDefault(c => c.CreatedBy == userId);
            DriverNextOfKin driverNextOfKin = db.DriverNextOfKins.FirstOrDefault(c => c.DriverId == driver.DriverId);
            if (driverNextOfKin == null)
            {
                return HttpNotFound();
            }
            return View(driverNextOfKin);
        }

        // POST: Union/DriverNextOfKins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateNextOfKin([Bind(Include = "DriverNextOfKinId,DriverId,FirstName,LastName,OtherNames,DateOfBirth,MaritalStatus,Gender,Email,PhoneNumber,State,City,Address,ImagePath,Relationship,CreatedDate,CreatedBy,ModifiedDate,ModifiedBy")] DriverNextOfKin driverNextOfKin, HttpPostedFileBase ImageFile)
        {
            if (ModelState.IsValid)
            {
                if (ImageFile != null)
                {
                    string fileType = System.IO.Path.GetExtension(ImageFile.FileName);
                    string file = System.IO.Path.GetFileName(DateTime.Now.Ticks + fileType);
                    string path = System.IO.Path.Combine(Server.MapPath("~/Resources/images/drivers"), file);

                    //file upload
                    ImageFile.SaveAs(path);
                    //updating imagepath
                    string fullPath = Request.MapPath("~/Repository/images/drivers/" + driverNextOfKin.ImagePath);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                    driverNextOfKin.ImagePath = file;
                }
                var userId = User.Identity.GetId();
                driverNextOfKin.ModifiedDate = DateTime.Now;
                driverNextOfKin.ModifiedBy = userId;
                db.Entry(driverNextOfKin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("NextOfKin");
            }
            return View(driverNextOfKin);
        }

        public ActionResult DeleteNextOfKin()
        {
            var userId = User.Identity.GetId();
            if (string.IsNullOrEmpty(userId))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = db.Drivers.Include(c => c.Garage).FirstOrDefault(c => c.CreatedBy == userId);
            DriverNextOfKin driverNextOfKin = db.DriverNextOfKins.FirstOrDefault(d => d.DriverId == driver.DriverId);
            db.DriverNextOfKins.Remove(driverNextOfKin);
            db.SaveChanges();
            return RedirectToAction("NextOfKin");
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
