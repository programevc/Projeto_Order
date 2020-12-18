using Dapper;
using Order.Domain.Interfaces.Repositories;
using Order.Domain.Interfaces.Repositories.DataConnector;
using Order.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Infra.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDbConnector _dbConnector;
        public OrderRepository(IDbConnector dbConnector)
        {
            _dbConnector = dbConnector;
        }

        public Task CreateAsync(OrderModel order)
        {
            throw new NotImplementedException();
        }

        public Task CreateItemAsync(OrderItemModel item)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string orderId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteItemAsync(string itemId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsByIdAsync(string orderId)
        {
            throw new NotImplementedException();
        }

        public Task<OrderModel> GetByIdAsync(string orderId)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderModel>> ListByFilterAsync(string orderId = null, string clientId = null, string userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderItemModel>> ListItemByOrderIdAsync(string orderId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(OrderModel order)
        {
            throw new NotImplementedException();
        }

        public Task UpdateItemAsync(OrderItemModel item)
        {
            throw new NotImplementedException();
        }
    }
}
