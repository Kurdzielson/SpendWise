using SpendWise.Modules.Expenses.Application.Tags.DTO;
using SpendWise.Modules.Expenses.Infrastructure.EF.Tags.Configurations.Read.Model;

namespace SpendWise.Modules.Expenses.Infrastructure.EF.Tags.Queries;

internal static class Extensions
{
    public static TagDto AsDto(this TagReadModel tag)
        => tag.Map<TagDto>();

    private static T Map<T>(this TagReadModel tag) where T : TagDto, new()
        => new()
        {
            TagId = tag.Id,
            ColorHex = tag.ColorHex,
            CustomerId = tag.CustomerId,
            Name = tag.Name
        };
}