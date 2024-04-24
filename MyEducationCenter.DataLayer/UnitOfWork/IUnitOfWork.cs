using Microsoft.EntityFrameworkCore.Storage;

namespace MyEducationCenter.DataLayer;

public interface IUnitOfWork : IDisposable
{
    IOrganizationRepository OrganizationRepository { get; }
    IRoleRepository RoleRepository { get; }


    IRoleModuleRepository RoleModuleRepository { get; }
    IModuleRepository ModuleRepository { get; }
    IUserRepository UserRepository { get; }
    IUserRoleRepository UserRoleRepository { get; }
    public IDbContextTransaction CurrentTransaction { get; }
    IDbContextTransaction BeginTransaction();

    Task CommitAsync();
    Task RollbackAsync();
    Task SaveChangesAsync();
}
