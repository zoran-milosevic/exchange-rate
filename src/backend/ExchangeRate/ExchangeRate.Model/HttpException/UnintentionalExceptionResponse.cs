using System.Text.Json;

namespace ExchangeRate.Model.HttpException;

public class UnintentionalExceptionResponse : ExceptionResponse
{
    public string InnerMessage { get; set; }
    public override string ToJsonString() => JsonSerializer.Serialize(this);
}