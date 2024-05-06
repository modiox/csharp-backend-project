using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;


[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly ILogger<UserController> _logger;
    private AppDBContext _appDbContext; 

    public UserController(UserService userService, AppDBContext appDBContext, ILogger<UserController> logger)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _logger = logger; 
        _appDbContext = appDBContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving all users.");
            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
        }
    }

    [HttpGet("{userId}")]
   public async Task<IActionResult> GetUser(Guid userId)
{
    try
    {
        var user = await _userService.GetUserById(userId);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }
    catch 
    {
       
        return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
    }
}


    [HttpPost]
    public async Task<IActionResult> CreateUser(UserModel newUser)
    {
        var createdUser = await _userService.CreateUser(newUser);
        if (createdUser != null)
        {
            return CreatedAtAction(nameof(GetUser), new { userId = createdUser.UserID }, createdUser);
        }
        else
        {
            return StatusCode(500, "An error occurred while creating the user.");
        }
    }


    [HttpPut("{userId}")]
    public async Task<IActionResult> UpdateUser(Guid userId, UserModel updateUser)
    {
        var user = await _userService.UpdateUser(userId, updateUser);
        if (user)
        {
            return Ok(user);
        }
            return NotFound();
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        var result = await _userService.DeleteUser(userId);
        if (result)
        {
            return NoContent();
        }
            return NotFound();
    }
}
