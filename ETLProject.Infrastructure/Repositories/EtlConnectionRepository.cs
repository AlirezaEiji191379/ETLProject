using ETLProject.Infrastructure.DatabaseContext;
using ETLProject.Infrastructure.Entities;
using ETLProject.Infrastructure.Repositories.Abstractions;

namespace ETLProject.Infrastructure.Repositories;

public class EtlConnectionRepository : BaseRepository<EtlConnection>
{
    public EtlConnectionRepository(EtlDbContext dbContext) : base(dbContext)
    {
    }
}