using api.Controllers;
using Microsoft.AspNetCore.Mvc;

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
            return ApiResponse.Success(users, "all users are returned successfully");
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
            return ApiResponse.Success(user, "User Returned");
        }
        catch
        {
            return ApiResponse.ServerError("An error occurred while processing the request.");
        }
    }


    [HttpPost]
    public async Task<IActionResult> CreateUser(UserModel newUser)
    {
        var createdUser = await _userService.CreateUser(newUser);
        if (createdUser != null)
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
    public async Task<IActionResult> UpdateUser(Guid userId, UserModel updateUser)
    {
        var user = await _userService.UpdateUser(userId, updateUser);
        if (user)
        {
            return ApiResponse.NotFound("User not exist or you provide an invalid Id");
        }
        return ApiResponse.Updated("User is updated successfully");
    }

    [HttpDelete("{userId}")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        var result = await _userService.DeleteUser(userId);
        if (result)
        {
            return ApiResponse.NotFound("User not exist or you provide an invalid Id");
        }
        return ApiResponse.Deleted("User is deleted successfully");
    }
}
