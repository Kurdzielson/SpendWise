using System.Net;

namespace SpendWise.Shared.Abstraction.Exceptions;

public record ExceptionResponse(object Response, HttpStatusCode StatusCode);