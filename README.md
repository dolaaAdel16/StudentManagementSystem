# Student Management System

A backend-focused Student Management System built with **C#**, **.NET 8**, **Entity Framework Core**, and **SQL Server**.

The project was developed to practice and demonstrate real-world database-driven application development using Entity Framework Core, including entity relationships, CRUD operations, LINQ queries, tracking behavior, eager loading, soft deletion, global query filters, data seeding, migrations, asynchronous database operations, and manual integration testing.

---

## 📌 Project Overview

The system manages:

- Students
- Courses
- Enrollments
- Instructors
- Student grades
- Course-instructor assignments

The project focuses on building a simple and practical EF Core data layer without unnecessary abstractions or over-engineering.

The main goal was to understand how application code interacts with a relational database through Entity Framework Core and how to design, query, and manage related entities effectively.

---

## 🚀 Features

### Student Management

- Add a student
- Get student by ID
- Update student information
- Soft delete a student
- Search students by name
- Get a student with all enrollments
- Retrieve deleted students when explicitly required

### Course Management

- Add a course
- Get course by ID
- Update course information
- Delete a course

### Enrollment Management

- Add an enrollment
- Get enrollment by Student ID and Course ID
- Update enrollment
- Delete enrollment
- Get all students enrolled in a specific course
- Get all courses and grades for a specific student
- Calculate the average grade for a course

### Instructor Management

- Model instructors as separate entities
- Support many-to-many relationships between courses and instructors
- Store the instructor's role for each course assignment

### Data Management

- SQL Server integration
- Entity Framework Core migrations
- Static database seeding using `HasData()`
- Composite keys
- Foreign key relationships
- Navigation properties
- Global query filters
- Soft deletion

---

## 🛠️ Technologies Used

| Technology | Purpose |
|---|---|
| C# | Programming language |
| .NET 8 | Application framework |
| Entity Framework Core | ORM and database access |
| SQL Server | Relational database |
| LINQ | Database querying |
| Visual Studio | Development environment |
| Git & GitHub | Version control |

---

## 🏗️ Architecture

The project follows a simple service-based architecture:

Application  
↓  
Services  
↓  
AppDbContext  
↓  
Entity Framework Core  
↓  
SQL Server

The project intentionally avoids unnecessary architectural layers such as:

- Generic Repository
- Unit of Work
- Service Interfaces
- Controllers
- Additional abstraction layers

These were not required for the project scope, so the implementation remains simple and focused on understanding EF Core and database operations.

---

## 📂 Project Structure

StudentManagementSystem/

├── Data/  
│   └── AppDbContext.cs  
│  
├── Entities/  
│   ├── Student.cs  
│   ├── Course.cs  
│   ├── Enrollment.cs  
│   ├── Instructor.cs  
│   └── CourseInstructor.cs  
│  
├── Services/  
│   ├── StudentService.cs  
│   ├── CourseService.cs  
│   └── EnrollmentService.cs  
│  
├── Migrations/  
│  
├── Program.cs  
├── appsettings.json  
└── StudentManagementSystem.csproj

---

# 🗄️ Database Design

## Student

Represents a student in the system.

Main properties:

- StudentId
- FName
- LName
- Email
- IsDeleted

A student can have multiple enrollments.

Student  
↓  
Enrollment

---

## Course

Represents an academic course.

Main properties:

- Id
- Title
- Credits
- Description

A course can have:

- Multiple enrollments
- Multiple instructors

---

## Enrollment

Represents the relationship between a student and a course.

Main properties:

- StudentId
- CourseId
- Grade
- EnrollmentDate

The enrollment connects:

Student  
↓  
Enrollment  
↓  
Course

The combination of `StudentId` and `CourseId` identifies an enrollment and prevents the same student from being enrolled in the same course more than once.

---

## Instructor

Represents an instructor.

Main properties:

- InstructorId
- FName
- LName

---

## CourseInstructor

Represents the many-to-many relationship between courses and instructors.

Main properties:

- InstructorId
- CourseId
- Role

Relationship:

Instructor  
↓  
CourseInstructor  
↑  
Course

A course can have multiple instructors, and an instructor can teach multiple courses.

The `Role` property allows the relationship itself to store information such as:

- Main Instructor
- Lead Instructor
- Assistant Instructor

---

# 🔗 Entity Relationships

Student → Enrollment: One-to-Many

Course → Enrollment: One-to-Many

Student ↔ Course: Many-to-Many through Enrollment

Course ↔ Instructor: Many-to-Many through CourseInstructor

Overall relationship:

Student  
↓  
Enrollment  
↑  
Course  
↓  
CourseInstructor  
↑  
Instructor

---

# 🔄 CRUD Operations

## StudentService

