public class OrderDetail
{
  public required Guid OrderDetailsId { get; set; }
  public required Guid ProductId { get; set; }
  public required int ProductQuantity { get; set; }
  public required string Payment { get; set; }
}