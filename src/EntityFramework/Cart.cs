using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

    [Table("Cart")]
    public class Cart
    {
        [Required(ErrorMessage = "User Id is required")]
        public Guid ProductID { get; set; }
        [Required(ErrorMessage = "User Id is required")]
        public Guid UserID { get; set; }

        public Product Product { get; set; }
       
        public User User { get; set; }
    }
