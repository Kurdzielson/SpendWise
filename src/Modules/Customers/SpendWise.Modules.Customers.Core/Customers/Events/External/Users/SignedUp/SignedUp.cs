using SpendWise.Shared.Abstraction.Events;

namespace SpendWise.Modules.Customers.Core.Customers.Events.External.Users.SignedUp;

internal record SignedUp(Guid UserId, string Email, string Role) : IEvent;