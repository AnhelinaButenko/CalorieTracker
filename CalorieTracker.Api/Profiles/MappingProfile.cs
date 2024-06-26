﻿using AutoMapper;
using CalorieTracker.Api.Extensions;

namespace CalorieTracker.Api.Profiles;

//This C# code defines a mapping profile using AutoMapper, a library that simplifies object-to-object
//mapping in .NET applications

// the MappingProfile class sets up AutoMapper mappings, making it easier
//to transform objects between domain entities and their corresponding DTOs in a.NET application.
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

        CreateMap<Domains.Category, Models.CategoryDto>().ReverseMap();

        CreateMap<Domains.MealProduct, Models.MealProductDto>().ReverseMap();

        CreateMap<Domains.DailyForDay, Models.DailyForDayUserDto>().ReverseMap();
    }
}
