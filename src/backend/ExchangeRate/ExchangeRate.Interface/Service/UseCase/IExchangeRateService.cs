using ExchangeRate.Model.Binding;
using ExchangeRate.Model.DTO;

namespace ExchangeRate.Interface.Service.UseCase;

public interface IExchangeRateService
{
    Task<IEnumerable<ExchangeRateDTO>> SearchAsync(ExchangeRateBindingModel model);
}