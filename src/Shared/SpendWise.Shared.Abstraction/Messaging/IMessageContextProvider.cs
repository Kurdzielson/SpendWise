namespace SpendWise.Shared.Abstraction.Messaging;

public interface IMessageContextProvider
{
    IMessageContext Get(IMessage message);
}