using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SpendWise.Modules.Expenses.Core.Expenses.Entities;
using SpendWise.Modules.Expenses.Core.Tags.Entities;
using SpendWise.Modules.Expenses.Core.Tags.Repositories;
using SpendWise.Modules.Expenses.Infrastructure.EF;

namespace SpendWise.Modules.Expenses.Infrastructure.Repositories;

internal class TagRepository(ExpensesWriteDbContext context) : ITagRepository
{
    private readonly DbSet<Tag> _tags = context.Tags;

    public async Task<Tag> GetAsync(Guid id, CancellationToken cancellationToken)
        => await _tags.AsNoTracking().Where(q => q.Id == id).FirstOrDefaultAsync(cancellationToken);

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