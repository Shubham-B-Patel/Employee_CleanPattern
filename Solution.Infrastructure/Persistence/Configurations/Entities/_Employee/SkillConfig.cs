using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Solution.Domain.Entities._Employee;

namespace Solution.Infrastructure.Persistence.Configurations.Entities._Employee
{
    public class SkillConfig : IEntityTypeConfiguration<Skill>
    {
        public void Configure(EntityTypeBuilder<Skill> builder)
        {
            /* Table and primary key configuration */
            builder.ToTable(name: "Skill", schema: "employee").HasKey(pk => pk.Skill_Id);
            builder.ToTable(name: "Skill", schema: "employee").Property(pk => pk.Skill_Id).HasColumnName("Skill_Id").UseIdentityColumn();

            /* Table columns configuration */
            builder.Property(p => p.Tech_Name).HasColumnName("Tech_Name").HasMaxLength(100);
            builder.Property(p => p.Work_Exp).HasColumnName("Work_Exp");
            builder.Property(p => p.Rating).HasColumnName("Rating");

            /* Configure relationships */
            builder.HasOne(p => p.Employee)
                   .WithMany(pta => pta.Skills)
                   .HasForeignKey(fk => fk.Employee_Id);
        }
    }
}
