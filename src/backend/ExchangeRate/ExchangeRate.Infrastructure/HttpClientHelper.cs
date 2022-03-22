using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json.Linq;
using ExchangeRate.Interface.Infrastructure;
using ExchangeRate.Model.Integration;
using ExchangeRate.Model.HttpException;

namespace ExchangeRate.Infrastructure;

public class HttpClientHelper : IHttpClientHelper
{
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _apiBaseUrl;

    public HttpClientHelper(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
        _apiBaseUrl= _configuration.GetSection("App:BaseUrl")?.Value;
    }

    public async Task<List<SearchResultDTO>> GetExchangeRateAsync(string date, RequestModel model = null)
    {
        List<SearchResultDTO> searchResult = null;

        var url = QueryHelpers.AddQueryString($"{_apiBaseUrl}/{date}", GetQueryParameters(model));
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        var httpClient = _httpClientFactory.CreateClient();

        httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

        var response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            throw new CustomHttpException(System.Net.HttpStatusCode.BadRequest, "Something went wrong.");
        }

        var responseString = await response.Content.ReadAsStringAsync();

        searchResult = MapToResult(responseString);

        return searchResult;
    }

    private List<SearchResultDTO> MapToResult(string responseString)
    {
        var searchResult = new List<SearchResultDTO>();

        dynamic result = JObject.Parse(responseString);

        foreach (var item in result.rates)
        {
            foreach (var prop in item)
            {
                searchResult.Add(new SearchResultDTO
                    {
                        Date = result.date,
                        BaseCurrency = result["base"],
                        Currency = item.Name,
                        CurrencyRate = (float)prop.Value
                    });
            }
        }

        return searchResult;
    }

    private Dictionary<string, string> GetQueryParameters(RequestModel model)
    {
        Dictionary<string, string> queryParametersDictionary = JToken.FromObject(model, new Newtonsoft.Json.JsonSerializer
                {
                    NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore
                })
            .Value<JObject>()
            .Properties()
            .ToDictionary(k => k.Name.ToLower(), v => $"{v.Value}");

        return queryParametersDictionary;
    }
}
