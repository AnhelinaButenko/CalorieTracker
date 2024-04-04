using AutoMapper;
using CalorieTracker.Api.Models;
using CalorieTracker.Data.Repository;
using CalorieTracker.Domains;

namespace CalorieTracker.Service;

public interface IProductService
{
    Task<IEnumerable<ProductDto>> GetAllFilteredProducts(string? searchStr, string filter = "all");
    Task<Product> GetProductById(int id);
    Task<Product> AddProduct(ProductDto productDto);
    Task<Product> DeleteProduct(int id);
    Task<Product> EditProduct(ProductDto productDto, int id);
}

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<ProductDto>> GetAllFilteredProducts(string? searchStr, string filter = "all")
    {
        var products = await _repository.GetAllFiltered(searchStr, filter);
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }

    public async Task<Product> AddProduct(ProductDto productDto)
    {
        Product product = _mapper.Map<Product>(productDto);
        await _repository.Add(product);
        return product;
    }

    public async Task<Product> DeleteProduct(int id)
    {
        var product = await _repository.GetById(id);
        await _repository.Remove(product);
        return product;
    }

    public async Task<Product> EditProduct(ProductDto productDto, int id)
    {
        Product product = await _repository.GetById(id);
        product.Name = productDto.Name;
        product.CaloriePer100g = productDto.CaloriePer100g;
        product.ProteinPer100g = productDto.ProteinPer100g;
        product.FatPer100g = productDto.FatPer100g;
        product.CarbohydratePer100g = productDto.CarbohydratePer100g;
        await _repository.Update(id, product);
        return product;
    }

    public async Task<Product> GetProductById(int id)
    {
        return await _repository.GetById(id);
    }
}
