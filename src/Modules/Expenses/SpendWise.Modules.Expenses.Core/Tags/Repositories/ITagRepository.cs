using SpendWise.Modules.Expenses.Core.Tags.Entities;

namespace SpendWise.Modules.Expenses.Core.Tags.Repositories;

internal interface ITagRepository
{
    Task<bool> DoesExistAsync(Guid id, Guid customerId, CancellationToken cancellationToken);
    Task<Tag> GetAsync(Guid id, Guid customerId, CancellationToken cancellationToken);
    Task<Guid> AddAsync(Tag tag, CancellationToken cancellationToken);
    Task<Guid> UpdateAsync(Tag tag, CancellationToken cancellationToken);
    Task RemoveAsync(Tag tag, CancellationToken cancellationToken);
}