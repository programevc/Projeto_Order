using System.Collections.Generic;

namespace Order.Domain.Models
{
    public class OrderModel: EntityBase
    {
        public ClientModel Client { get; set; }
        public UserModel User { get; set; }
        public List<OrderItemModel>  Items { get; set; }
    }
}
