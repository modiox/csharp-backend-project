using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Categories")]
public class Category
{
    public Guid CategoryID { get; set; }

    public required string Name { get; set; }
    public string Description { get; set; } 
     public List<Product>? Products { get; set; }
}

 // public string Slug { get; set; }= string.Empty;
    // public DateTime CreatedAt { get; set; } 