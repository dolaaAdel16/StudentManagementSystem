using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;
using StudentManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Services
{
    public class EnrollmentService
    {
        private readonly AppDbContext _context;
        
        public EnrollmentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddEnrollmentAsync(Enrollment enrollment)
        {
            await _context.Enrollments.AddAsync(enrollment);

            await _context.SaveChangesAsync();
        }

        public async Task<Enrollment?> GetEnrollmentByStudentAndCourseAsync(int studentId, int courseId)
        {
            return await _context.Enrollments
                .AsNoTracking()
                .FirstOrDefaultAsync(x =>
                x.CourseId == courseId &&
                x.StudentId == studentId);
        }

        public async Task UpdateEnrollmentAsync(Enrollment enrollment)
        {
            var existingenrollment =  await _context.Enrollments.FindAsync(enrollment.StudentId, enrollment.CourseId);

            if (existingenrollment == null)
                return;

            existingenrollment.Grade = enrollment.Grade;
            existingenrollment.EnrollmentDate = enrollment.EnrollmentDate;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteEnrollmentAsync(Enrollment enrollment)
        {
            var existingenrollment = await _context.Enrollments.FindAsync(enrollment.StudentId, enrollment.CourseId);

            if (existingenrollment == null)
                return;

            _context.Enrollments.Remove(existingenrollment);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Student>> GetStudentsInCourseEnrollmentsAsync(int courseId)
        {
            var result = await _context.Enrollments
                .AsNoTracking()
                .Where(x => x.CourseId == courseId)
                .Select(x => x.Student)
                .ToListAsync();

            return result;

        }

        public async Task<List<Enrollment>> GetCoursesAndGradesByStudentIdAsync(int studentId)
        {
            var result = await _context.Enrollments
                .AsNoTracking()
                .Include(x => x.Course)
                .Where(x => x.StudentId == studentId)
                .ToListAsync();

            return result;
        }

        public async Task<double?> CalculateAverageGradeAsync(int courseId)
        {
            var result = await _context.Enrollments
                .AsNoTracking()
                .Where(x => x.CourseId == courseId)
                .AverageAsync(x => x.Grade);

            return result;
        }
    }
}
