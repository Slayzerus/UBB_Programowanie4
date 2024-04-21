using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SmartERP.Development.Domain.Entities;

namespace SmartERP.Development.Database.Configuration
{
    public class CustomModuleConfiguration : IEntityTypeConfiguration<CustomModule>
    {
        public void Configure(EntityTypeBuilder<CustomModule> builder)
        {
            builder.ToTable("Development", "CustomModules");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name);
            builder.Property(x => x.DisplayName);
            builder.Property(x => x.Description);

            builder.HasMany(x => x.Entities)
                .WithOne(x => x.Module)
                .HasForeignKey(x => x.ModuleId);

            builder.HasMany(x => x.Views)
                .WithOne(x => x.Module)
                .HasForeignKey(x => x.ModuleId);
        }
    }
}
