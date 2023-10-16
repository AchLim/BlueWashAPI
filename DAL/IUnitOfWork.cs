using Microsoft.EntityFrameworkCore.Storage;
using WebAPI.Models;

namespace WebAPI.DAL
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        bool HasActiveTransaction { get; }
        IDbContextTransaction? GetCurrentTransaction();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task CommitAsync(IDbContextTransaction transaction);
    }
}
