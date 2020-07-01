using EmployeeManagement.Models;
using System.Data.Entity;

namespace EmployeeManagement.Context
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext() : base("name=EmployeeDbContext")
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}