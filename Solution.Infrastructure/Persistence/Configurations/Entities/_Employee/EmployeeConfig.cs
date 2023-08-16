using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solution.Domain.Entities._Employee;

namespace Solution.Infrastructure.Persistence.Configurations.Entities._Employee
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            /* Table and primary key configuration */
            builder.ToTable(name: "Employee", schema: "employee").HasKey(pk => pk.Employee_Id);
            builder.ToTable(name: "Employee", schema: "employee").Property(pk => pk.Employee_Id).HasColumnName("Employee_Id").UseIdentityColumn();

            /* Table columns configuration */
            builder.Property(p => p.First_Name).HasColumnName("First_Name").HasMaxLength(100);
            builder.Property(p => p.Last_Name).HasColumnName("Last_Name").HasMaxLength(100);
            builder.Property(p => p.Mobile_Number).HasColumnName("Mobile_Number").HasMaxLength(13);
            builder.Property(p => p.Email).HasColumnName("Email").HasMaxLength(100);
            builder.Property(p => p.DateOfBirth).HasColumnName("DateOfBirth");
            builder.Property(p => p.Is_Deleted).HasColumnName("Is_Deleted");

            /* Configure relationships */
        }
    }
}
