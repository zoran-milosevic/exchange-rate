namespace ExchangeRate.Model.Binding;

public class ExchangeRateBindingModel
{
    public string? BaseCurrency { get; set; }
    public ICollection<string> TargetCurrencies { get; set; }
    public ICollection<string> Dates { get; set; }
}
