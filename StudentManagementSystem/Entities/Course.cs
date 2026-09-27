using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Entities
{
    public class Course
    {
        // CourseId (PK) · Title · Credits · Description (with max length) · nav: Enrollments · (InstructorId optional)

        public int Id { get; set; } 
        public string Title { get; set; }
        public int Credits { get; set; }
        public string Description { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
        public ICollection<CourseInstructor> CourseInstructors { get; set; } = new List<CourseInstructor>();
    }
}
