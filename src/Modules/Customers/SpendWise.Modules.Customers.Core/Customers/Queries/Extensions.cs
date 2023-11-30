using Microsoft.AspNetCore.Http.HttpResults;
using SpendWise.Modules.Customers.Core.Customers.DAL.Configurations.Read.Model;
using SpendWise.Modules.Customers.Core.Customers.DTO;

namespace SpendWise.Modules.Customers.Core.Customers.Queries;

internal static class Extensions
{
    public static CustomerDto AsDto(this CustomerReadModel customer)
        => customer.Map<CustomerDto>();

    public static CustomerDetailsDto AsDetailsDto(this CustomerReadModel customer)
    {
        var dto = customer.Map<CustomerDetailsDto>();
        dto.CreatedAt = customer.CreatedAt;
        dto.CompletedAt = customer.CompletedAt;
        dto.VerifiedAt = customer.VerifiedAt;

        return dto;
    }

    private static T Map<T>(this CustomerReadModel customer) where T : CustomerDto, new()
        => new()
        {
            CustomerId = customer.Id,
            Email = customer.Email,
            Nick = customer.Nick,
            State = customer.State,
            FullName = customer.FullName
        };
}