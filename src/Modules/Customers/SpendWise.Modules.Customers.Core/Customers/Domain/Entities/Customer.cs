using SpendWise.Modules.Customers.Core.Customers.Domain.ValueObjects.State;
using SpendWise.Modules.Customers.Core.Customers.Exceptions;
using SpendWise.Shared.Abstraction.Kernel.Types;
using SpendWise.Shared.Abstraction.Kernel.Types.UserId;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects.CreatedAt;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects.Date;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects.Email;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects.FullName;
using SpendWise.Shared.Abstraction.Kernel.ValueObjects.Nick;

namespace SpendWise.Modules.Customers.Core.Customers.Domain.Entities;

internal class Customer
{
    //Customer Id is same as User Id
    public UserId Id { get; set; }
    public Email Email { get; set; }
    public Nick Nick { get; set; }
    public FullName FullName { get; set; }
    public CustomerState State { get; set; }
    public CreatedAt CreatedAt { get; set; }
    public Date CompletedAt { get; set; }
    public Date VerifiedAt { get; set; }
 
    //solution to dotnet ef error
    private Customer()
    {
    }

    private Customer(Guid userId, string email, DateTime? createdAt)
    {
        Id = userId;
        Email = email;
        CreatedAt = createdAt;
        Nick = null;
        FullName = null;
        CompletedAt = null;
        State = AvailableCustomerState.DefaultState;
    }

    public static Customer CreateFromUser(UserId userId, Email email, CreatedAt createdAt)
        => new(userId, email, createdAt);
}