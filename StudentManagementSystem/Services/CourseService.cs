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
    public class CourseService
    {
        private readonly AppDbContext _context;

        public CourseService (AppDbContext context)
        {
            _context = context;
        }
        public async Task AddCourseAsync(Course course)
        {
            await _context.Courses.AddAsync(course);

            await _context.SaveChangesAsync();
        }

        public async Task<Course?> GetCoursebyIdAsync(int id)
        {
            return await _context.Courses
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateCourseAsync(Course course)
        {
            var existingcourse = await _context.Courses.FindAsync(course.Id);

            if (existingcourse == null)
                return;

            existingcourse.Title = course.Title;
            existingcourse.Credits = course.Credits;
            existingcourse.Description = course.Description;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCourseAsync(Course course)
        {
            var existingcourse = await _context.Courses.FindAsync(course.Id);

            if (existingcourse == null)
                return;

             _context.Courses.Remove(existingcourse);

            await _context.SaveChangesAsync();

        }


    }
}
