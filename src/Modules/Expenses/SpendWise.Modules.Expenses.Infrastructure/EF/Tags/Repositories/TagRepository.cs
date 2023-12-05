using Microsoft.EntityFrameworkCore;
using SpendWise.Modules.Expenses.Core.Tags.Entities;
using SpendWise.Modules.Expenses.Core.Tags.Repositories;

namespace SpendWise.Modules.Expenses.Infrastructure.EF.Tags.Repositories;

internal class TagRepository(ExpensesWriteDbContext context) : ITagRepository
{
    private readonly DbSet<Tag> _tags = context.Tags;

    public Task<bool> DoesExistAsync(Guid id, Guid customerId, CancellationToken cancellationToken)
        => _tags.AsNoTracking()
            .Where(q => q.Id == id && q.CustomerId == customerId)
            .AnyAsync(cancellationToken);

    public async Task<Tag> GetAsync(Guid id, Guid customerId, CancellationToken cancellationToken)
        => await _tags.AsNoTracking()
            .Where(q => q.Id == id && q.CustomerId == customerId)
            .FirstOrDefaultAsync(cancellationToken);


    public async Task<Guid> AddAsync(Tag tag, CancellationToken cancellationToken)
    {
        var result = await _tags.AddAsync(tag, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return result.Entity.Id;
    }

    public async Task<Guid> UpdateAsync(Tag tag, CancellationToken cancellationToken)
    {
        var result = _tags.Update(tag);
        await context.SaveChangesAsync(cancellationToken);

        return result.Entity.Id;
    }

    public async Task RemoveAsync(Tag tag, CancellationToken cancellationToken)
    {
        _tags.Remove(tag);
        await context.SaveChangesAsync(cancellationToken);
    }
}