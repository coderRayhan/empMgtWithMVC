using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace _1265125_MVC.Models
{
    public class PersonalInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime DoB { get; set; }
        public string Gender { get; set; }
        [Required]
        //[RegularExpression(@"^[0][1-9]\d{9}$", ErrorMessage = "Invalid Phone Number")]
        public int Phone { get; set; }
        [Required]
        [RegularExpression("^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+.)+[a-z]{2,5}$", ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [ForeignKey("Division")]
        public int DivisionId { get; set; }
        [Required]
        [ForeignKey("District")]
        public int DistrictId { get; set; }
        public string Image { get; set; }

        public virtual ICollection<EducationalInfo> EducationalInfo { get; set; }
        public virtual ICollection<EmploymentInfo> EmploymentInfo { get; set; }
        public virtual Division Division { get; set; }
        public virtual District District { get; set; }
    }

    public class EducationalInfo
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("PersonalInfo")]
        public int EmployeeId { get; set; }
        public string HighestDegree { get; set; }
        public string Subject { get; set; }
        [Required]
        public int PassingYear { get; set; }
        [Required]
        [Range(2, 4, ErrorMessage = "CGPA can't be more than 4")]
        public int CGPA { get; set; }
        public string Training { get; set; }
        public string Duration { get; set; }
        public virtual PersonalInfo PersonalInfo { get; set; }
    }

    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public string DepName { get; set; }
        public virtual ICollection<EmploymentInfo> EmploymentInfos { get; set; }
    }

    public class EmploymentInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("PersonalInfo")]
        public int EmployeeId { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public string Designation { get; set; }
        [DataType(DataType.Currency)]
        public double Salary { get; set; }
        public string ReferenceName { get; set; }
        public int ReferencePhone { get; set; }

        public virtual PersonalInfo PersonalInfo { get; set; }
        public virtual Department Department { get; set; }
    }

    public class Division
    {
        [Key]
        public int DivisionId { get; set; }
        public string DivName { get; set; }
        public virtual ICollection<District> Districts { get; set; }
        public virtual ICollection<PersonalInfo> PersonalInfos { get; set; }
    }

    public class District
    {
        [Key]
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        [ForeignKey("Division")]
        public int DivisionId { get; set; }
        public virtual Division Division { get; set; }
        public virtual ICollection<PersonalInfo> PersonalInfos { get; set; }
    }
}