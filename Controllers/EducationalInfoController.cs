using _1265125_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _1265125_MVC.Controllers
{
    public class EducationalInfoController : Controller
    {
        Context db = new Context();
        public ActionResult Index()
        {
            return View();
        }

        //[Authorize(Roles = "Admin")]
        public ActionResult Insert()
        {
            ViewBag.Personal = db.PersonalInfos.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Insert(EducationalInfo educationalInfo)
        {
            if (ModelState.IsValid)
            {
                db.EducationalInfos.Add(educationalInfo);
                db.SaveChanges();
                return RedirectToAction("Insert", "EmploymentInfo");
            }
            else
            {
                ViewBag.Error = ViewBag.Error = ModelState.Values.SelectMany(m => m.Errors).Select(m => m.ErrorMessage);
                ViewBag.Personal = db.PersonalInfos.ToList();
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