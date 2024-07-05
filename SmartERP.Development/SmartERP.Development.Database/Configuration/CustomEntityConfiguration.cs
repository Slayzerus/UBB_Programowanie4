using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartERP.Development.Domain.Entities;

namespace SmartERP.Development.Database.Configuration
{
    public class CustomEntityConfiguration : IEntityTypeConfiguration<CustomEntity>
    {
        public void Configure(EntityTypeBuilder<CustomEntity> builder)
        {
            builder.ToTable("CustomEntities", "development");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name);
            builder.Property(x => x.DisplayName);
            builder.Property(x => x.Description);

            builder.HasMany(x => x.Fields)
                .WithOne(x => x.Entity)
                .HasForeignKey(x => x.EntityId);            
        }
    }
}
