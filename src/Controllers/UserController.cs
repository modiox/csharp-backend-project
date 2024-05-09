using System.Security.Claims;
using api.Controllers;
using api.Dtos.User;
using api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly AuthService _authService;
    private readonly ILogger<UserController> _logger;

    public UserController(UserService userService, ILogger<UserController> logger, AuthService authService)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _logger = logger;
        _authService = authService;
    }

    [Authorize(Roles = "Admin")]
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
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                return ApiResponse.UnAuthorized("User Id is missing from token");
            }
            if (!Guid.TryParse(userIdString, out userId))
            {
                return ApiResponse.BadRequest("Invalid User Id");
            }
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
            return ApiResponse.Created("User is created successfully");
        }
        else
        {
            return ApiResponse.ServerError("An error occurred while creating the user.");
        }
    }


    [HttpPut("profile")]
    public async Task<IActionResult> UpdateUser(Guid userId, UserModel updateUser)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdString))
        {
            return ApiResponse.UnAuthorized("User Id is missing from token");
        }
        if (!Guid.TryParse(userIdString, out userId))
        {
            return ApiResponse.BadRequest("Invalid User Id");
        }
        var user = await _userService.UpdateUser(userId, updateUser);
        if (user)
        {
            return ApiResponse.NotFound("User not exist or you provide an invalid Id");
        }
        return ApiResponse.Updated("User is updated successfully");
    }

    [HttpDelete("profile/delete")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdString))
        {
            return ApiResponse.UnAuthorized("User Id is missing from token");
        }
        if (!Guid.TryParse(userIdString, out userId))
        {
            return ApiResponse.BadRequest("Invalid User Id");
        }
        var result = await _userService.DeleteUser(userId);
        if (result)
        {
            return ApiResponse.NotFound("User not exist or you provide an invalid Id");
        }
        return ApiResponse.Deleted("User is deleted successfully");
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return ApiResponse.BadRequest("Invalid User Data");
        }
        var loggedInUser = await _userService.LoginUserAsync(loginDto);
        if (loggedInUser == null)
        {
            return ApiResponse.UnAuthorized("Invalid credentials");
        }

        var token = _authService.GenerateJwt(loggedInUser);
        return ApiResponse.Success(new { token, loggedInUser }, "User Logged In successfully");

    }
}
