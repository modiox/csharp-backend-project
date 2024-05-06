
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Logging;

public class UserService
{
    private readonly AppDBContext _dbContext;
    private readonly ILogger<UserService> _logger;

    public UserService(AppDBContext dbContext, ILogger<UserService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _dbContext.Users.Include(user => user.Orders).ToListAsync();

        // With product we can uncomment after product finish and remove the line above
        // return await _dbContext.Users.Include(u => u.Orders).ThenInclude(o => o.Products).ToListAsync();
    }

    public async Task<User?> GetUserById(Guid userId)
    {
        return await _dbContext.Users.Include(u => u.Orders).FirstOrDefaultAsync(u => u.UserID == userId);
    }

    public async Task<User> CreateUser(UserModel newUser)
    {
        try
        {
            User createUser = new User
            {
                Username = newUser.Username,
                Email = newUser.Email,
                Password = newUser.Password,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                CreatedAt = DateTime.UtcNow,
                Address = newUser.Address,
                IsAdmin = newUser.IsAdmin,
                IsBanned = false
            };

            _dbContext.Users.Add(createUser);

            await _dbContext.SaveChangesAsync();

            return createUser;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating a new user.");
            throw;
        }
    }

    public async Task<bool> UpdateUser(Guid userId, UserModel updateUser)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while updating user with ID: {userId}.");
            return false; // Return false if an exception occurs during the update operation
        }
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


}
