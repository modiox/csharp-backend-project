using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.EntityFramework
{
    [Table("CustomerOrder")]
    public class CustomerOrder
    {
        [Key, Required(ErrorMessage = "Order Id is required")]
        public required Guid OrderId { get; set; }

        [Required(ErrorMessage = "Order Status is required")]
        public required OrderStatus Status { get; set; } = OrderStatus.Pending;

        [Required(ErrorMessage = "Payment method is required")]
        public required string Payment { get; set; }

        [Required(ErrorMessage = "User Id is required")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Product Id is required")]
        public Guid ProductId { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        // [ForeignKey("UserId")]
        // public virtual List<Product>? Product { get; set; }

        
    }
}