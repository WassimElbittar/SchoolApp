﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolApp.Models
{
    public class Course
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]                   // primary key values are provided by the user rather than generated by the database..
        [Display(Name = "Number")]
        public int CourseID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        [Range(0, 5)]
        public int Credits { get; set; }

        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }   // A course can have any number of students enrolled in it...( navigation property)
        public virtual ICollection<Teacher> Teachers { get; set; }         // A course may be taught by multiple teachers ...
    }
}