using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SchoolApp.Database_Context;
using SchoolApp.Models;
using SchoolApp.ViewModels;
using System.Data.Entity.Infrastructure;

namespace SchoolApp.Controllers
{
    public class TeacherController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Teacher
        public ActionResult Index(int? id, int? CourseID)
        {
            var viewModel = new TeacherIndexData();
            viewModel.Instructors = db.Teachers
                .Include(i => i.OfficeAssignment)
                .Include(i => i.Courses.Select(c => c.Department))
                .OrderBy(i => i.FirstName);

            if (id != null)
            {
                ViewBag.TeacherID = id.Value;
                viewModel.Courses = viewModel.Instructors.Where(
                    i => i.Id == id.Value).Single().Courses;
            }

            if (CourseID != null)
            {
                ViewBag.CourseID = CourseID.Value;
                // viewModel.Enrollments = viewModel.Courses.Where(
                //    x => x.CourseID == CourseID).Single().Enrollments;

                var selectedCourse = viewModel.Courses.Where(x => x.CourseID == CourseID).Single();
                db.Entry(selectedCourse).Collection(x => x.Enrollments).Load();
                foreach (Enrollment enrollment in selectedCourse.Enrollments)
                {
                    db.Entry(enrollment).Reference(x => x.Student).Load();
                }

                viewModel.Enrollments = selectedCourse.Enrollments;
            }

            return View(viewModel);
        }

        // GET: Teacher/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // GET: Teacher/Create
        public ActionResult Create()
        {
            var teacher = new Teacher();
            teacher.Courses = new List<Course>();  //  in order to be able to add courses to the Courses navigation property you have to initialize the property as an empty collection
            PopulateAssignedCourseData(teacher);
            return View();
        }
        

        // POST: Teacher/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,HireDate,OfficeAssignment")] Teacher teacher , string[] selectedCourses)
        {

            if (selectedCourses != null)
            {
                teacher.Courses = new List<Course>();
                foreach (var course in selectedCourses)
                {
                    var courseToAdd = db.Courses.Find(int.Parse(course));
                    teacher.Courses.Add(courseToAdd);
                }
            }
            if (ModelState.IsValid)
            {
                db.Teachers.Add(teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            PopulateAssignedCourseData(teacher);
            return View(teacher);
        }
       

        // GET: Teacher/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers
                .Include(i => i.OfficeAssignment)
                .Include(i => i.Courses)
                .Where(i => i.Id == id)
                .Single();
            PopulateAssignedCourseData(teacher);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.OfficeAssignments, "TeacherID", "Location", teacher.Id);
            return View(teacher);
        }

        // POST: Teacher/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedCourses)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var instructorToUpdate = db.Teachers
               .Include(i => i.OfficeAssignment)
               .Include(i => i.Courses)
               .Where(i => i.Id == id)
               .Single();

            if (TryUpdateModel(instructorToUpdate, "",
               new string[] { "FirstName", "LastName", "HireDate", "OfficeAssignment" }))
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(instructorToUpdate.OfficeAssignment.Location))
                    {
                        instructorToUpdate.OfficeAssignment = null;
                    }
                    UpdateInstructorCourses(selectedCourses, instructorToUpdate);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            PopulateAssignedCourseData(instructorToUpdate);
            return View(instructorToUpdate);
        }
        // GET: Teacher/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if (teacher == null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        // POST: Teacher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = db.Teachers
            .Include(i => i.OfficeAssignment)
            .Where(i => i.Id == id)
            .Single();
            db.Teachers.Remove(teacher);
            var department = db.Departments
              .Where(d => d.TeacherID == id)
              .SingleOrDefault();
            if (department != null)
            {
                department.TeacherID = null;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void PopulateAssignedCourseData(Teacher teacher)
        {
            var allCourses = db.Courses;
            var instructorCourses = new HashSet<int>(teacher.Courses.Select(c => c.CourseID));
            var viewModel = new List<AssignedCourseData>();
            foreach (var course in allCourses)
            {
                viewModel.Add(new AssignedCourseData
                {
                    CourseID = course.CourseID,
                    Title = course.Title,
                    Assigned = instructorCourses.Contains(course.CourseID)
                });
            }
            ViewBag.Courses = viewModel;
        }

        private void UpdateInstructorCourses(string[] selectedCourses, Teacher instructorToUpdate)
        {
            if (selectedCourses == null)
            {
                instructorToUpdate.Courses = new List<Course>();
                return;
            }

            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var instructorCourses = new HashSet<int>
                (instructorToUpdate.Courses.Select(c => c.CourseID));
            foreach (var course in db.Courses)
            {
                // If the check box for a course was selected but the course isn't in the Teacher.Courses navigation property, the course is added to the collection in the navigation property.
                if (selectedCoursesHS.Contains(course.CourseID.ToString()))   
                {
                    if (!instructorCourses.Contains(course.CourseID))
                    {
                        instructorToUpdate.Courses.Add(course);
                    }
                }
                else
                {
                    //If the check box for a course wasn't selected, but the course is in the Teacher.Courses navigation property, the course is removed from the navigation property.
                    if (instructorCourses.Contains(course.CourseID))
                    {
                        instructorToUpdate.Courses.Remove(course);
                    }
                }
            }
        }
    }
}
