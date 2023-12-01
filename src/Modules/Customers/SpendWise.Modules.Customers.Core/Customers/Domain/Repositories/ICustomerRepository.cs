using SpendWise.Modules.Customers.Core.Customers.Domain.Entities;

namespace SpendWise.Modules.Customers.Core.Customers.Domain.Repositories;

internal interface ICustomerRepository
{
    Task<bool> DoesExistAsync(string nick, CancellationToken cancellationToken);
    Task<bool> DoesExistAsync(Guid id, CancellationToken cancellationToken);
    Task<Customer> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<Guid> AddAsync(Customer customer, CancellationToken cancellationToken);
    Task<Guid> UpdateAsync(Customer customer, CancellationToken cancellationToken);
}