using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPOTService.DataStorage.Entities;

namespace SPOTService.DataStorage.Configurations
{
    public class AnswerVariantConfiguration : IEntityTypeConfiguration<AnswerVariant>
    {
        public void Configure(EntityTypeBuilder<AnswerVariant> entity)
        {
            entity.ToTable(nameof(AnswerVariant));
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).IsRequired(true);
            entity.Property(e => e.Title).IsRequired(true);

            entity.HasOne(x => x.Question)
                  .WithMany(x => x.AnswerVariants)
                  .HasForeignKey(x => x.QuestionId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
