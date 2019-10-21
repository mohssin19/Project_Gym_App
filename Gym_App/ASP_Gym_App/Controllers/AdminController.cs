using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ASP_Gym_App.Models;

namespace ASP_Gym_App.Controllers
{
    public class AdminController : Controller
    {
        // GET: /Login/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authorise()
        {
            return View();
        }



        // GET: Admin
        public ActionResult AddOrEdit(int id=0)
        {
            Admin adminModel = new Admin();
            return View(adminModel);
        }

        // POST: Admin
        [HttpPost]
        public ActionResult AddOrEdit(Admin adminModel)
        {
            using (GymAppDBEntities dbc = new GymAppDBEntities())
            {
                if(dbc.Admins.Any(x => x.Username == adminModel.Username))
                {
                    ViewBag.DuplicateMessage = $"User {adminModel.Username} already exists.";
                    return View("AddOrEdit", adminModel);
                }

                dbc.Admins.Add(adminModel);
                dbc.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = $"Registration of {adminModel.Username} Successful.";
            return View("AddOrEdit", new Admin());
        }
    }
}