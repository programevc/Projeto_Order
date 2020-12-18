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
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnector _dbConnector;
        public ProductRepository(IDbConnector dbConnector)
        {
            _dbConnector = dbConnector;
        }

        const string baseSql = @"SELECT [Id]
                                      ,[Name]
                                      ,[Email]
                                      ,[PhoneNumber]
                                      ,[Adress]
                                      ,[CreatedAt]
                                  FROM[dbo].[Product]
                                  WHERE 1 = 1 ";
        public Task CreateAsync(ProductModel Product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string ProductId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsByIdAsync(string ProductId)
        {
            string sql = $"SELECT 1 FROM Product WHERE Id = @Id ";

            var Products = await _dbConnector.dbConnection.QueryAsync<bool>(sql, new { Id = ProductId }, _dbConnector.dbTransaction);

            return Products.FirstOrDefault();
        }

        public async Task<ProductModel> GetByIdAsync(string ProductId)
        {
            string sql = $"{baseSql} AND Id = @Id";

            var Products = await _dbConnector.dbConnection.QueryAsync<ProductModel>(sql, new { Id = ProductId}, _dbConnector.dbTransaction);

            return Products.FirstOrDefault();
        }

        public async Task<List<ProductModel>> ListByFilterAsync(string ProductId = null, string name = null)
        {
            string sql = $"{baseSql} ";

            if (string.IsNullOrWhiteSpace(ProductId))
                sql += "AND Id = @Id";

            if (string.IsNullOrWhiteSpace(name))
                sql += "AND Name like @Name";
            
            var Products = await _dbConnector.dbConnection.QueryAsync<ProductModel>(sql,new { Id = ProductId, Name = "%" + name + "%" }, _dbConnector.dbTransaction);

            return Products.ToList();
        }

        public Task UpdateAsync(ProductModel Product)
        {
            throw new NotImplementedException();
        }
    }
}
