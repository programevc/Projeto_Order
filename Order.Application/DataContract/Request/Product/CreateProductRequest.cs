namespace Order.Application.DataContract.Request.Product
{
    public sealed class CreateProductRequest
    {
        public string Description { get; set; }
        public decimal SellValue { get; set; }
        public int Stock { get; set; }
    }
}
