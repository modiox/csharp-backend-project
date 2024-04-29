public enum Status {pending, processing, shipped, delivered};
public class CustomerOrder
{
  public required Guid CustomerId { get; set; }
  public required Guid OrderId { get; set; }
  public enum Status { get, set }
  public required string Payment { get; set; }
}