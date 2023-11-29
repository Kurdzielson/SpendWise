using Microsoft.EntityFrameworkCore;
using SpendWise.Modules.Customers.Core.Customers.Domain.Entities;
using SpendWise.Modules.Customers.Core.Customers.Domain.Repositories;

namespace SpendWise.Modules.Customers.Core.Customers.DAL.Repositories;

internal class CustomerRepository(CustomersWriteDbContext context) : ICustomerRepository
{
    private readonly DbSet<Customer> _customers = context.Customers;

    public Task<bool> DoesExistAsync(string nick, CancellationToken cancellationToken)
        => _customers.AsNoTracking()
            .Where(q => q.Nick == nick)
            .AnyAsync(cancellationToken);

    public Task<bool> DoesExistAsync(Guid id, CancellationToken cancellationToken)
        => _customers
            .Where(q => q.Id == id)
            .AnyAsync(cancellationToken);

    public Task<Customer> GetAsync(Guid id, CancellationToken cancellationToken)
        => _customers
            .Where(q => q.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

    public async Task<Guid> AddAsync(Customer customer, CancellationToken cancellationToken)
    {
        var result = await _customers.AddAsync(customer, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return result.Entity.Id;
    }

    public async Task<Guid> UpdateAsync(Customer customer, CancellationToken cancellationToken)
    {
        var result = _customers.Update(customer);
        await context.SaveChangesAsync(cancellationToken);
        return result.Entity.Id;
    }
}