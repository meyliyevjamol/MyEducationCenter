using Microsoft.EntityFrameworkCore.Storage;

namespace MyEducationCenter.DataLayer;

public class UnitOfWork : IUnitOfWork
{
    public AppDbContext _context;

    private IDbContextTransaction _transaction;
    public IDbContextTransaction CurrentTransaction => _transaction;

    #region Repository
    private IOrganizationRepository _organizationRepository;
    private IRoleRepository _roleRepository;

    private IRoleModuleRepository _roleModuleRepository;
    private IModuleRepository _moduleRepository;
    private IUserRoleRepository _userRoleRepository;
    private IUserRepository _userRepository;
    #endregion


    private readonly IServiceProvider _serviceProvider;

    public UnitOfWork(AppDbContext context, IServiceProvider serviceProvider)
    {
        _context = context;
        _serviceProvider = serviceProvider;
    }
    public IOrganizationRepository OrganizationRepository
    {
        get
        {
            if (_organizationRepository == null)
                _organizationRepository = new OrganizationRepository(_context);
            return _organizationRepository;
        }
    }

    public IRoleRepository RoleRepository
    {
        get
        {
            if (_roleRepository == null)
                _roleRepository = new RoleRepository(_context);
            return _roleRepository;
        }
    }
    public IRoleModuleRepository RoleModuleRepository
    {
        get
        {
            if (_roleModuleRepository == null)
                _roleModuleRepository = new RoleModuleRepository(_context);
            return _roleModuleRepository;
        }
    }
    public IModuleRepository ModuleRepository
    {
        get
        {
            if (_moduleRepository == null)
                _moduleRepository = new ModuleRepository(_context);
            return _moduleRepository;
        }
    }

    public IUserRoleRepository UserRoleRepository
    {
        get
        {
            if (_userRoleRepository == null)
                _userRoleRepository = new UserRoleRepository(_context);
            return _userRoleRepository;
        }
    }
    public IUserRepository UserRepository
    {
        get
        {
            if (_userRepository == null)
                _userRepository = new UserRepository(_context);
            return _userRepository;
        }
    }

    public IDbContextTransaction BeginTransaction()
    {
        return _transaction = _context.Database.BeginTransaction();
    }


    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
        await _transaction.CommitAsync();
    }

    public async Task RollbackAsync()
    {
        await _transaction.RollbackAsync();
    }

    public Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _transaction?.DisposeAsync();
        _context.DisposeAsync();
    }
}
