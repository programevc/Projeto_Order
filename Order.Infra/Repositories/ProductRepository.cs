using Dapper;
using Order.Domain.Interfaces.Repositories;
using Order.Domain.Interfaces.Repositories.DataConnector;
using Order.Domain.Models;
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
                                        ,[Description]
                                        ,[SellValue]
                                        ,[Stock]
                                        ,[CreatedAt]
                                  FROM[dbo].[Product]
                                  WHERE 1 = 1 ";
        public async Task CreateAsync(ProductModel Product)
        {
            string sql = @"INSERT INTO [dbo].[Product]
                                ([Id]
                                ,[Description]
                                ,[SellValue]
                                ,[Stock]
                                ,[CreatedAt])
                          VALUES
                                (@Id
                                ,@Description
                                ,@SellValue
                                ,@Stock
                                ,@CreatedAt)";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new
            {
                Id = Product.Id,
                Description = Product.Description,
                SellValue = Product.SellValue,
                Stock = Product.Stock,
                CreatedAt = Product.CreatedAt,
            }, _dbConnector.dbTransaction);
        }

        public async Task DeleteAsync(string ProductId)
        {
            string sql = $"DELETE FROM [dbo].[Product] WHERE id = @id";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new { Id = ProductId }, _dbConnector.dbTransaction);
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

            if (!string.IsNullOrWhiteSpace(ProductId))
                sql += "AND Id = @Id";

            if (!string.IsNullOrWhiteSpace(name))
                sql += "AND Description like @Name";
            
            var Products = await _dbConnector.dbConnection.QueryAsync<ProductModel>(sql,new { Id = ProductId, Name = "%" + name + "%" }, _dbConnector.dbTransaction);

            return Products.ToList();
        }

        public async Task UpdateAsync(ProductModel Product)
        {
            string sql = @"UPDATE [dbo].[Product]
                               SET [Description] = @Description
                                  ,[SellValue] = @SellValue
                                  ,[Stock] = @Stock
                           WHERE Id = @Id";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new
            {
                Id = Product.Id,
                Description = Product.Description,
                SellValue = Product.SellValue,
                Stock = Product.Stock
            }, _dbConnector.dbTransaction);
        }
    }
}
