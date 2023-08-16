using Microsoft.EntityFrameworkCore;
using Solution.Domain.Entities._Employee;
using Solution.Domain.Entities._User;

namespace Solution.Core.Interfaces.Comman
{
    public interface IEmployeeDbContext
    {
        //DbSet
        DbSet<Employee> Employees { get; }
        DbSet<Skill> Skills { get; }
        DbSet<Users> Users { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
