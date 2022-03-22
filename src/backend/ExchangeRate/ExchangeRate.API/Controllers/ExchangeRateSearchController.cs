using System.Net;
using Microsoft.AspNetCore.Mvc;
using ExchangeRate.Interface.Service.UseCase;
using ExchangeRate.Model.Binding;
using ExchangeRate.Model.Validator;
using ExchangeRate.Model.HttpException;

namespace ExchangeRate.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Consumes("application/json"), Produces("application/json")]
public class ExchangeRateSearchController : ControllerBase
{
    private readonly IExchangeRateService _exchangeRateService;

    public ExchangeRateSearchController(IExchangeRateService exchangeRateService)
    {
        _exchangeRateService = exchangeRateService;
    }

    [HttpPost()]
    public async Task<IActionResult> GetExchangeRates([FromBody] ExchangeRateBindingModel model)
    {
        var validationResult = await new ExchangeRateValidator().ValidateAsync(model);

        if (!validationResult.IsValid)
        {
            throw new CustomHttpException(HttpStatusCode.BadRequest, validationResult.Errors);
        }

        var result = await _exchangeRateService.SearchAsync(model);

        return Ok(result);
    }
}
