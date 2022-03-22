using ExchangeRate.Model.DTO;
using ExchangeRate.Model.Integration;

namespace ExchangeRate.Interface.Service.DomainService;

public interface IExchangeRateManager
{
    List<ExchangeRateDTO> GetRates(List<SearchResultDTO> searchResults);
}