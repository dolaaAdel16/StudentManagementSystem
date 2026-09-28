using Microsoft.EntityFrameworkCore;
using StudentManagementSystem.Data;

namespace StudentManagementSystem
{
    public class Program
    {
        static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(
                "Server=.;Database=StudentManagementDb;Trusted_Connection=True;TrustServerCertificate=True;"
            )
            .Options;


            using var context = new AppDbContext();


        }
    }
}
