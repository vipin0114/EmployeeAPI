using EmployeeAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace EmployeeAPI.Context
{



    public class EmployeeDBContext : DbContext
    {
        
        public DbSet<Employee> Employees { get; set; }

        public EmployeeDBContext()
        {
        }

        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=vm001\SQLEXPRESS;  initial catalog=EmployeeDB;integrated security=true; user id=sa;Password=Viviana@12345;");
        }
    }
}