The service implements:

- Add Student
- Get Student By ID
- Update Student
- Soft Delete Student
- Search Student By Name
- Get Student With All Enrollments
- Get Deleted Students

## CourseService

The service implements:

- Add Course
- Get Course By ID
- Update Course
- Delete Course

## EnrollmentService

The service implements:

- Add Enrollment
- Get Enrollment By Student And Course
- Update Enrollment
- Delete Enrollment
- Get Students In Course
- Get Courses And Grades By Student
- Calculate Average Grade

---

# 🔎 LINQ Queries

LINQ is used extensively for database querying through Entity Framework Core.

### Where()

Used to filter entities based on specific conditions.

Examples include:

- Filtering enrollments by StudentId
- Filtering enrollments by CourseId
- Filtering students by name
- Filtering deleted students

### Select()

Used for projection when only specific data is required.

For example:

Enrollment  
↓  
Student

### FirstOrDefaultAsync()

Used when a query should return one entity or `null`.

### FindAsync()

Used for retrieving entities using their primary key or composite key.

### ToListAsync()

Used when retrieving multiple records.

### AverageAsync()

Used to calculate the average grade for a course.

---

# ⚡ Async Database Operations

Database operations are implemented asynchronously to avoid blocking while waiting for database I/O operations.

The project uses:

- AddAsync()
- FindAsync()
- FirstOrDefaultAsync()
- ToListAsync()
- AverageAsync()
- SaveChangesAsync()

This keeps database access asynchronous throughout the service layer.

---

# 🎯 Tracking vs No Tracking

The project demonstrates EF Core Change Tracking and No-Tracking queries.

## Read Operations

Read-only queries use:

`AsNoTracking()`

This is appropriate when entities are only being read or displayed and do not need to be modified afterward.

Examples include:

- Get Student
- Search Students
- Get Course
- Get Enrollment
- Get Students In Course
- Get Courses And Grades

## Update and Delete Operations

Tracking is preserved when an entity needs to be modified.

The general flow is:

Find Entity  
↓  
Entity becomes tracked  
↓  
Modify Entity  
↓  
SaveChangesAsync()

EF Core then detects the changes through the Change Tracker.

---

# 🔗 Eager Loading

Related entities are loaded explicitly using `Include()` when related data is required.

Examples include:

Student  
↓  
Enrollments

and:

Enrollment  
↓  
Course

Eager loading makes the required relationships explicit and avoids unnecessary additional database queries when related data is required.

---

# 🗑️ Soft Delete

The project implements Soft Delete for students.

Instead of physically deleting the database record, the application changes:

`IsDeleted = false`

to:

`IsDeleted = true`

The database row remains available.

Example:

Before:

Student  
IsDeleted = false

After Soft Delete:

Student  
IsDeleted = true

This preserves the record while keeping it hidden from normal application queries.

---

# 🌐 Global Query Filter

A Global Query Filter is configured for the Student entity.

The filter conceptually applies:

`!IsDeleted`

to normal Student queries.

Therefore:

- `IsDeleted = false` → Student is visible
- `IsDeleted = true` → Student is hidden

This means normal Student queries automatically exclude soft-deleted records without manually adding a `Where(IsDeleted == false)` condition to every query.

---

# 🔓 IgnoreQueryFilters

When deleted students need to be explicitly retrieved, the Global Query Filter can be ignored for a specific query using:

`IgnoreQueryFilters()`

The query can then explicitly filter for:

`IsDeleted = true`

This allows the application to retrieve deleted records when required while keeping them hidden from normal queries.

---

# 🌱 Data Seeding

Static test data is provided using EF Core `HasData()`.

The seeded database contains:

### Students

- Multiple active students
- Multiple students with the same first name for search testing
- A soft-deleted student

### Courses

- C# Fundamentals
- SQL Server
- EF Core
- ASP.NET Core

### Instructors

Multiple instructors are seeded to test instructor relationships.

### CourseInstructor

Courses are assigned to instructors, including a course with multiple instructors.

### Enrollments

Enrollments contain:

- Student
- Course
- Grade
- Enrollment Date

The seed data intentionally includes:

- Different grades
- A nullable grade
- Multiple students in the same course
- Multiple courses for the same student
- A course without enrollments
- A soft-deleted student

This makes the seed data useful for testing normal scenarios and edge cases.

---

# 🧪 Testing

The project uses manual integration testing rather than a unit-testing framework.

The tests execute the actual:

Service  
↓  
AppDbContext  
↓  
Entity Framework Core  
↓  
SQL Server

This allows the project to verify real database behavior instead of testing service logic in isolation.

## Student Tests

- Get existing student
- Get non-existing student
- Search students by name
- Get student with enrollments
- Add student
- Update student
- Soft delete student
- Verify deleted student is hidden
- Retrieve deleted students

