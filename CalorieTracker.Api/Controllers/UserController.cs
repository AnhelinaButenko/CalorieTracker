using AutoMapper;
using CalorieTracker.Api.Models;
using CalorieTracker.Data.Repository;
using CalorieTracker.Domains;
using Microsoft.AspNetCore.Mvc;

namespace CalorieTracker.Api.Controllers;

[Route("api/[Controller]")]
[ApiController]
[Produces("application/json")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public UserController(IUserRepository repository,
        IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> Get()
    {
        List<User> user = await _repository.GetAll();

        return Ok(_mapper.Map<IEnumerable<UserDto>>(user));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetId(int id)
    {
        User user = await _repository.GetById(id);

        return Ok(_mapper.Map<UserDto>(user));
    }

    [HttpPost]
    public async Task<ActionResult> AddUser([FromBody] UserDto userDto)
    {
        User user = _mapper.Map<User>(userDto);

        await _repository.Add(user);

        UserDto userDTO = _mapper.Map<UserDto>(user);

        return Ok(_mapper.Map<ProductDto>(userDTO));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        User user = await _repository.GetById(id);

        await _repository.Remove(user);

        return Ok(_mapper.Map<UserDto>(user));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> EditUser([FromBody] UserDto userDto, int id)
    {
        User user = await _repository.GetById(id);

        user.UserName = userDto.Name;
        user.CurrentWeight = userDto.CurrentWeight;
        user.DesiredWeight = userDto.DesiredWeight;

        await _repository.Update(id, user);

        return Ok(_mapper.Map<UserDto>(user));
    }
}
