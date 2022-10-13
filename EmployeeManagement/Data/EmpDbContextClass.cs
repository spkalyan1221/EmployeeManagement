using EmployeeManagement.Models.Domainmodel;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Data
{
    public class EmpDbContextClass : DbContext
    {
        public EmpDbContextClass(DbContextOptions options) : base(options)
        {

        }

       public DbSet<Employee>Employees { get; set; }
    }
}
