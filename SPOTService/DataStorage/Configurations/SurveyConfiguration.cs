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
            entity.Property(x => x.CreatorId).IsRequired(true);

            
            entity.HasOne(x => x.Group)
                  .WithMany(x => x.Surveys)
                  .HasForeignKey(x => x.GroupId)
                  .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(x => x.User)
                  .WithMany(x => x.Surveys)
                  .HasForeignKey(x => x.CreatorId)
                  .OnDelete(DeleteBehavior.Cascade);
            
            entity.HasOne(x => x.MainQuestionGroup)
                  .WithMany(x => x.Surveys)
                  .HasForeignKey(x => x.MainQuestionGroupId)
                  .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
