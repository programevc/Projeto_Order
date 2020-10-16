using Order.Domain.Interfaces.Services;
using Order.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Domain.Services
{
    public class OrderService : IOrderService
    {
        public Task CreateAsync(OrderModel order)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string orderId)
        {
            throw new NotImplementedException();
        }

        public Task<OrderModel> GetByIdAsync(string orderId)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderModel>> ListByFiltersAsync(string orderId = null, string clientId = null, string userId = null)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(OrderModel order)
        {
            throw new NotImplementedException();
        }
    }
}
