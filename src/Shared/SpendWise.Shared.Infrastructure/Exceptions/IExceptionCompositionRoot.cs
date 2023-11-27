using SpendWise.Shared.Abstraction.Exceptions;

namespace SpendWise.Shared.Infrastructure.Exceptions;

public interface IExceptionCompositionRoot
{
    ExceptionResponse Map(Exception exception);
}