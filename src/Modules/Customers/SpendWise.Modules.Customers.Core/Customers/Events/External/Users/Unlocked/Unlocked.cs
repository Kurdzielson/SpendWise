using SpendWise.Shared.Abstraction.Events;

namespace SpendWise.Modules.Customers.Core.Customers.Events.External.Users.Unlocked;

internal record Unlocked(Guid UserId) : IEvent;