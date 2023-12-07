using SpendWise.Modules.Customers.Core.Customers.Domain.ValueObjects.State;
using SpendWise.Shared.Abstraction.Kernel.Types.CustomerId;
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
    public CustomerId Id { get; set; }
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

    private Customer(UserId userId, Email email, CreatedAt createdAt)
    {
        Id = CustomerId.CreateFromUser(userId);
        Email = email;
        CreatedAt = createdAt;
        Nick = null;
        FullName = null;
        CompletedAt = null;
        State = AvailableCustomerStates.DefaultState;
    }

    public static Customer CreateFromUser(Guid userId, string email, DateTime createdAt)
        => new(userId, email, createdAt);

    public void Complete(Nick nick, FullName fullName, Date completedAt)
    {
        Nick = nick;
        FullName = fullName;
        CompletedAt = completedAt;

        State = AvailableCustomerStates.Completed;
    }

    public void Verify(Date verifiedAt)
    {
        VerifiedAt = verifiedAt;

        State = AvailableCustomerStates.Verified;
    }

    public void Lock()
        => State = AvailableCustomerStates.Locked;

    public void Unlock()
        => State = CompletedAt is null
            ? AvailableCustomerStates.New
            : VerifiedAt is null
                ? AvailableCustomerStates.Completed
                : AvailableCustomerStates.Verified;

    public void Update(Nick nick, FullName fullName)
    {
        Nick = nick;
        FullName = fullName;
    }
}