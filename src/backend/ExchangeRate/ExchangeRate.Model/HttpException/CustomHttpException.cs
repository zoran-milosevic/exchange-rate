using System.Net;
using FluentValidation.Results;

namespace ExchangeRate.Model.HttpException;

public class CustomHttpException : Exception
{
    public HttpStatusCode StatusCode { get; set; }
    
    public CustomHttpException(HttpStatusCode statusCode, string message) : base(message)
    { 
        StatusCode = statusCode;
    }

    public CustomHttpException(HttpStatusCode statusCode, string[] messages) : base(string.Join(", ", messages))
    { 
        StatusCode = statusCode;
    }

    public CustomHttpException(HttpStatusCode statusCode, IEnumerable<ValidationFailure> errors) : base(string.Join(", ", errors.Select(q => q.ErrorMessage).ToArray()))
    { 
        StatusCode = statusCode;
    }
}