namespace SchoolApp.Migrations
{
    using SchoolApp.Models;
    using SchoolApp.Database_Context;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SchoolApp.Database_Context.SchoolContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SchoolContext context)
        {
            var students = new List<Student>
            {
                new Student { FirstName = "Wassim",   LastName = "Elbittar",
                    EnrollmentDate = DateTime.Parse("2016-12-05") },
                new Student { FirstName = "Andreas",   LastName = "Ogenblad",
                    EnrollmentDate = DateTime.Parse("2016-12-05") },
                new Student { FirstName = "Steven",   LastName = "Kundsen",
                    EnrollmentDate = DateTime.Parse("2016-12-05") },
                 new Student { FirstName = "Gheath",   LastName = "Sharaf",
                    EnrollmentDate = DateTime.Parse("2016-12-01") },
                 new Student { FirstName = "Maher",   LastName = "Kouniehle",
                    EnrollmentDate = DateTime.Parse("2016-12-01") },
                new Student { FirstName = "Waseem",   LastName = "Totonji",
                    EnrollmentDate = DateTime.Parse("2016-12-01") },
                 new Student { FirstName = "Hussien",   LastName = "Maxamed",
                    EnrollmentDate = DateTime.Parse("2016-12-10") },
                 new Student { FirstName = "Mylah",   LastName = "Rufus",
                    EnrollmentDate = DateTime.Parse("2016-12-10") },
                  new Student { FirstName = "Pierre",   LastName = "Finnfors",
                    EnrollmentDate = DateTime.Parse("2017-01-01") },
            };

            students.ForEach(s => context.Students.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var teachers = new List<Teacher>
                {
                    new Teacher { FirstName = "Mike",     LastName = "Ash",
                        HireDate = DateTime.Parse("2015-03-08") },
                   new Teacher { FirstName = "Ulf",     LastName = "Bengtsson",
                        HireDate = DateTime.Parse("2010-03-21") },
                   new Teacher { FirstName = "Marcus",     LastName = "Gudmundsen",
                        HireDate = DateTime.Parse("2005-01-02") },
                   new Teacher { FirstName = "Fredrik",     LastName = "Odin",
                        HireDate = DateTime.Parse("2016-04-11") },
                };
            teachers.ForEach(s => context.Teachers.AddOrUpdate(p => p.LastName, s));
            context.SaveChanges();

            var departments = new List<Department>
                {
                  new Department { Name = "Harry", Budget = 35000 , StartDate =DateTime.Parse("2016-12-05"), TeacherID = teachers.SingleOrDefault(i => i.FirstName == "Mike").Id },
                  new Department { Name = "Selma", Budget = 100000 , StartDate =DateTime.Parse("2016-12-10"), TeacherID = teachers.SingleOrDefault(i => i.FirstName == "Ulf").Id },
                  new Department { Name = "Moa", Budget = 45000 , StartDate =DateTime.Parse("2017-01-01"), TeacherID = teachers.SingleOrDefault(i => i.FirstName == "Marcus").Id },
                  new Department { Name = "Audgust", Budget = 10000 , StartDate =DateTime.Parse("2016-12-01"), TeacherID = teachers.SingleOrDefault(i => i.FirstName == "Fredrik").Id }
                };
            departments.ForEach(s => context.Departments.AddOrUpdate(p => p.Name, s));
            context.SaveChanges();

            var courses = new List<Course>
                {
                  new Course { CourseID = 1, Title = "Programming C#", Credits = 4 , DepartmentID = departments.SingleOrDefault(c => c.Name == "Harry").DepartmentID , Teachers = new List<Teacher>()},
                  new Course { CourseID = 2, Title = "Bootstrap", Credits = 3 , DepartmentID = departments.SingleOrDefault(c => c.Name == "Harry").DepartmentID , Teachers = new List<Teacher>()},
                  new Course { CourseID = 3, Title = "HTML & CSS", Credits = 3 , DepartmentID = departments.SingleOrDefault(c => c.Name == "Selma").DepartmentID , Teachers = new List<Teacher>()},
                  new Course { CourseID = 4, Title = "JavaScript", Credits = 5 , DepartmentID = departments.SingleOrDefault(c => c.Name == "Moa").DepartmentID , Teachers = new List<Teacher>()},
                  new Course { CourseID = 5, Title = "SQL", Credits = 5 , DepartmentID = departments.SingleOrDefault(c => c.Name == "Audgust").DepartmentID , Teachers = new List<Teacher>()},
                  new Course { CourseID = 6, Title = "IT Support", Credits = 4 , DepartmentID = departments.SingleOrDefault(c => c.Name == "Moa").DepartmentID , Teachers = new List<Teacher>()},
                  new Course { CourseID = 7, Title = "Windows Office", Credits = 3 , DepartmentID = departments.SingleOrDefault(c => c.Name == "Selma").DepartmentID , Teachers = new List<Teacher>()},

               };
            courses.ForEach(s => context.Courses.AddOrUpdate(p => p.Title, s));
            context.SaveChanges();

            var officeAssignments = new List<OfficeAssignment>
              {
                new OfficeAssignment { TeacherID = teachers.SingleOrDefault (i => i.FirstName == "Mike").Id, Location = "Jönköping" },
                new OfficeAssignment { TeacherID = teachers.SingleOrDefault (i => i.FirstName == "Ulf").Id, Location = "Foserum" },
                new OfficeAssignment { TeacherID = teachers.SingleOrDefault (i => i.FirstName == "Marcus").Id, Location = "Växjö" },
                new OfficeAssignment { TeacherID = teachers.SingleOrDefault (i => i.FirstName == "Fredrik").Id, Location = "Huskvarna" },
              };
            officeAssignments.ForEach(s => context.OfficeAssignments.AddOrUpdate(p => p.TeacherID, s));
            context.SaveChanges();

                  AddOrUpdateTeacher(context, "Programming C#", "Mike");
                  AddOrUpdateTeacher(context, "Bootstrap", "Mike");
                  AddOrUpdateTeacher(context, "HTML & CSS", "Ulf");
                  AddOrUpdateTeacher(context, "JavaScript", "Marcus");

                  AddOrUpdateTeacher(context, "SQL", "Mike");
                  AddOrUpdateTeacher(context, "SQL", "Ulf");
                  AddOrUpdateTeacher(context, "IT Support", "Marcus");
                  AddOrUpdateTeacher(context, "Programming C#", "Ulf");
                  AddOrUpdateTeacher(context, "Windows Office", "Marcus");

            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                new Enrollment { StudentID = students.SingleOrDefault(s => s.FirstName == "Wassim").Id, CourseID = courses.SingleOrDefault(c => c.Title == "Programming C#").CourseID, Grade =Grade.A },
                new Enrollment { StudentID = students.SingleOrDefault(s => s.FirstName == "Wassim").Id, CourseID = courses.SingleOrDefault(c => c.Title == "JavaScript").CourseID, Grade =Grade.B },
                new Enrollment { StudentID = students.SingleOrDefault(s => s.FirstName == "Wassim").Id, CourseID = courses.SingleOrDefault(c => c.Title == "SQL").CourseID, Grade =Grade.C },
                new Enrollment { StudentID = students.SingleOrDefault(s => s.FirstName == "Andreas").Id, CourseID = courses.SingleOrDefault(c => c.Title == "Bootstrap").CourseID, Grade =Grade.A },
                new Enrollment { StudentID = students.SingleOrDefault(s => s.FirstName == "Steven").Id, CourseID = courses.SingleOrDefault(c => c.Title == "HTML & CSS").CourseID, Grade =Grade.B },
                new Enrollment { StudentID = students.SingleOrDefault(s => s.FirstName == "Steven").Id, CourseID = courses.SingleOrDefault(c => c.Title == "Windows Office").CourseID, Grade =Grade.C },
                new Enrollment { StudentID = students.SingleOrDefault(s => s.FirstName == "Mylah").Id, CourseID = courses.SingleOrDefault(c => c.Title == "IT Support").CourseID, Grade =Grade.D },
                new Enrollment { StudentID = students.SingleOrDefault(s => s.FirstName == "Maher").Id, CourseID = courses.SingleOrDefault(c => c.Title == "Bootstrap").CourseID, Grade =Grade.D },
                new Enrollment { StudentID = students.SingleOrDefault(s => s.FirstName == "Waseem").Id, CourseID = courses.SingleOrDefault(c => c.Title == "IT Support").CourseID, Grade =Grade.F},
                new Enrollment { StudentID = students.SingleOrDefault(s => s.FirstName == "Pierre").Id, CourseID = courses.SingleOrDefault(c => c.Title == "Windows Office").CourseID, Grade =Grade.B },
           };

            foreach( Enrollment e in enrollments)
            {
                var enrollmentInDataBase = context.Enrollments.Where(s => s.Student.Id == e.StudentID && s.Course.CourseID == e.CourseID).FirstOrDefault();

                if (enrollmentInDataBase == null)
                {
                    context.Enrollments.Add(e);
                }
            }
            //    foreach (Enrollment e in enrollments)
            //    {
            //        var enrollmentInDataBase = context.Enrollments.Where(
            //            s =>
            //                 s.Student.Id == e.StudentID && s.Course.CourseID == e.CourseID).FirstOrDefault();
            //        if (enrollmentInDataBase == null)
            //        {
            //            context.Enrollments.Add(e);
            //        }
            //    }
            //    context.SaveChanges();
        }

        void AddOrUpdateTeacher(SchoolContext context, string courseTitle, string teacherName)

                {
                    var crs = context.Courses.FirstOrDefault(c => c.Title == courseTitle);
                    var inst = crs.Teachers.FirstOrDefault(i => i.FirstName == teacherName);
                    if (inst == null)
                        crs.Teachers.Add(context.Teachers.Single(i => i.FirstName == teacherName));
                }

    }  
}



