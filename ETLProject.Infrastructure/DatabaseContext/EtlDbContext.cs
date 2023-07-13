using System.Reflection;
using ETLProject.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace ETLProject.Infrastructure.DatabaseContext;

public class EtlDbContext : DbContext
{
    public EtlDbContext(DbContextOptions options) : base(options)
    {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    public DbSet<EtlConnection> EtlConnections { get; set; }

}