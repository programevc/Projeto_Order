using Order.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Domain.Interfaces.Repositories
{
    public interface IClientRepository
    {
        Task CreateAsync(ClientModel client);
        Task UpdateAsync(ClientModel client);
        Task DeleteAsync(string clientId);
        Task<bool> ExistsByIdAsync(string clientId);
        Task<ClientModel> GetByIdAsync(string clientId);
        Task<List<ClientModel>> ListByFilterAsync(string clientId = null, string name = null);
    }
}
