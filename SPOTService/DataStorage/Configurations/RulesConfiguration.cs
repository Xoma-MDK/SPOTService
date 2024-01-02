using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPOTService.DataStorage.Entities;

namespace SPOTService.DataStorage.Configurations
{
    public class RulesConfiguration : IEntityTypeConfiguration<Rules>
    {
        public void Configure(EntityTypeBuilder<Rules> entity)
        {
            entity.ToTable(nameof(Rules));
            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id).IsRequired(true);
            entity.Property(e => e.Title).IsRequired(true);

        }
    }
}
