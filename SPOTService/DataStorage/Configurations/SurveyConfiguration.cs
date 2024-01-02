using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPOTService.DataStorage.Entities;

namespace SPOTService.DataStorage.Configurations
{
    public class SurveyConfiguration : IEntityTypeConfiguration<Survey>
    {
        public void Configure(EntityTypeBuilder<Survey> entity)
        {
            entity.ToTable(nameof(Survey));
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id).IsRequired(true);
            entity.Property(x => x.Title).IsRequired(true);
            entity.Property(x => x.AccessCode).IsRequired(true);
            entity.Property(x => x.Department).IsRequired(true);
            entity.Property(x => x.GroupId).IsRequired(true);
            entity.Property(x => x.UserId).IsRequired(true);

            entity.HasMany(x => x.Question)
                  .WithMany(x => x.Surveys)
                  .UsingEntity<SurveyQuestion>(
                x => x.HasOne(x => x.Question).WithMany(x => x.SurveyQuestions),
                x => x.HasOne(x => x.Survey).WithMany(x => x.SurveyQuestions)
                );
            
            entity.HasOne(x => x.Group)
                  .WithOne(x => x.Survey)
                  .HasForeignKey<Survey>(x => x.GroupId)
                  .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(x => x.User)
                  .WithOne(x => x.Survey)
                  .HasForeignKey<Survey>(x => x.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
