using System.Collections.Generic;

namespace Order.Application.DataContract.Request.Order
{
    public sealed class CreateOrderRequest
    {
        public string ClientId { get; set; }
        public string UserId { get; set; }
        public List<CreateOrderItemRequest> Items { get; set; }
    }
}
