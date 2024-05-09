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

            entity.HasOne(x => x.QuestionGroup)
                  .WithMany(x => x.Questions)
                  .HasForeignKey(x => x.QuestionGroupId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
