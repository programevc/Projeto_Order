using Dapper;
using Order.Domain.Interfaces.Repositories;
using Order.Domain.Interfaces.Repositories.DataConnector;
using Order.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Infra.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IDbConnector _dbConnector;
        public ClientRepository(IDbConnector dbConnector)
        {
            _dbConnector = dbConnector;
        }

        const string baseSql = @"SELECT [Id]
                                      ,[Name]
                                      ,[Email]
                                      ,[PhoneNumber]
                                      ,[Adress]
                                      ,[CreatedAt]
                                  FROM[dbo].[Client]
                                  WHERE 1 = 1 ";
        public Task CreateAsync(ClientModel client)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string clientId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ExistsByIdAsync(string clientId)
        {
            string sql = $"SELECT 1 FROM Client WHERE Id = @Id ";

            var clients = await _dbConnector.dbConnection.QueryAsync<bool>(sql, new { Id = clientId }, _dbConnector.dbTransaction);

            return clients.FirstOrDefault();
        }

        public async Task<ClientModel> GetByIdAsync(string clientId)
        {
            string sql = $"{baseSql} AND Id = @Id";

            var clients = await _dbConnector.dbConnection.QueryAsync<ClientModel>(sql, new { Id = clientId}, _dbConnector.dbTransaction);

            return clients.FirstOrDefault();
        }

        public async Task<List<ClientModel>> ListByFilterAsync(string clientId = null, string name = null)
        {
            string sql = $"{baseSql} ";

            if (string.IsNullOrWhiteSpace(clientId))
                sql += "AND Id = @Id";

            if (string.IsNullOrWhiteSpace(name))
                sql += "AND Name like @Name";
            
            var clients = await _dbConnector.dbConnection.QueryAsync<ClientModel>(sql,new { Id = clientId, Name = "%" + name + "%" }, _dbConnector.dbTransaction);

            return clients.ToList();
        }

        public Task UpdateAsync(ClientModel client)
        {
            throw new NotImplementedException();
        }
    }
}
