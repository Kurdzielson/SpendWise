using SpendWise.Modules.Expenses.Application.Tags.DTO;

namespace SpendWise.Modules.Expenses.Application.Tags.Queries.GetPaginated;

internal class GetTagsQuery : PagedQuery<TagDto>
{
    public string Name { get; set; }
}