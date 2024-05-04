
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

   

    public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
    {
        // Retrieve users asynchronously
        var dataList = await _dbContext.Users.ToListAsync();

        // Map data to UserModel
        var users = dataList.Select(row => new UserModel
        {
            UserID = row.UserID,
            Username = row.Username,
            Email = row.Email,
            Password = row.Password,
            FirstName = row.FirstName,
            LastName = row.LastName,
            PhoneNumber = row.PhoneNumber,
            Address = row.Address,
            IsAdmin = row.IsAdmin,
            IsBanned = row.IsBanned,
            BirthDate = row.BirthDate ?? DateTime.MinValue,
        
        }).ToList();

        return users;
    }

    public UserModel GetUserById(Guid userId)
    {
        try
        {
            var userEntity = _dbContext.Users.FirstOrDefault(u => u.UserID == userId);
            if (userEntity != null)
            {
                var userModel = new UserModel
                {
                    UserID = userEntity.UserID,
                    Username = userEntity.Username,
                    Email = userEntity.Email,
                    Password = userEntity.Password,
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    PhoneNumber = userEntity.PhoneNumber,
                    Address = userEntity.Address,
                    IsAdmin = userEntity.IsAdmin,
                    IsBanned = userEntity.IsBanned,
                    BirthDate = userEntity.BirthDate ?? DateTime.MinValue,
    
                };
                return userModel;
            }
            else
            {
                // Handle the case where the user is not found
                return null; 
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while retrieving user with ID: {userId}.");
            throw;
        }
    }



    public bool CreateUser(UserModel newUser, out UserModel createdUser)
    {
        try
        {
            User createUser = new User
            {
                UserID = Guid.NewGuid(),
                Username = newUser.Username,
                Email = newUser.Email,
                Password = newUser.Password,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                CreatedAt = DateTime.UtcNow, // ! postgres accept DateTime.UtcNow not DateTime.Now
            };

            _dbContext.Users.Add(createUser);

            // Save changes to the database
            int result = _dbContext.SaveChanges();

            if (result > 0)
            {
                // Operation successful, return the newly created user
                createdUser = new UserModel
                {
                    UserID = createUser.UserID,
                    Username = createUser.Username,
                    Email = createUser.Email,
                    Password = createUser.Password,
                    FirstName = createUser.FirstName,
                    LastName = createUser.LastName,
                    PhoneNumber = createUser.PhoneNumber,
                    Address = createUser.Address,
                    IsAdmin = createUser.IsAdmin,
                    IsBanned = createUser.IsBanned,
                    BirthDate = createUser.BirthDate ?? DateTime.MinValue,

                };
                return true;
            }
            else
            {
                createdUser = null;
                return false;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating a new user.");
            throw;
        }
    }

    public bool UpdateUser(Guid userId, UserModel updateUser)
    {
        try
        {
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.UserID == userId);
            if (existingUser != null && updateUser != null)
            {
                existingUser.Username = updateUser.Username;
                existingUser.Email = updateUser.Email;
                existingUser.Password = updateUser.Password;
                existingUser.FirstName = updateUser.FirstName;
                existingUser.LastName = updateUser.LastName;

                _dbContext.SaveChanges();
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


    public bool DeleteUser(Guid userId)
    {

        var userToDelete = _dbContext.Users.FirstOrDefault(u => u.UserID == userId);
        if (userToDelete != null)
        {
            _dbContext.Users.Remove(userToDelete);
            _dbContext.SaveChanges();
            return true;
        }
        return false;
    }

   
}
