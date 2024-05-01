using AutoMapper;
using CalorieTracker.Api.Models;
using CalorieTracker.Data.Repository;
using CalorieTracker.Domains;

namespace CalorieTracker.Service;

public interface IUserService
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<User> GetUserById(int id);
    Task<User> AddUser(UserDto userDto);
    Task<User> DeleteUser(int id);
    Task<User> EditUser(UserDto userDto, int id);
}

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository repository, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await _repository.GetAll();
    }

    public async Task<User> GetUserById(int id)
    {
        return await _repository.GetById(id);
    }

    public async Task<User> AddUser(UserDto userDto)
    {
        var user = _mapper.Map<User>(userDto);
        return await _repository.Add(user);
    }

    public async Task<User> DeleteUser(int id)
    {
        var user = await _repository.GetById(id);
        await _repository.Remove(user);
        return user;
    }

    public async Task<User> EditUser(UserDto userDto, int id)
    {
        var user = await _repository.GetById(id);
        if (user == null) return null;

        user.UserName = userDto.UserName;
        user.Email = userDto.Email;
        user.CurrentWeight = userDto.CurrentWeight;
        user.DesiredWeight = userDto.DesiredWeight;
        user.Height = userDto.Height;
        user.Age = userDto.Age;
        user.Gender = userDto.Gender;
        user.ActivityLevel = userDto.ActivityLevel;

        user.RecommendedCalories = userDto.RecommendedCalories;
        user.RecommendedProtein = userDto.RecommendedProtein;
        user.RecommendedFat = userDto.RecommendedFat;
        user.RecommendedCarbs = userDto.RecommendedCarbs;

        await _repository.Update(id, user);
        return user;
    }
}