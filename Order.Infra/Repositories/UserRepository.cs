using Dapper;
using Order.Domain.Interfaces.Repositories;
using Order.Domain.Interfaces.Repositories.DataConnector;
using Order.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnector _dbConnector;
        public UserRepository(IDbConnector dbConnector)
        {
            _dbConnector = dbConnector;
        }

        const string baseSql = @"SELECT [Id]
                                      ,[Name]
                                      ,[Login]
                                      ,[PasswordHash]
                                      ,[CreatedAt]
                                  FROM [dbo].[User]
                                  WHERE 1 = 1 ";

        public async Task CreateAsync(UserModel user)
        {
            string sql = @"INSERT INTO [dbo].[User]
                                ([Id]
                                ,[Name]
                                ,[Login]
                                ,[PasswordHash]
                                ,[CreatedAt])
                          VALUES
                                (@Id
                                ,@Name
                                ,@Login
                                ,@PasswordHash
                                ,@CreatedAt)";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new
            {
                Id = user.Id,
                Name = user.Name,
                Login = user.Login,
                PasswordHash = user.PasswordHash,
                CreatedAt = user.CreatedAt
            }, _dbConnector.dbTransaction);
        }
        public async Task UpdateAsync(UserModel user)
        {
            string sql = @"UPDATE [dbo].[User]
                             SET [Name] = @Name
                                ,[Login] = @Login
                                ,[PasswordHash] = @PasswordHash
                           WHERE id = @Id ";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new
            {
                Id = user.Id,
                Name = user.Name,
                Login = user.Login,
                PasswordHash = user.PasswordHash
            }, _dbConnector.dbTransaction);
        }
        public async Task DeleteAsync(string userId)
        {
            string sql = $"DELETE FROM [dbo].[User] WHERE id = @id";

            await _dbConnector.dbConnection.ExecuteAsync(sql, new { Id = userId }, _dbConnector.dbTransaction);
        }
        public async Task<bool> ExistsByIdAsync(string userId)
        {
            string sql = $"SELECT 1 FROM User WHERE Id = @Id ";

            var users = await _dbConnector.dbConnection.QueryAsync<bool>(sql, new { Id = userId }, _dbConnector.dbTransaction);

            return users.FirstOrDefault();
        }
        public async Task<bool> ExistsByLoginAsync(string login)
        {
            string sql = $"SELECT 1 FROM [User] WHERE Login = @Login ";
                
            var users = await _dbConnector.dbConnection.QueryAsync<bool>(sql, new { Login = login }, _dbConnector.dbTransaction);

            return users.FirstOrDefault();
        }
        public async Task<UserModel> GetByIdAsync(string userId)
        {
            string sql = $"{baseSql} AND Id = @Id";

            var users = await _dbConnector.dbConnection.QueryAsync<UserModel>(sql, new { Id = userId }, _dbConnector.dbTransaction);

            return users.FirstOrDefault();
        }
        public async Task<List<UserModel>> ListByFilterAsync(string login = null, string name = null)
        {
            string sql = $"{baseSql} ";

            if (!string.IsNullOrWhiteSpace(login))
                sql += "AND login = @Login";

            if (!string.IsNullOrWhiteSpace(name))
                sql += "AND Name like @Name";

            var users = await _dbConnector.dbConnection.QueryAsync<UserModel>(sql, new { Login = login, Name = "%" + name + "%" }, _dbConnector.dbTransaction);

            return users.ToList();
        }

        public async Task<UserModel> GetByLoginAsync(string login)
        {
            string sql = $"{baseSql} AND Login = @Login";

            var users = await _dbConnector.dbConnection.QueryAsync<UserModel>(sql, new { Login = login }, _dbConnector.dbTransaction);

            return users.FirstOrDefault();
        }
    }
}
