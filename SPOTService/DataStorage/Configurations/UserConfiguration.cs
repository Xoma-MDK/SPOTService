using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPOTService.DataStorage.Entities;

namespace SPOTService.DataStorage.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable(nameof(User));
            entity.HasKey(x => x.Id);

            entity.Property(e => e.Id).IsRequired(true);
            entity.Property(e => e.Surname).IsRequired(true);
            entity.Property(e => e.Name).IsRequired(true);
            entity.Property(e => e.Login).IsRequired(true);
            entity.Property(e => e.PasswordHash).IsRequired(true);
            entity.Property(e => e.RoleId).IsRequired(true);

            entity.HasOne(e => e.Role)
                  .WithMany(e => e.Users)
                  .HasForeignKey(e => e.RoleId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
