using ExchangeRate.Interface.Service.DomainService;
using ExchangeRate.Model.DTO;
using ExchangeRate.Model.Integration;

namespace ExchangeRate.Service.DomainService;

public class ExchangeRateManager : IExchangeRateManager
{
    public List<ExchangeRateDTO> GetRates(List<SearchResultDTO> searchResults)
    {
        var result = new List<ExchangeRateDTO>();
        
        var ratesByDate = searchResults
            .GroupBy(g => new { g.Date, g.Currency})
            .Select(s => new { 
                Date = s.Key.Date,
                Currency = s.Key.Currency,
                Data = searchResults.FirstOrDefault(q => q.Date == s.Key.Date && q.Currency == s.Key.Currency)
                }
            )
            .ToList();

        var currencies = ratesByDate
            .GroupBy(g => g.Currency)
            .Select(g => g)
            .ToList();

        foreach (var curency in currencies)
        {
            var min = ratesByDate.Where(q => q.Currency == curency.Key).MinBy(m => m.Data?.CurrencyRate);
            var max = ratesByDate.Where(q => q.Currency == curency.Key).MaxBy(m => m.Data?.CurrencyRate);
            var avg = ratesByDate.Where(q => q.Currency == curency.Key).Select(s => s.Data?.CurrencyRate);

            result.Add(new ExchangeRateDTO {
                Date = min.Date,
                BaseCurrency = min.Data?.BaseCurrency,
                TargetCurrency = min.Data?.Currency,
                MinExchangeRate = min.Data?.CurrencyRate
            });

            result.Add(new ExchangeRateDTO {
                Date = max.Date,
                BaseCurrency = max.Data?.BaseCurrency,
                TargetCurrency = max.Data?.Currency,
                MaxExchangeRate = max.Data?.CurrencyRate
            });

            result.Add(new ExchangeRateDTO {
                BaseCurrency = searchResults.FirstOrDefault()?.BaseCurrency,
                TargetCurrency = curency.Key,
                AverageExchangeRate = avg.Average()
            });
        }

        return result;
    }
}