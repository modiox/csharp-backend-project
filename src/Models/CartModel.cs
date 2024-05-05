

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class CartModel
{

    [Required(ErrorMessage = "User Id is required")]
    public Guid ProductID { get; set; }
    [Required(ErrorMessage = "User Id is required")]
    public Guid UserID { get; set; }
    public ProductModel Product { get; set; }
    public UserModel User { get; set; }


}