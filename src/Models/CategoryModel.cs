using System.ComponentModel.DataAnnotations;
public class CategoryModel
{
    public Guid CategoryID { get; set; }
    public required string Name { get; set; } 
    public string Description { get; set; } 
    public List<ProductModel>? Products { get; set; } 

}
 // public string Slug { get; set; }= string.Empty;
    // public DateTime CreatedAt { get; set; } 