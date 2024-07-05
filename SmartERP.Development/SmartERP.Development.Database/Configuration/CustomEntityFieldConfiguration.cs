using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartERP.Development.Domain.Entities;

namespace SmartERP.Development.Database.Configuration
{
    public class CustomEntityFieldConfiguration : IEntityTypeConfiguration<CustomEntityField>
    {
        public void Configure(EntityTypeBuilder<CustomEntityField> builder)
        {
            builder.ToTable("CustomEntityFields", "development");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name);
            builder.Property(x => x.DisplayName);
            builder.Property(x => x.Type);
            builder.Property(x => x.IsRequired);

            builder.HasOne(x => x.Entity)
                .WithMany(x => x.Fields)
                .HasForeignKey(x => x.EntityId);
        }
    }
}
