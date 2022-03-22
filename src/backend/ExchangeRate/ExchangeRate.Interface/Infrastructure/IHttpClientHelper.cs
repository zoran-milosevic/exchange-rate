using ExchangeRate.Model.DTO;
using ExchangeRate.Model.Integration;

namespace ExchangeRate.Interface.Infrastructure;

public interface IHttpClientHelper
{
    Task<List<SearchResultDTO>> GetExchangeRateAsync(string date, RequestModel model);
}