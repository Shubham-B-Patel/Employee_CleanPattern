using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solution.Domain.Entities._User;

namespace Solution.Infrastructure.Persistence.Configurations.Entities._User
{
    public class UserConfig : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            /* Table and primary key configuration */
            builder.ToTable(name: "Users", schema: "employee").HasKey(pk => pk.User_Id);
            builder.ToTable(name: "Users", schema: "employee").Property(pk => pk.User_Id).HasColumnName("Employee_Id").UseIdentityColumn();

            /* Table columns configuration */
            builder.Property(p => p.First_Name).HasColumnName("First_Name").HasMaxLength(50);
            builder.Property(p => p.Last_Name).HasColumnName("Last_Name").HasMaxLength(50);
            builder.Property(p => p.User_Name).HasColumnName("User_Name").HasMaxLength(100);
            builder.Property(p => p.DateOfBirth).HasColumnName("DateOfBirth");
            builder.Property(p => p.Email).HasColumnName("Email").HasMaxLength(100);
            builder.Property(p => p.Mobile_Number).HasColumnName("Mobile_Number").HasMaxLength(13);
            builder.Property(p => p.Password).HasColumnName("Password").HasMaxLength(50);
            builder.Property(p => p.Profile_Path).HasColumnName("Profile_Path").HasMaxLength(250);

            /* Configure relationships */
        }
    }
}
