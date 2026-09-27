using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StudentManagementSystem.Entities
{
    public class Instructor
    {
        // InstructorId(PK) · FullName · one-to-many with Course(bonus: many-to-many if a course can have more than one instructor)

        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public ICollection<CourseInstructor> CourseInstructors { get; set; } = new List<CourseInstructor>();


    }
}
