using CharchoobApi.Domain.Entities.gnr;
using Microsoft.EntityFrameworkCore;

namespace CharchoobApi.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TblRefreshToken> TblRefreshToken { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
