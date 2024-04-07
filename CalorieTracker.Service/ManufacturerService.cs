using AutoMapper;
using CalorieTracker.Api.Models;
using CalorieTracker.Data.Repository;
using CalorieTracker.Domains;

namespace CalorieTracker.Service;

public interface IManufacturerService
{
    Task<IEnumerable<ManufacturerDto>> GetAllFilteredManufacturers(string? searchStr, string filter = "all");
    Task<ManufacturerDto> GetManufacturerById(int id);
    Task<ManufacturerDto> GetManufacturerByName(string name);
    Task<ManufacturerDto> AddManufacturer(ManufacturerDto manufacturerDto);
    Task<ManufacturerDto> EditManufacturer(int id, ManufacturerDto manufacturerDto);
    Task<ManufacturerDto> DeleteManufacturer(int id);
}
public  class ManufacturerService : IManufacturerService
{
    private readonly IManufacturerRepository _repository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public ManufacturerService(
        IManufacturerRepository repository,
        IMapper mapper,
        IProductRepository productRepository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<IEnumerable<ManufacturerDto>> GetAllFilteredManufacturers(string? searchStr, string filter = "all")
    {
        var manufacturers = await _repository.GetAllFiltered(searchStr, filter);
        return _mapper.Map<IEnumerable<ManufacturerDto>>(manufacturers);
    }

    public async Task<ManufacturerDto> GetManufacturerById(int id)
    {
        var manufacturer = await _repository.GetById(id);
        return _mapper.Map<ManufacturerDto>(manufacturer);
    }

    public async Task<ManufacturerDto> AddManufacturer(ManufacturerDto manufacturerDto)
    {
        var manufacturer = _mapper.Map<Manufacturer>(manufacturerDto);
        await ProcessProducts(manufacturer, manufacturerDto.ProductsId);
        await _repository.Add(manufacturer);
        return _mapper.Map<ManufacturerDto>(manufacturer);
    }

    public async Task<ManufacturerDto> EditManufacturer(int id, ManufacturerDto manufacturerDto)
    {
        var manufacturer = await _repository.GetById(id);
        manufacturer.Name = manufacturerDto.Name;
        await ProcessProducts(manufacturer, manufacturerDto.ProductsId);
        await _repository.Update(id, manufacturer);
        return _mapper.Map<ManufacturerDto>(manufacturer);
    }

    public async Task<ManufacturerDto> GetManufacturerByName(string name)
    {
        var manufacturer = await _repository.GetByName(name);
        return _mapper.Map<ManufacturerDto>(manufacturer);
    }

    public async Task<ManufacturerDto> DeleteManufacturer(int id)
    {
        var manufacturer = await _repository.GetById(id);
        await _repository.Remove(manufacturer);
        return _mapper.Map<ManufacturerDto>(manufacturer);
    }

    private async Task ProcessProducts(Manufacturer manufacturer, List<int>? productIds)
    {
        if (productIds != null)
        {
            foreach (var productId in productIds)
            {
                Product product = await _productRepository.GetById(productId);
                if (product == null)
                {
                    throw new ArgumentException("Invalid product ID.");
                }
                manufacturer.Products.Add(product);
            }
        }
    }
}