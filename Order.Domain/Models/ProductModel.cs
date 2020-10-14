namespace Order.Domain.Models
{
    public class ProductModel :EntityBase
    {
        public string Description { get; set; }
        public string SellValue { get; set; }
        public int Stock { get; set; }
    }
}
