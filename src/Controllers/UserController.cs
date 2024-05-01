using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;


[ApiController]
[Route("api/[controller]")]
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
   public IActionResult GetUser(Guid userId)
{
    try
    {
        var user = _userService.GetUserById(userId);
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
    public IActionResult CreateUser(UserModel newUser)
    {
        UserModel createdUser;
        var success = _userService.CreateUser(newUser, out createdUser);
        if (success)
        {
            return CreatedAtAction(nameof(GetUser), new { userId = createdUser.UserID }, createdUser);
        }
        else
        {
            return StatusCode(500, "An error occurred while creating the user.");
        }
    }


    [HttpPut("{userId}")]
    public IActionResult UpdateUser(Guid userId, UserModel updateUser)
    {
        var user = _userService.UpdateUser(userId, updateUser);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpDelete("{userId}")]
    public IActionResult DeleteUser(Guid userId)
    {
        var result = _userService.DeleteUser(userId);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}
