using AutoMapper;
using CalorieTracker.Api.Models;
using CalorieTracker.Data.Repository;
using CalorieTracker.Domains;

namespace CalorieTracker.Service;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProducts();
    Task<Product> GetProductById(int id);
    Task<Product> AddProduct(ProductDto productDto);
    Task<Product> DeleteProduct(int id);
    Task<Product> EditProduct(ProductDto productDto, int id);
}

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IManufacturerRepository _manufacturerRepository;
    private readonly IMapper _mapper;



    public ProductService(IProductRepository repository, IManufacturerRepository manufacturerRepository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _manufacturerRepository = manufacturerRepository ?? throw new ArgumentNullException(nameof(manufacturerRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await _repository.GetAll();
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
