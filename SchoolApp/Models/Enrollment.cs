using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SchoolApp.Models
{
    public enum Grade
    {
        A,B,C,D,F
    }
    public class Enrollment
    {
        public int EnrollmentID { get; set; }   // primary key

        // An enrollment record is for a single student ...
        public int StudentID { get; set; }     // foreign key( Student )
        public virtual Student Student { get; set; }  // ( navigation property )

        // An enrollment record is for a single course...
        public int CourseID { get; set; }     // foreign key( Course )
        public virtual Course Course { get; set; }   // ( navigation property )

        [DisplayFormat(NullDisplayText = "No grade")]
        public Grade? Grade { get; set; }     // nullable
    }
}