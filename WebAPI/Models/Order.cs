using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebAPI.Utilities;

namespace WebAPI.Models
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "OrderDate cannot be empty")]
        public DateTime OrderDate { get; set; }

        public Status Status { get; set; }

        [Required(ErrorMessage = "CustomerEmail cannot be empty")]
        public string CustomerEmail { get; set; } = null!;


        [ForeignKey("CustomerEmail")]
        public virtual User Customer { get; set; } = null!;
        
        [ForeignKey("OrderId")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = null!;

    }
}
