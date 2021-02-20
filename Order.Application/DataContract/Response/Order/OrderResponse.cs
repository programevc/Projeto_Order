using System.Collections.Generic;

namespace Order.Application.DataContract.Response.Client
{
    public sealed class OrderResponse
    {
        public string ClientId { get; set; }
        public string UserId { get; set; }
        public List<OrderItemResponse> Items { get; set; }
    }
}
