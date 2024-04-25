
namespace DesktopApp.Models
{
    public class Order
    {
        public string Id { get; set; } = string.Empty;
        
        public DateTime Date {  get; set; } = DateTime.Now;
        
        public int Status { get;set; } = 0;

        public string CustomerEmail { get; set; } = string.Empty;

        public List<BasketItem> OrderDetails { get; set; } = null!;     
    }
}
