using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

   [Table("Categories")]
    public class Category
{
    [Key,Required]
    public  Guid CategoryID { get; set; }
    [Required(ErrorMessage = "Name of category is required")]
    [StringLength(50)]
    public required string Name { get; set; } 
    public string Description { get; set; } = string.Empty;
    // public string Slug { get; set; }= string.Empty;
    // public DateTime CreatedAt { get; set; } // ! postgres accept DateTime.UtcNow not DateTime.Now

}

