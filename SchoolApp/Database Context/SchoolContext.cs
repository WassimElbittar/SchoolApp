using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using SchoolApp.Models;

namespace SchoolApp.Database_Context
{
    public class SchoolContext  :  DbContext
    {
        public SchoolContext():base("SchoolContext")
        {
       
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<OfficeAssignment> OfficeAssignments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();      //  prevents table names from being pluralized...

        //    modelBuilder.Entity<Course>()
        //   .HasMany(t => t.Teachers).WithMany(c => c.Courses)
        //   .Map(i => i.MapLeftKey("CourseId")
        //   .MapRightKey("TeacherId")
        //   .ToTable("CourseTeacher"));
        //    modelBuilder.Entity<Department>().MapToStoredProcedures();
        }
    }
}