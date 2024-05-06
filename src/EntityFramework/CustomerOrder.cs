using System.ComponentModel.DataAnnotations.Schema;


namespace EntityFramework
{
    public enum Status { pending, processing, shipped, delivered };
    public enum Payment { CreditCard, ApplePay, Visa, Cash, PayPal };

    [Table("CustomerOrder")]
    public class CustomerOrder
    {
        public required Guid OrderId { get; set; }

        public required OrderStatus Status { get; set; } = OrderStatus.Pending;

        public required PaymentMethod Payment { get; set; } = PaymentMethod.CreditCard;

        public required double Amount { get; set; }

        public Guid UserId { get; set; }

        public virtual User? User { get; set; }

        public Guid ProductId { get; set; }

        public virtual List<Product>? Product { get; set; }

    }
}