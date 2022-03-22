namespace ExchangeRate.Model.Integration;

public class SearchResultDTO
{
    public string Currency { get; set; }
    public float CurrencyRate { get; set; }
    public string BaseCurrency { get; set; }
    public string Date { get; set; }
}
