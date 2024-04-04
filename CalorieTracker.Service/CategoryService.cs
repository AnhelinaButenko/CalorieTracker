using AutoMapper;
using CalorieTracker.Api.Models;
using CalorieTracker.Data.Repository;
using CalorieTracker.Domains;
using System.Data;

namespace CalorieTracker.Service;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllFilteredCategories(string? searchStr, string filter = "all");
    Task<CategoryDto> GetCategoryById(int id);
    Task<CategoryDto> GetCategoryByName(string name);
    Task<CategoryDto> AddCategory(CategoryDto categoryDto);
    Task<CategoryDto> EditCategory(int id, CategoryDto categoryDto);
    Task<CategoryDto> DeleteCategory(int id);
}

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CategoryService(
        ICategoryRepository categoryRepository,
        IProductRepository productRepository, 
        IMapper mapper)
    {
        _repository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<CategoryDto>> GetAllFilteredCategories(string? searchStr, string filter = "all")
    {
        var categories = await _repository.GetAllFiltered(searchStr, filter);
        return _mapper.Map<IEnumerable<CategoryDto>>(categories);
    }

    public async Task<CategoryDto> GetCategoryById(int id)
    {
        var category = await _repository.GetById(id);
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> AddCategory(CategoryDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);
        await ProcessCategories(category, categoryDto.ProductsId);
        await _repository.Add(category);
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> GetCategoryByName(string name)
    {
       var category = await _repository.GetByName(name);
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> EditCategory(int id, CategoryDto categoryDto)
    {
        var category = await _repository.GetById(id);
        category.Name = categoryDto.Name;
        await ProcessCategories(category, categoryDto.ProductsId);
        await _repository.Update(id, category);
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> DeleteCategory(int id)
    {
        var category = await _repository.GetById(id);
        await _repository.Remove(category);
        return _mapper.Map<CategoryDto>(category);
    }

    private async Task ProcessCategories(Category category, List<int> productIds)
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
                category.Products.Add(product);
            }
        }
    }
}
