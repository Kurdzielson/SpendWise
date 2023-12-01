using SpendWise.Shared.Abstraction.Events;

namespace SpendWise.Modules.Customers.Core.Customers.Events;

internal record Unlocked(Guid CustomerId) : IEvent;