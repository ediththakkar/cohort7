using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ContosoUniversity.Models
{
    public class Enrollment
    {
        public int EnrollmentID { get; set; }

        public int CourseID { get; set; }

        public int StudentID { get; set; }
        [DisplayFormat(NullDisplayText ="No Grade")]

        public Grade? Grade { get; set; }


        //navigational properties
        public Models.Course Course { get; set; }

        public Student Student { get; set; }

    }

    public enum Grade
    {
        A,B,C,D,F
    }
}