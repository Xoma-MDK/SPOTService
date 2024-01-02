using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPOTService.DataStorage.Entities;

namespace SPOTService.DataStorage.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> entity)
        {
            entity.ToTable(nameof(Role));
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).IsRequired(true);
            entity.Property(e => e.Title).IsRequired(true);

            entity.HasMany(e => e.Rules)
                  .WithMany(e => e.Roles)
                  .UsingEntity<RoleRules>(
                    x => x.HasOne(x => x.Rule).WithMany(x => x.RoleRules),
                    x => x.HasOne(x => x.Role).WithMany(x => x.RoleRules)
                  );
        }
    }
}
