namespace SpendWise.Shared.Abstraction.Exceptions;

public interface IExceptionToResponseMapper
{
    ExceptionResponse Map(Exception exception);
}