using Order.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> AutheticationAsync(UserModel user);
        Task CreateAsync(UserModel user);
        Task UpdateAsync(UserModel user);
        Task DeleteAsync(string userId);
        Task<UserModel> GetByIdAsync(string userId);
        Task<List<UserModel>> ListByFilterAsync(string userId = null, string name = null);
    }
}
