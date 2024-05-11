using api.Dtos;
using api.Dtos.User;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserService
{
    private readonly AppDBContext _dbContext;
    private readonly ILogger<UserService> _logger;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(AppDBContext dbContext, ILogger<UserService> logger, IPasswordHasher<User> passwordHasher, IMapper mapper)
    {

        _passwordHasher = passwordHasher;
        _dbContext = dbContext;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        
        // return await _dbContext.Users.Include(user => user.Orders).ToListAsync();
        var users = await _dbContext.Users.Select(user => _mapper.Map<UserDto>(user)).ToListAsync();
        return  users;
    }

    public async Task<UserDto?> GetUserById(Guid userId)
    {
        // return await _dbContext.Users.Include(u => u.Orders).FirstOrDefaultAsync(u => u.UserID == userId);
         var user = await _dbContext.Users.FindAsync(userId);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
    }

    public async Task<User> CreateUser(UserModel newUser)
    {
            User createUser = new User
            {
                Username = newUser.Username,
                Email = newUser.Email,
                Password = _passwordHasher.HashPassword(null, newUser.Password),
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                CreatedAt = DateTime.UtcNow,
                BirthDate = newUser.BirthDate,
                Address = newUser.Address,
                IsAdmin = newUser.IsAdmin,
                IsBanned = newUser.IsBanned
            };

            _dbContext.Users.Add(createUser);

            await _dbContext.SaveChangesAsync();

            return createUser;
    }

    public async Task<bool> UpdateUser(Guid userId, UserModel updateUser)
    {
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.UserID == userId);
            if (existingUser != null && updateUser != null)
            {
                existingUser.Username = updateUser.Username;
                existingUser.Email = updateUser.Email;
                existingUser.FirstName = updateUser.FirstName;
                existingUser.LastName = updateUser.LastName;
                existingUser.Address = updateUser.Address;
                existingUser.PhoneNumber = updateUser.PhoneNumber;
                existingUser.IsBanned = updateUser.IsBanned;

                await _dbContext.SaveChangesAsync();
                return true; // Return true indicating successful update
            }

            return false; // Return false if either existingUser or updateUser is null
    }

    public async Task<bool> DeleteUser(Guid userId)
    {

        var userToDelete = _dbContext.Users.FirstOrDefault(u => u.UserID == userId);
        if (userToDelete != null)
        {
            _dbContext.Users.Remove(userToDelete);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<UserDto?> LoginUserAsync(LoginDto loginDto)
    {
        var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == loginDto.Email);
        if (user == null)
        {
            return null;
        }

        var result = _passwordHasher.VerifyHashedPassword(user, user.Password, loginDto.Password);
        if (result == PasswordVerificationResult.Failed)
        {
            return null;
        }
        var userDto = new UserDto
        {
            UserID = user.UserID,
            Username = user.Username,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            CreatedAt = user.CreatedAt,
            Address = user.Address,
            IsAdmin = user.IsAdmin,
            IsBanned = user.IsBanned
        };

        return userDto;
    }

}
