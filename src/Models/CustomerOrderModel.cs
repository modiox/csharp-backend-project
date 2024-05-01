using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

public enum OrderStatus { Creating = 0, Pending = 1, Processing = 2, Shipped = 3, Delivered = 4 };
public class CustomerOrderModel
{
  [Required(ErrorMessage = "Order Id is required")]
  public required Guid OrderId { get; set; }

  [Required(ErrorMessage = "Order Status is required")]
  public required OrderStatus Status { get; set; }

  [Required(ErrorMessage = "Payment method is required")]
  public required string Payment { get; set; }

  [Required(ErrorMessage = "Product Id is required")]
  public required Guid ProductId { get; set; }

  // public virtual List<ProductModel>? Product { get; set; }

  [Required(ErrorMessage = "User Id is required")]
  public Guid UserId { get; set; }

  // public virtual UserModel? User { get; set; }
}