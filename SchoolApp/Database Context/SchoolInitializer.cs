using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using SchoolApp.Models;

namespace SchoolApp.Database_Context
{
    public class SchoolInitializer : DropCreateDatabaseIfModelChanges<SchoolContext>
        {
            protected override void Seed(SchoolContext context)
            {
                var students = new List<Student>
                      {
                         new Student{FirstName="Wassim",LastName="Elbittar",EnrollmentDate=DateTime.Parse("2016-12-05")},
                         new Student{FirstName="Andreas",LastName="Ogenblad",EnrollmentDate=DateTime.Parse("2016-12-05")},
                         new Student{FirstName="Mylah",LastName="Rufus",EnrollmentDate=DateTime.Parse("2016-12-05")},
                         new Student{FirstName="Steven",LastName="Knudsen",EnrollmentDate=DateTime.Parse("2016-12-05")},
                         new Student{FirstName="Pier",LastName="Finnfors",EnrollmentDate=DateTime.Parse("2017-01-01")},
                         new Student{FirstName="Waseem",LastName="Totonji",EnrollmentDate=DateTime.Parse("2016-12-10")},
                         new Student{FirstName="Maher",LastName="Kouniehle",EnrollmentDate=DateTime.Parse("2016-12-10")},
                     };

                students.ForEach(s => context.Students.Add(s));
                context.SaveChanges();

             //***********************************************************************************************

                var courses = new List<Course>
                    {
                        new Course{Title="Programming C#",Credits=3,},
                        new Course{Title="Bootstrap",Credits=3,},
                        new Course{Title="HTML & CSS",Credits=3,},
                        new Course{Title="JavaScript",Credits=4,},
                        new Course{Title="SQL",Credits=4,},
                        new Course{Title="IT Support",Credits=3,},
                        new Course{Title="Windows Office",Credits=4,}
                   };
                courses.ForEach(s => context.Courses.Add(s));
                context.SaveChanges();

             //***********************************************************************************************

                var enrollments = new List<Enrollment>
                   {
                        new Enrollment{StudentID=1,CourseID=1,Grade=Grade.A},
                        new Enrollment{StudentID=1,CourseID=2,Grade=Grade.C},
                        new Enrollment{StudentID=1,CourseID=4,Grade=Grade.B},
                        new Enrollment{StudentID=2,CourseID=1,Grade=Grade.B},
                        new Enrollment{StudentID=2,CourseID=3,Grade=Grade.F},
                        new Enrollment{StudentID=2,CourseID=2,Grade=Grade.F},
                        new Enrollment{StudentID=3,CourseID=5},
                        new Enrollment{StudentID=4,CourseID=5},
                        new Enrollment{StudentID=4,CourseID=4,Grade=Grade.F},
                        new Enrollment{StudentID=5,CourseID=4,Grade=Grade.C},
                        new Enrollment{StudentID=6,CourseID=6},
                        new Enrollment{StudentID=7,CourseID=7,Grade=Grade.A},
                 };
                enrollments.ForEach(s => context.Enrollments.Add(s));
                context.SaveChanges();
            }
        }
    }

