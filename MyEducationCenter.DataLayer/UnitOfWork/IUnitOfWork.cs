using Microsoft.EntityFrameworkCore.Storage;

namespace MyEducationCenter.DataLayer;

public interface IUnitOfWork : IDisposable
{
    IOrganizationRepository OrganizationRepository { get; }
    IDbContextTransaction BeginTransaction();
    Task CommitAsync();
    Task RollbackAsync();
    Task SaveChangesAsync();
}
