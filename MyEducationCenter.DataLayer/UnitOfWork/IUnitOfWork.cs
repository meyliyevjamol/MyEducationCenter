using Microsoft.EntityFrameworkCore.Storage;

namespace MyEducationCenter.DataLayer;

public interface IUnitOfWork : IDisposable
{
    IDbContextTransaction BeginTransaction();
    Task CommitAsync();
    Task RollbackAsync();
    Task SaveChangesAsync();
}
