using FluentValidation;
using ExchangeRate.Model.Binding;
using System.Globalization;

namespace ExchangeRate.Model.Validator;

public class ExchangeRateValidator : AbstractValidator<ExchangeRateBindingModel>
{
    public ExchangeRateValidator()
    {
        RuleFor(m => m.BaseCurrency)
            .NotEmpty()
            .MinimumLength(3);

        RuleForEach(m => m.TargetCurrencies)
            .NotEmpty()
            .MinimumLength(3);

        RuleFor(m => m.Dates)
            .NotEmpty()
            .Must(IsValidDate)
            .WithMessage("One or more dates provided are invalid.");
    }

    private bool IsValidDate(ICollection<string> dates)
    {
        return dates.Any(date => !(ParseDate(date) == false));
    }

    private bool ParseDate(string date)
    {
        var result = false;

        if (DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime output))
        {
            result = true;
        }
        
        return result;
    }
}