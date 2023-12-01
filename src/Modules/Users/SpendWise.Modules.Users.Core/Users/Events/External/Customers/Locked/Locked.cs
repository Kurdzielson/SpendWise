using SpendWise.Shared.Abstraction.Events;

namespace SpendWise.Modules.Users.Core.Users.Events.External.Customers.Locked;

internal record Locked(Guid CustomerId) : IEvent;