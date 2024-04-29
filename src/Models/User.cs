using System.ComponentModel.DataAnnotations;

public class User
{
    public int UserID { get; set; }

    [Required(ErrorMessage = "Username is required")]
    [StringLength(50)]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [StringLength(32)]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "First name is required")]
    [StringLength(20)]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(20)]
    public string LastName { get; set; } = string.Empty;

    [StringLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;

    [StringLength(255)]
    public string Address { get; set; } = string.Empty;
    public bool IsAdmin { get; set; }
    public bool IsBanned { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime CreatedAt { get; set; }
}
