using System.ComponentModel.DataAnnotations;

public class CategoryModel
{
    public Guid CategoryID { get; set; }
    public required string Name { get; set; } 
    public string Description { get; set; } = string.Empty;
    public string? ImgUrl { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public List<ProductModel> Products { get; set; } = new List<ProductModel>();
}