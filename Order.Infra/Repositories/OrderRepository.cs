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

        const string baseSql = @"SELECT o.[Id]
                                      ,o.[CreatedAt]
                                      ,c.Id
	                                  ,c.[Name]
                                      ,u.Id
	                                  ,u.[Name]
                                 FROM [dbo].[Order] o
                                 JOIN [dbo].[Client] c ON o.ClientId = c.Id
                                 JOIN [dbo].[User] u ON o.UserId = u.Id
                                 WHERE 1 = 1 ";

        public async Task CreateAsync(OrderModel order)
        {
            string sql = @"INSERT INTO [dbo].[Order]
                                 ([Id]
                                 ,[ClientId]
                                 ,[UserId]
                                 ,[CreatedAt])
                           VALUES
                                 (@Id
                                 ,@ClientId
                                 ,@UserId
                                 ,@CreatedAt)";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new
            {
                Id = order.Id,
                ClientId = order.Client.Id,
                UserId = order.User.Id,
                CreatedAt = order.CreatedAt
            }, _dbConnector.dbTransaction);

            if (order.Items.Any())
            {
                foreach (var item in order.Items)
                {
                    await CreateItemAsync(item);
                }
            }
        }

        public async Task CreateItemAsync(OrderItemModel item)
        {
            string sql = @"INSERT INTO [dbo].[OrderItem]
                                   ([Id]
                                   ,[OrderId]
                                   ,[ProductId]
                                   ,[SellValue]
                                   ,[Quantity]
                                   ,[TotalAmount]
                                   ,[CreatedAt])
                             VALUES
                                   (@Id
                                   ,@OrderId
                                   ,@ProductId
                                   ,@SellValue
                                   ,@Quantity
                                   ,@TotalAmount
                                   ,@CreatedAt)";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new
            {
                Id = item.Id,
                OrderId = item.Order.Id,
                ProductId = item.Product.Id,
                SellValue = item.SellValue,
                Quantity = item.Quantity,
                TotalAmount = item.TotalAmout,
                CreatedAt = item.CreatedAt
            }, _dbConnector.dbTransaction);
        }

        public async Task DeleteAsync(string orderId)
        {
            string sql = $"DELETE FROM [dbo].[Order] WHERE id = @id";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new { Id = orderId }, _dbConnector.dbTransaction);
        }

        public async Task DeleteItemAsync(string itemId)
        {
            string sql = $"DELETE FROM [dbo].[OrderItem] WHERE id = @id";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new { Id = itemId }, _dbConnector.dbTransaction);
        }

        public async Task<bool> ExistsByIdAsync(string orderId)
        {
            string sql = $"SELECT 1 FROM [Order] WHERE Id = @Id ";

            var order = await _dbConnector.dbConnection.QueryAsync<bool>(sql, new { Id = orderId }, _dbConnector.dbTransaction);

            return order.FirstOrDefault();
        }

        public async Task<OrderModel> GetByIdAsync(string orderId)
        {
            string sql = $"{baseSql} AND o.Id = @Id";

            var order = await _dbConnector.dbConnection.QueryAsync<OrderModel, ClientModel, UserModel, OrderModel>(
                sql: sql,
                map: (ord, client, user) =>
                {
                    ord.Client = client;
                    ord.User = user;
                    return ord;
                },
                param: new { Id = orderId },
                splitOn: "Id",
                transaction: _dbConnector.dbTransaction);

            return order.FirstOrDefault();
        }

        public async Task<List<OrderModel>> ListByFilterAsync(string orderId = null, string clientId = null, string userId = null)
        {
            string sql = $"{baseSql} ";

            if (!string.IsNullOrWhiteSpace(orderId))
                sql += "AND o.Id = @OrderId";

            if (!string.IsNullOrWhiteSpace(clientId))
                sql += "AND o.clientId = @ClientId";

            if (!string.IsNullOrWhiteSpace(userId))
                sql += "AND o.userId = @UserId";

            var order = await _dbConnector.dbConnection.QueryAsync<OrderModel, ClientModel, UserModel, OrderModel>(
                sql: sql,
                map: (ord, client, user) =>
                {
                    ord.Client = client;
                    ord.User = user;
                    return ord;
                },
                param: new { OrderId = orderId, ClientId = clientId, UserId = userId },
                splitOn: "Id",
                transaction: _dbConnector.dbTransaction);

            return order.ToList();
        }

        public async Task<List<OrderItemModel>> ListItemByOrderIdAsync(string orderId)
        {
            string sql = @"SELECT oi.[Id]
                                 ,oi.[SellValue]
                                 ,oi.[Quantity]
                                 ,oi.[TotalAmount]
                                 ,oi.[CreatedAt]
                                 ,oi.[OrderId] as id
                                 ,oi.[ProductId] as id
	                             ,p.[Description]
                             FROM [dbo].[OrderItem] oi
                             JOIN [dbo].[Product] p on oi.ProductId = p.id
                             WHERE oi.OrderId = @OrderId";

            var itens = await _dbConnector.dbConnection.QueryAsync<OrderItemModel, OrderModel, ProductModel, OrderItemModel>(
                sql: sql,
                map: (item, order, prod) =>
                {
                    item.Order = order;
                    item.Product = prod;
                    return item;
                },
                param: new { OrderId = orderId },
                splitOn: "Id",
                transaction: _dbConnector.dbTransaction);

            return itens.ToList();
        }

        public async Task UpdateAsync(OrderModel order)
        {
            string sql = @"UPDATE [dbo].[Order]
                            SET [ClientId] = @ClientId
                               ,[UserId] = @UserId
                          WHERE id = @Id ";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new
            {
                Id = order.Id,
                ClientId = order.Client.Id,
                UserId = order.User.Id
            }, _dbConnector.dbTransaction);

            if (order.Items.Any())
            {
                string deleteItem = $"DELETE FROM [dbo].[OrderItem] WHERE OrderId = @OrderId";

                await _dbConnector.dbConnection.ExecuteAsync(deleteItem, new { OrderId = order.Id }, _dbConnector.dbTransaction);

                foreach (var item in order.Items)
                {
                    await CreateItemAsync(item);
                }
            }
        }
    }
}
