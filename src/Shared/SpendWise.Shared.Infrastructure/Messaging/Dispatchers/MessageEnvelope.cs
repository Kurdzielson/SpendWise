using SpendWise.Shared.Abstraction.Messaging;

namespace SpendWise.Shared.Infrastructure.Messaging.Dispatchers;

internal record MessageEnvelope(IMessage Message, IMessageContext MessageContext);