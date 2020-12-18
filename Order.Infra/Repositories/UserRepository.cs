using Order.Domain.Interfaces.Repositories;
using Order.Domain.Interfaces.Repositories.DataConnector;
using Order.Domain.Models;
using System;
using System.Collections.Generic;
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

        public Task CreateAsync(UserModel user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ExistsByLoginAsync(string login)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> GetByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserModel>> ListByFilterAsync(string userId = null, string name = null)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UserModel user)
        {
            throw new NotImplementedException();
        }
    }
}
