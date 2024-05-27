using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

public class ProductModel 
{
    public Guid ProductID { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "Product name is required")]
    [StringLength(50)]
    public required string ProductName {get;set;}

   public string? ImgUrl { get; set; }

    public string Description {get;set;}=string.Empty;

    [Required(ErrorMessage = "Quantity is required")]
    public required int Quantity {get;set;}

    [Required(ErrorMessage = "Price is required")]
    public required decimal Price {get;set;}

    [Required(ErrorMessage = "CategoryId is required")]
    public required Guid CategoryID {get;set;}
    public CategoryModel? Category { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    internal object ToListAsync()
    {
        throw new NotImplementedException();
    }
}