using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPOTService.DataStorage.Entities;

namespace SPOTService.DataStorage.Configurations
{
    public class QuestionGroupConfiguration : IEntityTypeConfiguration<QuestionGroup>
    {
        public void Configure(EntityTypeBuilder<QuestionGroup> entity)
        {
            entity.HasKey(x => x.Id);

            entity.HasOne(x => x.ParentQuestionGroup)
                  .WithOne(x => x.ChildrenQuestionGroup)
                  .HasForeignKey<QuestionGroup>(x => x.ParentId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
