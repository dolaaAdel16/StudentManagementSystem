using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using StudentManagementSystem.Data;
using StudentManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Services
{
    public class StudentService
    {
        private readonly AppDbContext _context;

        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddStudentAsync(Student student)
        {
            await _context.Students.AddAsync(student);

            await _context.SaveChangesAsync();
        }

        public async Task<Student?> GetStudentbyIdAsync(int id)
        {
            return await _context.Students
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.StudentId == id);
        }

        public async Task UpdateStudentAsync(Student student)
        {
            var existingstudent = await _context.Students.
                FindAsync(student.StudentId);

            if (existingstudent == null)
                return; 

            existingstudent.FName = student.FName;  
            existingstudent.LName = student.LName;  
            existingstudent.Email = student.Email;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(Student student)
        {
            var existingstudent = await _context.Students.
                FindAsync(student.StudentId);

            if (existingstudent == null)
                return;

            existingstudent.IsDeleted = true;

            await _context.SaveChangesAsync();

        }

        public async Task<List<Student>> SearchStudentByName(string name)
        {
             var result = await _context.Students
                .AsNoTracking()
                .Where(n => 
                EF.Functions.Like(n.FName, $"%{name}%") ||
                EF.Functions.Like(n.FName, $"%{name}%")).ToListAsync();
            
            return result;
        }

        public async Task<Student?> GetStudentWithAllEnrollmentsAsync(int id)
        {
            var result = await _context.Students
                .AsNoTracking()
                .Include(s => s.Enrollments)
                .Where(s => s.StudentId == id)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<Student>> GetDeletedStudentsAsync()
        {
            var result = await _context.Students
               .AsNoTracking()
               .IgnoreQueryFilters()
               .Where(s => s.IsDeleted)
               .ToListAsync();

            return result;
        }
    }
}
