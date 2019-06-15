using DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infra
{
    public interface IApplicationDbContext : IDisposable, IInfrastructure<IServiceProvider>, IDbContextDependencies, IDbSetCache, IDbQueryCache, IDbContextPoolable
    {
        DbSet<Usuario> Usuarios { get; set; }
        DbSet<Comanda> Comandas { get; set; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        DatabaseFacade Database { get; }
    }
}
