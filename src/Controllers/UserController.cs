public class UserService
{
    // users api 
    public static List<User> _users = new List<User>() {
    new User{
        UserID = Guid.Parse("75424b9b-cbd4-49b9-901b-056dd1c6a020"),
        Username = "Johnx",
        Email = "john@example.com",
        Password = "password123",
        FirstName = "John",
        LastName = "Doe",
        PhoneNumber = "+133 670 683",
        Address = "123 Main St",
        IsAdmin = false,
        IsBanned = false,
        BirthDate = new DateTime(1999, 8, 29),
        CreatedAt = DateTime.Now
    },
    new User{
        UserID = Guid.Parse("24508f7e-94ec-4f0b-b8d6-e8e16a9a3b29"),
        Username = "AliceSm",
        Email = "alice@example.com",
        Password = "password3354",
        FirstName = "Alice",
        LastName = "Smith",
        PhoneNumber = "+133 670 968",
        Address = "155 Oak St",
        IsAdmin = false,
        IsBanned = false,
        BirthDate = new DateTime(1980, 7, 19),
        CreatedAt = DateTime.Now
    },
    new User{
        UserID = Guid.Parse("87e5c4f3-d3e5-4e16-88b5-809b2b08b773"),
        Username = "BobJ",
        Email = "bob@example.com",
        Password = "password789",
        FirstName = "Bob", 
        LastName = "Johnson",
        PhoneNumber = "+144 565 476",
        Address = "789 Oak St",
        IsAdmin = false,
        IsBanned = false,
        BirthDate =  new DateTime(1986, 10, 16),
        CreatedAt = DateTime.Now

        
    }
};

    public IEnumerable<User> GetAllUsersService()
    {
        return _users;
    }
    public User? GetUserById(Guid userId)
    {
        return _users.Find(user => user.UserID == userId);
    }
    public User CreateUserService(User newUser)
    {
        newUser.UserID = Guid.NewGuid();
        newUser.CreatedAt = DateTime.Now;
        _users.Add(newUser); // store this user in our database
        return newUser;
    }
    public User UpdateUserService(Guid userId, User updateUser)
    {
        var existingUser = _users.FirstOrDefault(u => u.UserID == userId);
        if (existingUser != null)
        {
            existingUser.Username = updateUser.Username;
            existingUser.Email = updateUser.Email;
            existingUser.Password = updateUser.Password;
            existingUser.FirstName = updateUser.FirstName;
            existingUser.LastName = updateUser.LastName;
            existingUser.PhoneNumber = updateUser.PhoneNumber;
            existingUser.Address = updateUser.Address;
            existingUser.IsAdmin = updateUser.IsAdmin;
            existingUser.IsBanned = updateUser.IsBanned;
        }
        return existingUser;
    }
    public bool DeleteUserService(Guid userId)
    {
        var userToRemove = _users.FirstOrDefault(u => u.UserID == userId);
        if (userToRemove != null)
        {
            _users.Remove(userToRemove);
            return true;
        }
        return false;
    }

}