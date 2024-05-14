using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPOTService.DataStorage.Entities;

namespace SPOTService.DataStorage.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> entity)
        {
            entity.ToTable(nameof(Question));
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).IsRequired(true);
            entity.Property(e => e.Title).IsRequired(true);

            entity.HasMany(e => e.AnswerVariants)
                  .WithMany(e => e.Questions)
                  .UsingEntity<QuestionAnswerVariant>(
                x => x.HasOne(e => e.AnswerVariant).WithMany(e => e.QuestionAnswerVariants),
                t => t.HasOne(e => e.Question).WithMany(e => e.QuestionAnswerVariants)
            );

        }
    }
}
