using _1265125_MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _1265125_MVC.ViewModel
{
    public class EditViewModel
    {
        Context db = null;

        public EditViewModel()
        {
            db = new Context();
        }
        public int EmployeeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime DoB { get; set; }
        public string Gender { get; set; }
        [Required]
        public int Phone { get; set; }
        [Required]
        [RegularExpression("^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+.)+[a-z]{2,5}$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        public int DivisionId { get; set; }
        [Required]
        public int DistrictId { get; set; }
        public string Image { get; set; }
        public string HighestDegree { get; set; }
        public string Subject { get; set; }
        [Required]
        public int PassingYear { get; set; }
        [Required]
        [Range(2, 4, ErrorMessage = "CGPA can't be more than 4")]
        public int CGPA { get; set; }
        public string Training { get; set; }
        public string Duration { get; set; }
        public int DepartmentId { get; set; }
        public string DepName { get; set; }
        public string Designation { get; set; }
        [DataType(DataType.Currency)]
        public double Salary { get; set; }
        public string ReferenceName { get; set; }
        public int ReferencePhone { get; set; }
        public string DivName { get; set; }
        public string DistrictName { get; set; }

        public EditViewModel EditView(int Id)
        {
                       return new EditViewModel();
        }
    }


}