using System.ComponentModel.DataAnnotations;

public class UserModel 
{
    public Guid UserID { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "Username is required")][StringLength(50)] 
    public required string Username { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [StringLength(50, ErrorMessage = "Must be between 5 and 50 characters", MinimumLength = 5)]
    [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Must be a valid email")]
    public required string Email { get; set; }


    [Required(ErrorMessage = "Password is required")]
    [StringLength(32, ErrorMessage = "Password must be between 8 and 32 characters", MinimumLength = 8)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,32}$",ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character")]
    [DataType(DataType.Password)]

    public required string Password { get; set; }

    [Required(ErrorMessage = "First name is required")]
    [StringLength(20)]
    public required string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(20)]
    public required string LastName { get; set; } 

    [StringLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;

    [StringLength(255)]
    public string Address { get; set; } = string.Empty;
    public bool IsAdmin { get; set; } = false; 
    public bool IsBanned { get; set; } = false; 
    public DateTime BirthDate { get; set; }
   // public DateTime CreatedAt { get; set; } = DateTime.Now;
}
