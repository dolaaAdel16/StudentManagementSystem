using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace StudentManagementSystem.Entities
{
    public class Student
    {
        // StudentId(PK) · FullName · Email(unique index) · DateOfBirth · EnrollmentDate · nav: Enrollments

        public int StudentId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    }
}
