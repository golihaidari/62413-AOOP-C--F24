namespace DesktopApp.Models
{
    public class Product
    {
        public string id { get; set; } 
        public string name { get; set; }
        public decimal price { get; set; }
        public string currency { get; set; }
        public int rebateQuantity { get; set; }
        public int rebatePercent { get; set; }
        public string upsellProductId { get; set; }
        public string imageUrl { get; set; }
       
    }
}
