using System.Net;
using System.Text.Json;

namespace ExchangeRate.Model.HttpException;

public class ExceptionResponse
{
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;
    public string Message { get; set; } = "An unexpected error occurred.";
    public virtual string ToJsonString() => JsonSerializer.Serialize(this);
}