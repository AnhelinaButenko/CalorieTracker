using AutoMapper;

namespace CalorieTracker.Api.Profiles;

public class MappingProfile
    : Profile
{
    public MappingProfile() 
    {
        CreateMap<Domains.Product, Models.ProductDto>().ReverseMap();

        CreateMap<Domains.User, Models.UserDto>().ReverseMap();

        CreateMap<Domains.Manufacturer, Models.ManufacturerDto>().ReverseMap();
    }
}
