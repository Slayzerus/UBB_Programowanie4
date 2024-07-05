using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartERP.Development.Domain.Entities;

namespace SmartERP.Development.Database.Configuration
{
    public class CustomViewConfiguration : IEntityTypeConfiguration<CustomView>
    {
        public void Configure(EntityTypeBuilder<CustomView> builder)
        {
            builder.ToTable("CustomViews", "development");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Module)
                .WithMany(x => x.Views)
                .HasForeignKey(x => x.ModuleId);
        }
    }
}
