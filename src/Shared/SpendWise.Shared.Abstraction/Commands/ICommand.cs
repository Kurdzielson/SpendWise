using SpendWise.Shared.Abstraction.Messaging;

namespace SpendWise.Shared.Abstraction.Commands;

//Marker
public interface ICommand : IMessage;

public interface ICommand<TResult> : IMessage where TResult : class;