using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPOTService.DataStorage.Entities;

namespace SPOTService.DataStorage.Configurations
{
    public class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> entity)
        {
            entity.ToTable(nameof(Answer));
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id).IsRequired(true);
            entity.Property(e => e.SurveyId).IsRequired(true);
            entity.Property(e => e.QuestionId).IsRequired(true);
            

            entity.HasOne(e => e.Survey)
                  .WithMany(e => e.Answers)
                  .HasForeignKey(e => e.SurveyId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Question)
                  .WithMany(e => e.Answers)
                  .HasForeignKey(e => e.QuestionId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.AnswerVariant)
                  .WithMany(e => e.Answers)
                  .HasForeignKey(e => e.AnswerVariantId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Respondent)
                  .WithMany(e => e.Answers)
                  .HasForeignKey(e => e.RespondentId)
                  .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
