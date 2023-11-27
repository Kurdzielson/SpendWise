using SpendWise.Shared.Abstraction.Events;

namespace SpendWise.Modules.Users.Core.Users.Events;

internal record SignedUp(Guid UserId, string Email, string Role) : IEvent;