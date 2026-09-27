using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Entities
{
    public class CourseInstructor
    {
        public int InstructorId { get; set; }   
        public int CourseId { get; set; }  
        public string Role { get; set; }
        public Instructor? Instructor { get; set; }
        public Course? Course { get; set; }
    }
}
