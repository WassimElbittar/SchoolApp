using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolApp.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(25, ErrorMessage = "First name cannot be longer than 25 characters.")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Hire Date")]
        public DateTime HireDate { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get { return FirstName + ", " + LastName; }
        }

        public virtual ICollection<Course> Courses { get; set; }   // Teacher can teach any number of courses...
        public virtual OfficeAssignment OfficeAssignment { get; set; }  // Teacher can only have at most one office ...
    }
}