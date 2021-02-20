namespace Order.Application.DataContract.Response.Product
{
    public class ProductResponse
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public decimal SellValue { get; set; }
        public int Stock { get; set; }
    }
}
