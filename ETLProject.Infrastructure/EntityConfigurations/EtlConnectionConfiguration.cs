using ETLProject.Common.Database;
using ETLProject.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ETLProject.Infrastructure.EntityConfigurations;

public class EtlConnectionConfiguration : IEntityTypeConfiguration<EtlConnection>
{
    public void Configure(EntityTypeBuilder<EtlConnection> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.DataSourceType)
            .HasConversion(t => t.ToString(),
                t => (DataSourceType)Enum.Parse(typeof(DataSourceType), t));
    }
}