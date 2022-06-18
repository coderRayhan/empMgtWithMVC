using _1265125_MVC.Models;
using _1265125_MVC.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace _1265125_MVC.Controllers
{
    public class HomeController : Controller
    {
        Context db = new Context();
        public ActionResult Index()
        {
            return View();
        }

        //[Authorize(Roles ="Admin")]

        public ActionResult Edit(int? EmployeeId)
        {
            TempData["pInfo"] = db.PersonalInfos.Find(EmployeeId);
            TempData["eduInfo"] = db.EducationalInfos.Where(m => m.EmployeeId == EmployeeId).FirstOrDefault();
            TempData["empInfo"] = db.EmploymentInfos.Where(m => m.EmployeeId == EmployeeId).FirstOrDefault();
            TempData["depInfo"] = db.Departments.ToList();
            List<Division> DivisionList = db.Divisions.ToList();
            ViewBag.DivisionLists = new SelectList(DivisionList, "DivisionId", "DivName");
            return View();
        }


        [HttpPost]
        public ActionResult Edit(PersonalInfo personalInfo, EducationalInfo educationalInfo, EmploymentInfo employmentInfo, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                string fileName = Image.FileName;
                string filePath = Server.MapPath("~/Images/") + fileName;
                Image.SaveAs(filePath);
                personalInfo.Image = "~/Images/" + fileName;
                var empPer = db.PersonalInfos.Where(w => w.EmployeeId == personalInfo.EmployeeId).FirstOrDefault();
                empPer.Name = personalInfo.Name;
                empPer.DoB = personalInfo.DoB;
                empPer.Gender = personalInfo.Gender;
                empPer.Phone = personalInfo.Phone;
                empPer.Email = personalInfo.Email;
                empPer.Address = personalInfo.Address;
                empPer.DivisionId = personalInfo.DivisionId;
                empPer.DistrictId = personalInfo.DistrictId;
                empPer.Image = personalInfo.Image;

                var empEdu = db.EducationalInfos.Where(w => w.EmployeeId == educationalInfo.EmployeeId).FirstOrDefault();
                empEdu.HighestDegree = educationalInfo.HighestDegree;
                empEdu.Subject = educationalInfo.Subject;
                empEdu.PassingYear = educationalInfo.PassingYear;
                empEdu.CGPA = educationalInfo.CGPA;
                empEdu.Training = educationalInfo.Training;
                empEdu.Duration = educationalInfo.Duration;

                var empInfo = db.EmploymentInfos.Where(w => w.EmployeeId == employmentInfo.EmployeeId).FirstOrDefault();
                empInfo = employmentInfo;
                empInfo.DepartmentId = employmentInfo.DepartmentId;
                empInfo.Designation = employmentInfo.Designation;
                empInfo.Salary = employmentInfo.Salary;
                empInfo.ReferenceName = employmentInfo.ReferenceName;
                empInfo.ReferencePhone = employmentInfo.ReferencePhone;

                db.SaveChanges();
                return RedirectToAction("Index", "PersonalInfo");
            }
            else
            {
                TempData["pInfo"] = db.PersonalInfos.Find(personalInfo.EmployeeId);
                TempData["eduInfo"] = db.EducationalInfos.Where(m => m.EmployeeId == educationalInfo.EmployeeId).FirstOrDefault();
                TempData["empInfo"] = db.EmploymentInfos.Where(m => m.EmployeeId == employmentInfo.EmployeeId).FirstOrDefault();
                TempData["depInfo"] = db.Departments.ToList();
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
        //[Authorize(Roles ="Admin")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult viewData(int EmployeeId)
        {
            TempData["personalInfo"] = db.PersonalInfos.Find(EmployeeId);
            TempData["educationalInfo"] = db.EducationalInfos.Where(m => m.EmployeeId == EmployeeId).FirstOrDefault();
            TempData["employmentInfo"] = db.EmploymentInfos.Where(m => m.EmployeeId == EmployeeId).FirstOrDefault();
            return View();
        }

        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LoginViewModel model)
        {
            bool finUser = db.Users.Any(m => m.UserName == model.UserName && m.Email == model.Email);
            if (finUser)
            {
                FormsAuthentication.SetAuthCookie(model.UserName, false);
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}