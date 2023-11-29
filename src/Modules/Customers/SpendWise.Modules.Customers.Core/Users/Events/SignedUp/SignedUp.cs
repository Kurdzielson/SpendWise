using SpendWise.Shared.Abstraction.Events;

namespace SpendWise.Modules.Customers.Core.Users.Events.SignedUp;

internal record SignedUp(Guid UserId, string Email, string Role) : IEvent;