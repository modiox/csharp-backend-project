using System.ComponentModel.DataAnnotations;
public class CategoryModel
{
    [Required]
    public Guid CategoryID { get; set; }
    [Required(ErrorMessage = "Name of category is required")]
    [StringLength(50)]
    public required string Name { get; set; } 
    public string Description { get; set; } = string.Empty;
    // public string Slug { get; set; }= string.Empty;
    // public DateTime CreatedAt { get; set; } // ! postgres accept DateTime.UtcNow not DateTime.Now

}
