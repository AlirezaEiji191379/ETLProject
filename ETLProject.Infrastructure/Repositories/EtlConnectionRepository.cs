using ETLProject.Infrastructure.DatabaseContext;
using ETLProject.Infrastructure.Entities;
using ETLProject.Infrastructure.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace ETLProject.Infrastructure.Repositories;

public class EtlConnectionRepository : BaseRepository<EtlConnection>
{
    public EtlConnectionRepository(EtlDbContext dbContext) : base(dbContext)
    {
    }

    public override void Attach(EtlConnection entity)
    {
        DbContext.Attach(entity);
        DbContext.Entry(entity).State = EntityState.Modified;
    }
}