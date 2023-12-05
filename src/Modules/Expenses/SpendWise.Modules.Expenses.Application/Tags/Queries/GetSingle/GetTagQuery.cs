using SpendWise.Modules.Expenses.Application.Tags.DTO;

namespace SpendWise.Modules.Expenses.Application.Tags.Queries.GetSingle;

internal record GetTagQuery(Guid TagId) : IQuery<TagDto>;