using AutoMapper;
using CalorieTracker.Api.Models;
using CalorieTracker.Data.Repository;
using CalorieTracker.Domains;
using System.Linq.Expressions;

namespace CalorieTracker.Service;

public interface IMealProductService
{
    Task<IEnumerable<MealProductDto>> GetAllMealProducts(Expression<Func<MealProduct, bool>> filter);
    Task<IEnumerable<MealProduct>> GetAllMealProduct();
    Task<MealProduct> GetMealProductById(int id);
    Task<MealProduct> AddMealProduct(MealProductDto mealProductDto);
    Task<MealProduct> DeleteMealProduct(int id);
    Task<MealProduct> EditMealProduct(MealProductDto mealProductDto, int id);
}

public class MealProductService : IMealProductService
{
    private readonly IMealProductRepository _repository;
    private readonly IMapper _mapper;

    public MealProductService(IMealProductRepository repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    private void ValidateMealProductDto(MealProductDto mealProductDto)
    {
        if (mealProductDto == null)
        {
            throw new ArgumentNullException(nameof(mealProductDto), "MealProductDto can`t be null");
        }

        if (mealProductDto.ProductWeightGr <= 0 || mealProductDto.ProductId <= 0 || mealProductDto.DailyFoodDairyId <= 0)
        {
            throw new ArgumentException("Product weight, Product ID, Daily food diary ID, must be greater than zero.");
        }

        if (string.IsNullOrWhiteSpace(mealProductDto.Name))
        {
            throw new ArgumentException("Meal product name cannot be empty or null.", nameof(mealProductDto.Name));
        }
    }

    public async Task<IEnumerable<MealProduct>> GetAllMealProduct()
    {
        return await _repository.GetAll();
    }

    public async Task<MealProduct> GetMealProductById(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task<MealProduct> AddMealProduct(MealProductDto mealProductDto)
    {
        ValidateMealProductDto(mealProductDto);

        var mealProduct = _mapper.Map<MealProduct>(mealProductDto);
        return await _repository.Add(mealProduct);
    }

    public async Task<MealProduct> DeleteMealProduct(int id)
    {
        var mealProduct = await _repository.GetById(id);
        await _repository.Remove(mealProduct);
        return mealProduct;
    }

    public async Task<MealProduct> EditMealProduct(MealProductDto mealProductDto, int id)
    {
        ValidateMealProductDto(mealProductDto);

        var mealProduct = await _repository.GetById(id);
        if (mealProduct == null) return null;

        mealProduct.GramsConsumed = mealProductDto.ProductWeightGr;

        await _repository.Update(id, mealProduct);
        return mealProduct;
    }

    public async Task<IEnumerable<MealProductDto>> GetAllMealProducts(Expression<Func<MealProduct, bool>> filter)
    {
        var mealProducts = await _repository.GetAll(filter);
        return _mapper.Map<IEnumerable<MealProductDto>>(mealProducts);
    }
}
