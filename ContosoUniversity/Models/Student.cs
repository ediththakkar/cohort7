using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Models
{
    public class Student
    {
        public int ID { get; set; }
        [Required]
        [Display(Name ="LastName")]
        [StringLength(50)]

        public string LastName { get; set;  }
        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        [Column("FirstName")]

        public string FirstMidName { get; set; }
        [Required]
        [Display(Name = "Enrollment Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}", ApplyFormatInEditMode =true)]

        public DateTime EnrollmentDate { get; set; }

        public string FullName { get { return LastName + ", " + FirstMidName; }}


        //navigation property
        public ICollection<Models.Enrollment> Enrollments { get; set; }
    }
}
