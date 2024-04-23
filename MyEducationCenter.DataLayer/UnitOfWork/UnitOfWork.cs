using Microsoft.EntityFrameworkCore.Storage;

namespace MyEducationCenter.DataLayer;

public class UnitOfWork:IUnitOfWork
{
    public AppDbContext _context;

    private IDbContextTransaction _transaction;
    public IDbContextTransaction CurrentTransaction => _transaction;

    #region Repository
    private IOrganizationRepository _organizationRepository;
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
