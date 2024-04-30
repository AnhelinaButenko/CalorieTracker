using AutoMapper;
using CalorieTracker.Api.Models;
using CalorieTracker.Data.Repository;
using CalorieTracker.Domains;
using System.ComponentModel.DataAnnotations;

namespace CalorieTracker.Service;

public interface IMealProductService
{
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
        var validationContext = new ValidationContext(mealProductDto, null, null);

        var validationResults = new List<ValidationResult>();

        bool isValid = Validator.TryValidateObject(mealProductDto, validationContext, validationResults, true);

        if (!isValid)
        {
            var errorMessage = string.Join(",", validationResults.Select(r => r.ErrorMessage));
            throw new ArgumentException(errorMessage);
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

        //user.UserName = userDto.UserName;
        //user.Email = userDto.Email;
        //user.CurrentWeight = userDto.CurrentWeight;
        //user.DesiredWeight = userDto.DesiredWeight;
        //user.Height = userDto.Height;
        //user.Age = userDto.Age;
        //user.Gender = userDto.Gender;
        //user.ActivityLevel = userDto.ActivityLevel;

        //user.RecommendedCalories = userDto.RecommendedCalories;
        //user.RecommendedProtein = userDto.RecommendedProtein;
        //user.RecommendedFat = userDto.RecommendedFat;
        //user.RecommendedCarbs = userDto.RecommendedCarbs;

        await _repository.Update(id, mealProduct);
        return mealProduct;
    }
}
