using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.EntityFramework
{
    public enum Status { pending, processing, shipped, delivered };

    public enum PaymentMethod { CreditCard, ApplePay, Visa, Cash, PayPal };

    public record class OrderPayment(
        [Required(ErrorMessage = "Payment Method is required!")]
        PaymentMethod Method,

        [Required(ErrorMessage = "Amount is Required!")]
        [Range(0, double.MaxValue, ErrorMessage ="The amount should be more than 0!")]
        double Amount
    );

    [Table("CustomerOrders")]
    public class CustomerOrder
    {
        [Key]
        public Guid OrderId { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "User Id is required!")]
        public required Guid UserId { get; set; }

        public virtual User? User { get; set; }

        public Status OrderStatus { get; set; } = Status.pending;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public required OrderPayment Payment { get; set; }

    }
}