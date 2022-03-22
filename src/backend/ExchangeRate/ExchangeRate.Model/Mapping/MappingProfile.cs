using AutoMapper;
using ExchangeRate.Model.Binding;
using ExchangeRate.Model.Integration;

namespace ExchangeRate.Model.Mapping;

public class MappingProfile : Profile
{    
    public MappingProfile()
    {
        SearchMappingProfile();
    }

    private void SearchMappingProfile()
    {
        CreateMap<ExchangeRateBindingModel, RequestModel>()
            .ForMember(dest => dest.Base, opt => opt.MapFrom(src => src.BaseCurrency))
            .ForMember(dest => dest.Symbols, opt => opt.MapFrom(src => string.Join(",", src.TargetCurrencies)))
            .ForMember(dest => dest.Format, opt => opt.MapFrom(src => "json"))
        ;
    }
}
