using AutoMapper;

namespace CalorieTracker.Api.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile() 
        {
            CreateMap<Domains.Product, Models.ProductDto>();
        }
    }
}
