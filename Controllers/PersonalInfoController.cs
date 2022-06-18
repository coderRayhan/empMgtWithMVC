using _1265125_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _1265125_MVC.Controllers
{
    public class PersonalInfoController : Controller
    {
        Context db = new Context();
        public ActionResult Index(string searchString)
        {
            var emp = from e in db.PersonalInfos select e;
            if (!String.IsNullOrEmpty(searchString))
            {
                emp = db.PersonalInfos.Where(e => e.Name.Contains(searchString));
            }
            ViewBag.Emp = db.EmploymentInfos.ToList();
            return View(emp.ToList());
        }

        //[Authorize(Roles = "Admin")]
        public ActionResult Insert()
        {
            List<Division> DivisionList = db.Divisions.ToList();
            ViewBag.DivisionLists = new SelectList(DivisionList, "DivisionId", "DivName");
            ViewBag.Err = ModelState.Values.SelectMany(m => m.Errors).Select(m => m.ErrorMessage);
            return View();
        }

        [HttpPost]
        public ActionResult Insert(PersonalInfo personalInfo, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                string fileName = Image.FileName;
                string filePath = Server.MapPath("~/Images/") + fileName;
                Image.SaveAs(filePath);
                personalInfo.Image = "~/Images/" + fileName;
                db.PersonalInfos.Add(personalInfo);
                db.SaveChanges();
                return RedirectToAction("Insert", "EducationalInfo");
            }
            else
            {
                ViewBag.Err = ModelState.Values.SelectMany(m => m.Errors).Select(m => m.ErrorMessage);
                List<Division> DivisionList = db.Divisions.ToList();
                ViewBag.DivisionLists = new SelectList(DivisionList, "DivisionId", "DivName");
                return View();
            }

        }


        public JsonResult GetDistrictList(int DivisionId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<District> DistrictList = db.Districts.Where(m => m.DivisionId == DivisionId).ToList();
            return Json(DistrictList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int? EmployeeId)
        {
            var ptod = db.PersonalInfos.Find(EmployeeId);
            var edutod = db.EducationalInfos.Where(m => m.EmployeeId == EmployeeId).FirstOrDefault();
            var empltd = db.EmploymentInfos.Where(m => m.EmployeeId == EmployeeId).FirstOrDefault();
            db.PersonalInfos.Remove(ptod);
            db.EducationalInfos.Remove(edutod);
            db.EmploymentInfos.Remove(empltd);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}