## Course Tests

- Get existing course
- Get non-existing course
- Add course
- Update course
- Delete course

## Enrollment Tests

- Get enrollment by student and course
- Get all students in a course
- Get courses and grades for a student
- Calculate average grade
- Handle nullable grades
- Handle courses with no grades
- Add enrollment
- Update enrollment
- Delete enrollment

---

# 📊 Example Test Data

## C# Fundamentals

Ahmed Adel → 85  
Ahmed Hassan → 75  
Omar Mohamed → 95  
Youssef Samir → 65

Expected average:

(85 + 75 + 95 + 65) / 4 = 80

## SQL Server

Ahmed Adel → 90  
Ahmed Hassan → NULL  
Sara Ali → 70

Expected average:

(90 + 70) / 2 = 80

The nullable grade is not treated as a numeric grade.

---

# 🧩 EF Core Concepts Demonstrated

This project was used to practice and understand:

- DbContext
- DbSet
- Entity relationships
- Primary keys
- Composite keys
- Foreign keys
- Navigation properties
- Include()
- Where()
- Select()
- FirstOrDefaultAsync()
- FindAsync()
- ToListAsync()
- AverageAsync()
- AsNoTracking()
- Change Tracking
- SaveChangesAsync()
- Global Query Filters
- IgnoreQueryFilters()
- Soft Delete
- HasData()
- EF Core Migrations
- Async/Await
- SQL Server integration

---

# 🗃️ Migrations

EF Core migrations are used to keep the database schema synchronized with the application's entity model.

Typical workflow:

Modify Entity  
↓  
Create Migration  
↓  
Review Migration  
↓  
Update Database

Example using Package Manager Console:

`Add-Migration InitialCreate`

`Update-Database`

Or using .NET CLI:

`dotnet ef migrations add InitialCreate`

`dotnet ef database update`

---

# ⚙️ Installation & Setup

## Prerequisites

Make sure the following are installed:

- .NET 8 SDK
- SQL Server
- Visual Studio
- Entity Framework Core tools

## 1. Clone the Project

`git clone <repository-url>`

`cd StudentManagementSystem`

## 2. Configure SQL Server

Update the connection string according to your SQL Server instance.

Example:

`Server=.\SQLEXPRESS;Database=StudentManagementDb;Trusted_Connection=True;TrustServerCertificate=True;`

Replace the Server value if a different SQL Server instance is being used.

## 3. Restore Dependencies

`dotnet restore`

## 4. Create / Update the Database

If migrations already exist:

`dotnet ef database update`

Or from Visual Studio Package Manager Console:

`Update-Database`

## 5. Run the Project

`dotnet run`

---

# 🧠 Design Decisions

## Why no Repository Pattern?

EF Core already provides a powerful data-access abstraction through `DbContext` and `DbSet`.

For this project's scope, adding a Repository layer would introduce another abstraction without providing enough value.

Therefore, the services communicate directly with `AppDbContext`.

## Why no Service Interfaces?

The project does not require multiple implementations of the services.

Adding interfaces would increase the number of abstractions without a practical requirement, so the services are used directly.

## Why no Controllers?

The project requirements focus on:

- Entity Framework Core
- Database operations
- Services
- LINQ
- Entity relationships
- Data seeding
- Migrations
- Testing

A REST API layer was therefore not necessary for the current scope.

---

# 📚 What This Project Demonstrates

The project demonstrates how to:

1. Design relational entities
2. Model one-to-many relationships
3. Model many-to-many relationships
4. Work with composite keys
5. Use foreign keys and navigation properties
6. Query related data using LINQ
7. Understand EF Core Change Tracking
8. Use AsNoTracking() for read-only queries
9. Load related entities using Include()
10. Perform asynchronous database operations
11. Implement Soft Delete
12. Apply Global Query Filters
13. Override filters using IgnoreQueryFilters()
14. Seed realistic test data
15. Apply EF Core migrations
16. Test the service-to-database workflow

---

# 🔮 Possible Future Improvements

The current implementation intentionally stays within the project's scope.

Potential future improvements include:

- REST API Controllers
- DTOs
- Input validation
- Global exception handling
- Pagination
- Authentication and Authorization
- Logging
- Automated Unit Testing
- Automated Integration Testing
- API documentation
- More advanced dependency injection
- Repository abstraction if the project grows significantly

---

# 👨‍💻 Author

Developed as a practical .NET 8 and Entity Framework Core project focused on relational database development, EF Core querying, entity relationships, asynchronous database operations, and backend service design.

---

## ⭐ Project Philosophy

Build simple.  
Understand deeply.  
Avoid unnecessary abstraction.
