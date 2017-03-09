using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolApp.Models
{
    public class Student
    {
        // Id ( primary key )
        public int Id { get; set; }

        // First Name
        [Required]
        [Display(Name = "First Name")]
        [StringLength(25, ErrorMessage = "First name cannot be longer than 25 characters.")]
        public string FirstName { get; set; }

        // Last Name
        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string LastName { get; set; }

        // Enrollment Date
        [Display(Name = "Enrollment Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }

        // FullName
        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return FirstName + ", " + LastName;
            }
        }

        // Enrollments ( navigation property )
        public virtual ICollection<Enrollment> Enrollments { get; set; }  // Virtual <=> lazy loading(EF)
    }
}