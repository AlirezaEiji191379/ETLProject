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
        if (entity.Username != null)
            DbContext.Entry(entity).Property(x => x.Username).IsModified = true;

        if (entity.Password != null)
            DbContext.Entry(entity).Property(x => x.Password).IsModified = true;

        if (entity.Host != null)
            DbContext.Entry(entity).Property(x => x.Host).IsModified = true;
        
        if (entity.Port != null)
            DbContext.Entry(entity).Property(x => x.Port).IsModified = true;
        
        if (entity.ConnectionName != null)
            DbContext.Entry(entity).Property(x => x.ConnectionName).IsModified = true;
    }
}