using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolApp.Models
{
    public class OfficeAssignment
    {
        [Key]
        [ForeignKey("Teacher")]
        public int TeacherID { get; set; }  // its primary key is also its foreign key to the Teacher entity
        // There's a one-to-zero-or-one relationship between the Teacher and the OfficeAssignment entities. An office assignment only exists in relation to the Teacher it's assigned to..


        [StringLength(50)]
        [Display(Name = "Office Location")]
        public string Location { get; set; }


        public virtual Teacher Teacher { get; set; }
    }
}