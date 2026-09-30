using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StudentManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagementSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<CourseInstructor> CourseInstructors { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=.;Database=StudentManagementDb;Trusted_Connection=True;TrustServerCertificate=True;"
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.Entity<Student>().HasQueryFilter(s => !s.IsDeleted);

            modelBuilder.Entity<Student>().HasData(
        new Student
        {
            StudentId = 1,
            FName = "Ahmed",
            LName = "Adel",
            Email = "ahmed.adel@test.com",
            IsDeleted = false
        },

        new Student
        {
            StudentId = 2,
            FName = "Ahmed",
            LName = "Hassan",
            Email = "ahmed.hassan@test.com",
            IsDeleted = false
        },

        new Student
        {
            StudentId = 3,
            FName = "Omar",
            LName = "Mohamed",
            Email = "omar.mohamed@test.com",
            IsDeleted = false
        },

        new Student
        {
            StudentId = 4,
            FName = "Sara",
            LName = "Ali",
            Email = "sara.ali@test.com",
            IsDeleted = false
        },

        new Student
        {
            StudentId = 5,
            FName = "Youssef",
            LName = "Samir",
            Email = "youssef.samir@test.com",
            IsDeleted = false
        },

        // Soft-deleted student
        new Student
        {
            StudentId = 6,
            FName = "Deleted",
            LName = "Student",
            Email = "deleted.student@test.com",
            IsDeleted = true
        });


            // Courses

            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = 101,
                    Title = "C# Fundamentals",
                    Credits = 3,
                    Description = "Fundamentals of C# programming and OOP."
                },

                new Course
                {
                    Id = 102,
                    Title = "SQL Server",
                    Credits = 3,
                    Description = "Relational databases and SQL Server."
                },

                new Course
                {
                    Id = 103,
                    Title = "EF Core",
                    Credits = 4,
                    Description = "Entity Framework Core and database operations."
                },

                new Course
                {
                    Id = 104,
                    Title = "ASP.NET Core",
                    Credits = 3,
                    Description = "Building backend applications using ASP.NET Core."
                });

            // Instructors

            modelBuilder.Entity<Instructor>().HasData(
                new Instructor
                {
                    InstructorId = 1,
                    FName = "Mohamed",
                    LName = "Hassan"
                },
                new Instructor
                {
                    InstructorId = 2,
                    FName = "Ahmed",
                    LName = "Ali"
                },
                new Instructor
                {
                    InstructorId = 3,
                    FName = "Sara",
                    LName = "Mohamed"
                });

            // Course - Instructor

            modelBuilder.Entity<CourseInstructor>().HasData(
                new CourseInstructor
                {
                    InstructorId = 1,
                    CourseId = 101,
                    Role = "Main Instructor"
                },
                new CourseInstructor
                {
                    InstructorId = 2,
                    CourseId = 102,
                    Role = "Main Instructor"
                },
                new CourseInstructor
                {
                    InstructorId = 1,
                    CourseId = 103,
                    Role = "Main Instructor"
                },
                new CourseInstructor
                {
                    InstructorId = 1,
                    CourseId = 104,
                    Role = "Lead Instructor"
                },
                new CourseInstructor
                {
                    InstructorId = 3,
                    CourseId = 104,
                    Role = "Assistant Instructor"
                }
            );


            // Enrollments

            modelBuilder.Entity<Enrollment>().HasData(
                // Ahmed Adel
                new Enrollment
                {
                    StudentId = 1,
                    CourseId = 101,
                    Grade = 85,
                    EnrollmentDate = new DateTime(2026, 1, 10)
                },

                new Enrollment
                {
                    StudentId = 1,
                    CourseId = 102,
                    Grade = 90,
                    EnrollmentDate = new DateTime(2026, 1, 11)
                },

                new Enrollment
                {
                    StudentId = 1,
                    CourseId = 103,
                    Grade = 95,
                    EnrollmentDate = new DateTime(2026, 1, 12)
                },

                // Ahmed Hassan
                new Enrollment
                {
                    StudentId = 2,
                    CourseId = 101,
                    Grade = 75,
                    EnrollmentDate = new DateTime(2026, 1, 10)
                },

                new Enrollment
                {
                    StudentId = 2,
                    CourseId = 102,
                    Grade = null,
                    EnrollmentDate = new DateTime(2026, 1, 11)
                },

                // Omar
                new Enrollment
                {
                    StudentId = 3,
                    CourseId = 101,
                    Grade = 95,
                    EnrollmentDate = new DateTime(2026, 1, 10)
                },

                new Enrollment
                {
                    StudentId = 3,
                    CourseId = 103,
                    Grade = 88,
                    EnrollmentDate = new DateTime(2026, 1, 12)
                },

                // Sara
                new Enrollment
                {
                    StudentId = 4,
                    CourseId = 102,
                    Grade = 70,
                    EnrollmentDate = new DateTime(2026, 1, 11)
                },

                new Enrollment
                {
                    StudentId = 4,
                    CourseId = 103,
                    Grade = 92,
                    EnrollmentDate = new DateTime(2026, 1, 12)
                },

                // Youssef
                new Enrollment
                {
                    StudentId = 5,
                    CourseId = 101,
                    Grade = 65,
                    EnrollmentDate = new DateTime(2026, 1, 10)
                });

        }

    }
    
}

