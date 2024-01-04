using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPOTService.DataStorage.Entities;

namespace SPOTService.DataStorage.Configurations
{
    public class RespondentConfiguration : IEntityTypeConfiguration<Respondent>
    {
        public void Configure(EntityTypeBuilder<Respondent> entity)
        {
            entity.ToTable(nameof(Respondent));
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).IsRequired(true);
            entity.Property(e => e.GroupId).IsRequired(true);
            entity.Property(e => e.TelegramId).IsRequired(true);

            entity.HasOne(e => e.Group)
                  .WithMany(e => e.Respondents)
                  .HasForeignKey(e => e.GroupId)
                  .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
