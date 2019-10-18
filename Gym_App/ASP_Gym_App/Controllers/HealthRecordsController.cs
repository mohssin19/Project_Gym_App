using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASP_Gym_App.Models;

namespace ASP_Gym_App.Controllers
{
    public class HealthRecordsController : Controller
    {
        private GymAppDBEntities db = new GymAppDBEntities();

        // GET: HealthRecords
        public ActionResult Index()
        {
            var healthRecords = db.HealthRecords.Include(h => h.User);
            return View(healthRecords.ToList());
        }

        // GET: HealthRecords/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthRecord healthRecord = db.HealthRecords.Find(id);
            if (healthRecord == null)
            {
                return HttpNotFound();
            }
            return View(healthRecord);
        }

        // GET: HealthRecords/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName");
            return View();
        }

        // POST: HealthRecords/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HealthRecordId,UserId,Age,Weight,Height,BMR,BMI,KCAL,TargetBMI")] HealthRecord healthRecord)
        {
            if (ModelState.IsValid)
            {
                db.HealthRecords.Add(healthRecord);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", healthRecord.UserId);
            return View(healthRecord);
        }

        // GET: HealthRecords/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthRecord healthRecord = db.HealthRecords.Find(id);
            if (healthRecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", healthRecord.UserId);
            return View(healthRecord);
        }

        // POST: HealthRecords/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "HealthRecordId,UserId,Age,Weight,Height,BMR,BMI,KCAL,TargetBMI")] HealthRecord healthRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(healthRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", healthRecord.UserId);
            return View(healthRecord);
        }

        // GET: HealthRecords/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HealthRecord healthRecord = db.HealthRecords.Find(id);
            if (healthRecord == null)
            {
                return HttpNotFound();
            }
            return View(healthRecord);
        }

        // POST: HealthRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HealthRecord healthRecord = db.HealthRecords.Find(id);
            db.HealthRecords.Remove(healthRecord);
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
