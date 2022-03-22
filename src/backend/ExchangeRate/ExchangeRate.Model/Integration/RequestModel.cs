namespace ExchangeRate.Model.Integration;

public class RequestModel
{
    public string Format { get; set; }
    public string Base { get; set; }
    public string Symbols { get; set; }
}