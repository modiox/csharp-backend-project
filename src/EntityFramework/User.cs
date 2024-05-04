using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


[Table("Users")]
public class User
{

    [Key, Required] //userid is the primary key 
    public Guid UserID { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "Username is required")]
    [StringLength(50)]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [StringLength(100)]
    public required string Email { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [StringLength(32)]
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
    public DateTime? BirthDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // ! postgres accept DateTime.UtcNow not DateTime.Now
}
