namespace Order.Domain.Models
{
    public class OrderItemModel : EntityBase
    {
        public OrderModel Order { get; set; }
        public ProductModel Product { get; set; }
        public decimal SellValue { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmout { get; set; }
    }
}
