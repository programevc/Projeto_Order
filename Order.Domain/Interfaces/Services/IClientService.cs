using Order.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Domain.Interfaces.Services
{
    public interface IClientService
    {
        Task CreateAsync(ClientModel client);
        Task UpdateAsync(ClientModel client);
        Task DeleteAsync(string clientId);
        Task<ClientModel> GetByIdAsync(string clientId);
        Task<List<ClientModel>> ListByFiltersAsync(string clientId = null, string name = null);
    }
}
