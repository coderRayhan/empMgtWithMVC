using _1265125_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _1265125_MVC.Controllers
{
    public class EmploymentInfoController : Controller
    {
        Context db = new Context();
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Insert()
        {
            ViewBag.Personal = db.PersonalInfos.ToList();
            ViewBag.Data = db.Departments.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Insert(EmploymentInfo employmentInfo)
        {
            if (ModelState.IsValid)
            {
                db.EmploymentInfos.Add(employmentInfo);
                db.SaveChanges();
                return RedirectToAction("Index", "PersonalInfo");
            }
            else
            {
                ViewBag.Personal = db.PersonalInfos.ToList();
                ViewBag.Error = ModelState.Values.SelectMany(m => m.Errors).Select(m => m.ErrorMessage);
                ViewBag.Data = db.Departments.ToList();
                return View();
            }

        }

        public ActionResult GetEmployeeName()
        {
            var data = from employee in db.PersonalInfos select new { employee.EmployeeId, employee.Name };
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}