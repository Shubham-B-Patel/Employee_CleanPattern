using Microsoft.EntityFrameworkCore;
using Solution.Core.Interfaces.Comman;
using Solution.Domain.Entities._Employee;
using Solution.Domain.Entities._User;
using System.Reflection;

namespace Solution.Infrastructure.Persistence
{
    public class EmployeeDbContext : DbContext, IEmployeeDbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Employee> Employees => Set<Employee>();

        public DbSet<Skill> Skills => Set<Skill>();

        public DbSet<Users> Users => Set<Users>();
    }
}
