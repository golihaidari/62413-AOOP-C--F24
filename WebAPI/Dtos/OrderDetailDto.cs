using System.ComponentModel.DataAnnotations;
using WebAPI.Models;

namespace WebAPI.Dtos
{
    public class OrderDetailDto
    {
        public required Product Product { get; set; } = null!;

        [Required]
        public int Quantity { get; set; }
    }
}
