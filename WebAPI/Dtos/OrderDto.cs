using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WebAPI.Models;
using WebAPI.Utilities;

namespace WebAPI.Dtos
{
    public class OrderDto
    {
        public string CustomerEmail { get; set; } = null!;
        public virtual ICollection<OrderDetailDto> OrderDetails { get; set; } = null!;
    }
}
