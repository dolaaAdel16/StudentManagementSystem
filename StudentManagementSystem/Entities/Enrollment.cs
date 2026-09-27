using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Entities
{
    public class Enrollment
    {
        // StudentId + CourseId (composite key or a separate Id with a unique index)
        // · EnrollmentDate · Grade (stays null until entered, int?) · nav: Student, Course

        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int? Grade { get; set; }
        public Student Student { get; set; }    
        public Course Course { get; set; }
    }
}
