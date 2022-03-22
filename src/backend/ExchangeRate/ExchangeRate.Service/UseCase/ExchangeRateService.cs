using Microsoft.Extensions.Logging;
using AutoMapper;
using ExchangeRate.Interface.Infrastructure;
using ExchangeRate.Interface.Service.DomainService;
using ExchangeRate.Interface.Service.UseCase;
using ExchangeRate.Model.Binding;
using ExchangeRate.Model.Integration;
using ExchangeRate.Model.DTO;

namespace ExchangeRate.Service.UseCase;

public class ExchangeRateService : IExchangeRateService
{
    private readonly IMapper _mapper;
    private readonly ILogger<ExchangeRateService> _logger;
    private readonly IHttpClientHelper _http;
    private readonly IExchangeRateManager _exchangeRateManager;

    public ExchangeRateService(IMapper mapper, ILogger<ExchangeRateService> logger, IHttpClientHelper http, IExchangeRateManager exchangeRateManager)
    {
        _mapper = mapper;
        _logger = logger;
        _http = http;
        _exchangeRateManager = exchangeRateManager;
    }

    public async Task<IEnumerable<ExchangeRateDTO>> SearchAsync(ExchangeRateBindingModel model)
    {
        var searchResults = new List<SearchResultDTO>();
        var tasks = new List<Task<List<SearchResultDTO>>>();
        var requestModel = _mapper.Map<RequestModel>(model);

        model.Dates.ToList().ForEach(date => tasks.Add(_http.GetExchangeRateAsync(date, requestModel)));

        await Task.WhenAll(tasks);

        tasks.ForEach(async task => searchResults.AddRange(await (Task<List<SearchResultDTO>>)task));

        return _exchangeRateManager.GetRates(searchResults);
    }
}