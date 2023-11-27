using SpendWise.Shared.Abstraction.Events;

namespace SpendWise.Modules.Users.Core.Users.Events;

internal record SignedIn(Guid UserId) : IEvent;