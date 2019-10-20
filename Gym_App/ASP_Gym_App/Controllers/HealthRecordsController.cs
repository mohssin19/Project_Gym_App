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
        public ActionResult Create([Bind(Include = "HealthRecordId,UserId,Age,Weight,Height")] HealthRecord healthRecord, int gender, int exFrequency)
        {
            bool error = true;
            #region Calculations
            // BMI CALUCULATIONS
            double bmi = Math.Round((((double)healthRecord.Weight / (double)healthRecord.Height / (double)healthRecord.Height) * 10000), 2);
            healthRecord.BMI = (decimal)bmi;
            // BMR
            if (gender == 1)// Female
            {
                double bmr = Math.Round(447.593 + (9.247 * (double)healthRecord.Weight) + (3.098 * (double)healthRecord.Height) - (4.330 * (double)healthRecord.Age), 2);
                healthRecord.BMR = (decimal)bmr;
            }
            else // Male (Default)
            {
                double bmr = Math.Round(88.362 + (13.397 * (double)healthRecord.Weight) + (4.799 * (double)healthRecord.Height) - (5.677 * (double)healthRecord.Age), 2);
                healthRecord.BMR = (decimal)bmr;
            }
            
            //Kcal
            double Kcal = CalculateKcal(exFrequency,(decimal)healthRecord.BMR);
            healthRecord.KCAL = (decimal)Kcal;
            
            #endregion

            HealthRecord existingHealthRecord = db.HealthRecords.Where(hr => hr.UserId == healthRecord.UserId).FirstOrDefault();
            if(existingHealthRecord != null)
            {
                //update
                existingHealthRecord.Age = healthRecord.Age;
                existingHealthRecord.Height = healthRecord.Height;
                existingHealthRecord.Weight = healthRecord.Weight;
                existingHealthRecord.BMI = healthRecord.BMI;
                existingHealthRecord.BMR = healthRecord.BMR;
                existingHealthRecord.KCAL = healthRecord.KCAL;
                db.SaveChanges();
                error = false;
            }
            else
            {
                //add
                db.HealthRecords.Add(healthRecord);
                db.SaveChanges();
                error = false;
            }
            if(error)
            {
                return RedirectToAction("Index");
            }

            if (ModelState.IsValid)
            {
                //db.HealthRecords.Add(healthRecord);
                //db.SaveChanges();
                //return RedirectToAction("Index");
                ModelState.Clear();
            }


            //ViewBag.UserId = new SelectList(db.Users, "UserId", "FirstName", healthRecord.UserId);
            //return View(healthRecord);
            return RedirectToAction("Index");


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

        [NonAction]
        public double CalculateKcal(int excerciseLvl, decimal BMR )
        {
            decimal cal = 0;

            switch (excerciseLvl)
            {
                case 1:
                    cal = 1.2m;
                    break;
                case 2:
                    cal = 1.375m;
                    break;
                case 3:
                    cal = 1.55m;
                    break;
                case 4:
                    cal = 1.725m;
                    break;
                case 5:
                    cal = 1.9m;
                    break;
                default:
                    cal = 1.2m;
                    break;
            }
            //Kcal
            double Kcal = (double)(BMR * cal);
           
            return Kcal;
        }
    }

}
