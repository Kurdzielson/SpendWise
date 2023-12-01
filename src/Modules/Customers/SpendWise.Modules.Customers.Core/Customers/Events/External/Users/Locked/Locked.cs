using SpendWise.Shared.Abstraction.Events;

namespace SpendWise.Modules.Customers.Core.Customers.Events.External.Users.Locked;

internal record Locked(Guid UserId) : IEvent;