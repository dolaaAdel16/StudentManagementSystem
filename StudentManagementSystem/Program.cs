using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Entities;
using StudentManagementSystem.Services;

namespace StudentManagementSystem
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(
                "Server=.;Database=StudentManagementDb;Trusted_Connection=True;TrustServerCertificate=True;"
            )
            .Options;


            using var context = new AppDbContext();

            // Make sure database is reachable
            await context.Database.CanConnectAsync();

            Console.WriteLine("========================================");
            Console.WriteLine(" STUDENT MANAGEMENT SYSTEM - TESTING");
            Console.WriteLine("========================================");
            Console.WriteLine();


             
            // Create Services
             

            var studentService = new StudentService(context);
            var courseService = new CourseService(context);
            var enrollmentService = new EnrollmentService(context);


             
            // STUDENT SERVICE TESTS
             

            Console.WriteLine("========== STUDENT SERVICE ==========");
            Console.WriteLine();


             
            // 1. Get Student By ID
             

            var student = await studentService.GetStudentbyIdAsync(1);

            if (student != null &&
                student.StudentId == 1 &&
                student.FName == "Ahmed")
            {
                Pass("Get Student By ID");
            }
            else
            {
                Fail("Get Student By ID");
            }


             
            // 2. Get Non-Existing Student
             

            var missingStudent =
                await studentService.GetStudentbyIdAsync(9999);

            if (missingStudent == null)
            {
                Pass("Get Non-Existing Student");
            }
            else
            {
                Fail("Get Non-Existing Student");
            }


             
            // 3. Search Student By Name
             

            var searchResult =
                await studentService.SearchStudentByName("Ahmed");

            if (searchResult.Count == 2)
            {
                Pass("Search Students By Name");
            }
            else
            {
                Fail("Search Students By Name");
            }


             
            // 4. Get Student With Enrollments
             

            var studentWithEnrollments =
                await studentService.GetStudentWithAllEnrollmentsAsync(1);

            if (studentWithEnrollments != null &&
                studentWithEnrollments.Enrollments.Count == 3)
            {
                Pass("Get Student With All Enrollments");
            }
            else
            {
                Fail("Get Student With All Enrollments");
            }


             
            // 5. Add Student
             

            var newStudent = new Student
            {
                FName = "Test",
                LName = "Student",
                Email = "test.student@test.com",
                IsDeleted = false
            };

            await studentService.AddStudentAsync(newStudent);

            if (newStudent.StudentId > 0)
            {
                Pass("Add Student");
            }
            else
            {
                Fail("Add Student");
            }


             
            // 6. Update Student
             

            newStudent.FName = "Updated";
            newStudent.Email = "updated.student@test.com";

            await studentService.UpdateStudentAsync(newStudent);

            var updatedStudent =
                await studentService.GetStudentbyIdAsync(newStudent.StudentId);

            if (updatedStudent != null &&
                updatedStudent.FName == "Updated" &&
                updatedStudent.Email == "updated.student@test.com")
            {
                Pass("Update Student");
            }
            else
            {
                Fail("Update Student");
            }


             
            // 7. Delete Student - Hard/Normal CRUD
             
            // NOTE:
            // We will NOT delete the seeded students.
            // This is a separate test student.

            await studentService.DeleteStudentAsync(newStudent);

            var deletedNormalStudent =
                await studentService.GetStudentbyIdAsync(newStudent.StudentId);

            if (deletedNormalStudent == null)
            {
                Pass("Delete Student / Soft Delete");
            }
            else
            {
                Fail("Delete Student / Soft Delete");
            }


             
            // COURSE SERVICE TESTS
             

            Console.WriteLine();
            Console.WriteLine("========== COURSE SERVICE ==========");
            Console.WriteLine();


             
            // 8. Get Course By ID
             

            var course =
                await courseService.GetCoursebyIdAsync(101);

            if (course != null &&
                course.Id == 101 &&
                course.Title == "C# Fundamentals")
            {
                Pass("Get Course By ID");
            }
            else
            {
                Fail("Get Course By ID");
            }


             
            // 9. Get Non-Existing Course
             

            var missingCourse =
                await courseService.GetCoursebyIdAsync(9999);

            if (missingCourse == null)
            {
                Pass("Get Non-Existing Course");
            }
            else
            {
                Fail("Get Non-Existing Course");
            }


             
            // 10. Add Course
             

            var newCourse = new Course
            {
                Title = "Testing Course",
                Credits = 3,
                Description = "Course created for testing."
            };

            await courseService.AddCourseAsync(newCourse);

            if (newCourse.Id > 0)
            {
                Pass("Add Course");
            }
            else
            {
                Fail("Add Course");
            }


             
            // 11. Update Course
             

            newCourse.Title = "Updated Testing Course";
            newCourse.Credits = 4;

            await courseService.UpdateCourseAsync(newCourse);

            var updatedCourse =
                await courseService.GetCoursebyIdAsync(newCourse.Id);

            if (updatedCourse != null &&
                updatedCourse.Title == "Updated Testing Course" &&
                updatedCourse.Credits == 4)
            {
                Pass("Update Course");
            }
            else
            {
                Fail("Update Course");
            }


             
            // 12. Delete Course
             

            await courseService.DeleteCourseAsync(newCourse);

            var deletedCourse =
                await courseService.GetCoursebyIdAsync(newCourse.Id);

            if (deletedCourse == null)
            {
                Pass("Delete Course");
            }
            else
            {
                Fail("Delete Course");
            }


             
            // ENROLLMENT SERVICE TESTS
             

            Console.WriteLine();
            Console.WriteLine("========== ENROLLMENT SERVICE ==========");
            Console.WriteLine();


             
            // 13. Get Enrollment
             

            var enrollment =
                await enrollmentService
                    .GetEnrollmentByStudentAndCourseAsync(1, 101);

            if (enrollment != null &&
                enrollment.StudentId == 1 &&
                enrollment.CourseId == 101 &&
                enrollment.Grade == 85)
            {
                Pass("Get Enrollment By Student And Course");
            }
            else
            {
                Fail("Get Enrollment By Student And Course");
            }


             
            // 14. Get Students In Course
             

            var studentsInCourse =
                await enrollmentService
                    .GetStudentsInCourseEnrollmentsAsync(101);

            if (studentsInCourse.Count == 4)
            {
                Pass("Get Students In Course");
            }
            else
            {
                Fail("Get Students In Course");
            }


             
            // 15. Get Courses And Grades For Student
             

            var coursesAndGrades =
                await enrollmentService
                    .GetCoursesAndGradesByStudentIdAsync(1);

            if (coursesAndGrades.Count == 3)
            {
                Pass("Get Courses And Grades By Student");
            }
            else
            {
                Fail("Get Courses And Grades By Student");
            }


             
            // 16. Calculate Average Grade
             

            var averageGrade =
                await enrollmentService
                    .CalculateAverageGradeAsync(101);

            if (averageGrade.HasValue &&
                Math.Abs(averageGrade.Value - 80.0) < 0.001)
            {
                Pass("Calculate Average Grade");
            }
            else
            {
                Fail("Calculate Average Grade");
            }


             
            // 17. Average With NULL Grade
             

            var averageWithNull =
                await enrollmentService
                    .CalculateAverageGradeAsync(102);

            if (averageWithNull.HasValue &&
                Math.Abs(averageWithNull.Value - 80.0) < 0.001)
            {
                Pass("Average Ignores NULL Grades");
            }
            else
            {
                Fail("Average Ignores NULL Grades");
            }


             
            // 18. Course With No Enrollments
             

            var emptyCourseAverage =
                await enrollmentService
                    .CalculateAverageGradeAsync(104);

            if (emptyCourseAverage == null)
            {
                Pass("Average For Course With No Grades");
            }
            else
            {
                Fail("Average For Course With No Grades");
            }


             
            // 19. Add Enrollment
             

            var newEnrollment = new Enrollment
            {
                StudentId = 5,
                CourseId = 104,
                Grade = 88,
                EnrollmentDate = new DateTime(2026, 2, 1)
            };

            await enrollmentService.AddEnrollmentAsync(newEnrollment);

            var addedEnrollment =
                await enrollmentService
                    .GetEnrollmentByStudentAndCourseAsync(5, 104);

            if (addedEnrollment != null &&
                addedEnrollment.Grade == 88)
            {
                Pass("Add Enrollment");
            }
            else
            {
                Fail("Add Enrollment");
            }


             
            // 20. Update Enrollment
             

            newEnrollment.Grade = 95;
            newEnrollment.EnrollmentDate =
                new DateTime(2026, 2, 2);

            await enrollmentService
                .UpdateEnrollmentAsync(newEnrollment);

            var updatedEnrollment =
                await enrollmentService
                    .GetEnrollmentByStudentAndCourseAsync(5, 104);

            if (updatedEnrollment != null &&
                updatedEnrollment.Grade == 95)
            {
                Pass("Update Enrollment");
            }
            else
            {
                Fail("Update Enrollment");
            }


             
            // 21. Delete Enrollment
             

            await enrollmentService
                .DeleteEnrollmentAsync(newEnrollment);

            var deletedEnrollment =
                await enrollmentService
                    .GetEnrollmentByStudentAndCourseAsync(5, 104);

            if (deletedEnrollment == null)
            {
                Pass("Delete Enrollment");
            }
            else
            {
                Fail("Delete Enrollment");
            }


             
            // SOFT DELETE TESTS
             

            Console.WriteLine();
            Console.WriteLine("========== SOFT DELETE ==========");
            Console.WriteLine();


             
            // 22. Get Deleted Students
             
            // Student 6 was seeded with IsDeleted = true.

            var deletedStudents =
                await studentService.GetDeletedStudentsAsync();

            var seededDeletedStudent =
                deletedStudents.FirstOrDefault(
                    s => s.StudentId == 6);

            if (seededDeletedStudent != null)
            {
                Pass("Get Deleted Students");
            }
            else
            {
                Fail("Get Deleted Students");
            }


             
            // 23. Soft Delete A Student
             

            var studentToSoftDelete =
                await studentService.GetStudentbyIdAsync(2);

            if (studentToSoftDelete != null)
            {
                await studentService
                    .DeleteStudentAsync(studentToSoftDelete);

                var normalQuery =
                    await studentService.GetStudentbyIdAsync(2);

                if (normalQuery == null)
                {
                    Pass("Global Query Filter Hides Deleted Student");
                }
                else
                {
                    Fail("Global Query Filter Hides Deleted Student");
                }


                var deletedAfterSoftDelete =
                    await studentService.GetDeletedStudentsAsync();

                var foundDeleted =
                    deletedAfterSoftDelete.FirstOrDefault(
                        s => s.StudentId == 2);

                if (foundDeleted != null &&
                    foundDeleted.IsDeleted)
                {
                    Pass("IgnoreQueryFilters Retrieves Deleted Student");
                }
                else
                {
                    Fail("IgnoreQueryFilters Retrieves Deleted Student");
                }
            }
            else
            {
                Fail("Soft Delete Setup");
            }


             
            // FINISHED
             
            Console.WriteLine(" TESTING FINISHED");
            Console.WriteLine("========================================");
        }


         
        // TEST RESULT HELPERS
         

        static void Pass(string testName)
        {
            Console.WriteLine($"[PASS] {testName}");
        }

        static void Fail(string testName)
        {
            Console.WriteLine($"[FAIL] {testName}");
        }
    }

}
    

