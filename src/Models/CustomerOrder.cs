public enum Status { pending, processing, shipped, delivered };

public class CustomerOrder
{

  public Guid OrderId { get; set; } = Guid.NewGuid();

  public required Guid UserId { get; set; }

  public Status OrderStatus { get; set; } = Status.pending;

  public DateTime CreatedAt { get; set; } = DateTime.Now;

  public required string Payment { get; set; }

}