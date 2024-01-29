using AutoMapper;
using CalorieTracker.Api.Extensions;

namespace CalorieTracker.Api.Profiles;

public class MappingProfile
    : Profile
{
    public MappingProfile() 
    {
        CreateMap<Domains.Product, Models.ProductDto>()
            .ForMember(x => x.ManufacturerName, y => y.MapFrom(x => x.MapManufacturerName()))
            .ReverseMap();

        CreateMap<Domains.User, Models.UserDto>().ReverseMap();

        CreateMap<Domains.Manufacturer, Models.ManufacturerDto>().ReverseMap();
    }   
}
