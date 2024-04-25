
namespace DesktopApp.Models.JsonModels
{
    class OrderResponse
    {
        public string Id { get; set; } = string.Empty;

        public DateTime OrderDate { get; set; }

        public Status Status { get; set; }

        public string CustomerEmail { get; set; } = string.Empty;

        public List<OrderDetail> OrderDetails { get; set; } = null!;
    }

    internal enum Status
    {
        Pending,
        Finished,
        Canceled
    }

    internal class OrderDetail
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public string OrderId { get; set; }
        public string ProductId { get; set; }
        public object Order { get; set; }
        public Product Product { get; set; } = null!;
    }

    internal class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public int RebateQuantity { get; set; }
        public int RebatePercent { get; set; }
        public string UpsellProductId { get; set; }
        public string ImageUrl { get; set; }
    }

}
