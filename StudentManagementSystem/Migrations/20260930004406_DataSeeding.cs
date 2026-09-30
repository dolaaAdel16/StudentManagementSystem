using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Credits", "Description", "Title" },
                values: new object[,]
                {
                    { 101, 3, "Fundamentals of C# programming and OOP.", "C# Fundamentals" },
                    { 102, 3, "Relational databases and SQL Server.", "SQL Server" },
                    { 103, 4, "Entity Framework Core and database operations.", "EF Core" },
                    { 104, 3, "Building backend applications using ASP.NET Core.", "ASP.NET Core" }
                });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "InstructorId", "FName", "LName" },
                values: new object[,]
                {
                    { 1, "Mohamed", "Hassan" },
                    { 2, "Ahmed", "Ali" },
                    { 3, "Sara", "Mohamed" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentId", "Email", "EnrollmentDate", "FName", "IsDeleted", "LName" },
                values: new object[,]
                {
                    { 1, "ahmed.adel@test.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ahmed", false, "Adel" },
                    { 2, "ahmed.hassan@test.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ahmed", false, "Hassan" },
                    { 3, "omar.mohamed@test.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Omar", false, "Mohamed" },
                    { 4, "sara.ali@test.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sara", false, "Ali" },
                    { 5, "youssef.samir@test.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Youssef", false, "Samir" },
                    { 6, "deleted.student@test.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Deleted", true, "Student" }
                });

            migrationBuilder.InsertData(
                table: "CourseInstructors",
                columns: new[] { "CourseId", "InstructorId", "Role" },
                values: new object[,]
                {
                    { 101, 1, "Main Instructor" },
                    { 102, 2, "Main Instructor" },
                    { 103, 1, "Main Instructor" },
                    { 104, 1, "Lead Instructor" },
                    { 104, 3, "Assistant Instructor" }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "CourseId", "StudentId", "EnrollmentDate", "Grade" },
                values: new object[,]
                {
                    { 101, 1, new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 85 },
                    { 102, 1, new DateTime(2026, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 90 },
                    { 103, 1, new DateTime(2026, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 95 },
                    { 101, 2, new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 75 },
                    { 102, 2, new DateTime(2026, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { 101, 3, new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 95 },
                    { 103, 3, new DateTime(2026, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 88 },
                    { 102, 4, new DateTime(2026, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 70 },
                    { 103, 4, new DateTime(2026, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 92 },
                    { 101, 5, new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 65 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CourseInstructors",
                keyColumns: new[] { "CourseId", "InstructorId" },
                keyValues: new object[] { 101, 1 });

            migrationBuilder.DeleteData(
                table: "CourseInstructors",
                keyColumns: new[] { "CourseId", "InstructorId" },
                keyValues: new object[] { 102, 2 });

            migrationBuilder.DeleteData(
                table: "CourseInstructors",
                keyColumns: new[] { "CourseId", "InstructorId" },
                keyValues: new object[] { 103, 1 });

            migrationBuilder.DeleteData(
                table: "CourseInstructors",
                keyColumns: new[] { "CourseId", "InstructorId" },
                keyValues: new object[] { 104, 1 });

            migrationBuilder.DeleteData(
                table: "CourseInstructors",
                keyColumns: new[] { "CourseId", "InstructorId" },
                keyValues: new object[] { 104, 3 });

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumns: new[] { "CourseId", "StudentId" },
                keyValues: new object[] { 101, 1 });

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumns: new[] { "CourseId", "StudentId" },
                keyValues: new object[] { 102, 1 });

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumns: new[] { "CourseId", "StudentId" },
                keyValues: new object[] { 103, 1 });

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumns: new[] { "CourseId", "StudentId" },
                keyValues: new object[] { 101, 2 });

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumns: new[] { "CourseId", "StudentId" },
                keyValues: new object[] { 102, 2 });

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumns: new[] { "CourseId", "StudentId" },
                keyValues: new object[] { 101, 3 });

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumns: new[] { "CourseId", "StudentId" },
                keyValues: new object[] { 103, 3 });

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumns: new[] { "CourseId", "StudentId" },
                keyValues: new object[] { 102, 4 });

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumns: new[] { "CourseId", "StudentId" },
                keyValues: new object[] { 103, 4 });

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumns: new[] { "CourseId", "StudentId" },
                keyValues: new object[] { 101, 5 });

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "InstructorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "InstructorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "InstructorId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "StudentId",
                keyValue: 5);
        }
    }
}
