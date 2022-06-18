using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace _1265125_MVC.Models
{
    public class Context : IdentityDbContext
    {
        public Context() : base("DbConnection")
        {

        }

        public DbSet<PersonalInfo> PersonalInfos { get; set; }
        public DbSet<EducationalInfo> EducationalInfos { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EmploymentInfo> EmploymentInfos { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<District> Districts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }
    }
}