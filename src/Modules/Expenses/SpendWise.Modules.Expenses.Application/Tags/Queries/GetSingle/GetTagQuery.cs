using SpendWise.Modules.Expenses.Application.Tags.DTO;
using SpendWise.Shared.Abstraction.Queries;

namespace SpendWise.Modules.Expenses.Application.Tags.Queries.GetSingle;

internal record GetTagQuery(Guid TagId) : IQuery<TagDto>;