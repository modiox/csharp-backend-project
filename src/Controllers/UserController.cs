using api.Controllers;
using Microsoft.AspNetCore.Mvc;

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
            return ApiResponse.Success(users, "Users Returned");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving all users.");
            return ApiResponse.ServerError("An error occurred while processing the request.");
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
                return ApiResponse.NotFound("User not exist or you provide an invalid Id");
            }
            return Ok(user);
        }
        catch
        {
            return ApiResponse.ServerError("An error occurred while processing the request.");
        }
    }


    [HttpPost]
    public IActionResult CreateUser(UserModel newUser)
    {
        UserModel createdUser;
        var success = _userService.CreateUser(newUser, out createdUser);
        if (success)
        {
            // return CreatedAtAction(nameof(GetUser), new { userId = createdUser.UserID }, createdUser);
            return ApiResponse.Created("User is created successfully");
        }
        else
        {
            // return StatusCode(500, "An error occurred while creating the user.");
            return ApiResponse.ServerError("An error occurred while creating the user.");
        }
    }


    [HttpPut("{userId}")]
    public IActionResult UpdateUser(Guid userId, UserModel updateUser)
    {
        var user = _userService.UpdateUser(userId, updateUser);
        if (user == null)
        {
            return ApiResponse.NotFound("User not exist or you provide an invalid Id");
        }
        return ApiResponse.Updated("User is updated successfully");
    }

    [HttpDelete("{userId}")]
    public IActionResult DeleteUser(Guid userId)
    {
        var result = _userService.DeleteUser(userId);
        if (!result)
        {
            return ApiResponse.NotFound("User not exist or you provide an invalid Id");
        }
        return ApiResponse.Deleted("User is deleted successfully");
    }
}
