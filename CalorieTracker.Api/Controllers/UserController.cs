using AutoMapper;
using CalorieTracker.Api.Models;
using CalorieTracker.Service;
using Microsoft.AspNetCore.Mvc;

namespace CalorieTracker.Api.Controllers;

[Route("api/[Controller]")]
[ApiController]
[Produces("application/json")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService,
        IMapper mapper)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> Get()
    {
        var users = await _userService.GetAllUsers();

        return Ok(_mapper.Map<IEnumerable<UserDto>>(users));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetId(int id)
    {
        var user = await _userService.GetUserById(id);

        return Ok(_mapper.Map<UserDto>(user));
    }

    [HttpPost]
    public async Task<ActionResult> AddUser([FromBody] UserDto userDto)
    {
        var user = await _userService.AddUser(userDto);

        return Ok(_mapper.Map<UserDto>(user));
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        var user = await _userService.DeleteUser(id);

        return Ok(_mapper.Map<UserDto>(user));
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> EditUser([FromBody] UserDto userDto, int id)
    {
        var user = await _userService.EditUser(userDto, id);

        return Ok(_mapper.Map<UserDto>(user));
    }
}

