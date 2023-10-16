using Microsoft.EntityFrameworkCore;

namespace CharchoobApi.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
