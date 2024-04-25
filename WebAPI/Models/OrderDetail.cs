using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class OrderDetail
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Quantity cannot be empty")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "OrderId cannot be empty")]
        public Guid OrderId { get; set; }

        [Required(ErrorMessage = "ProductId cannot be empty")]
        public string ProductId { get; set; } = null!;

        public virtual Order Order { get; set; } = null!;

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; } = null!;
    }
}
