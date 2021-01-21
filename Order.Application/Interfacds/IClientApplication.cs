using Order.Application.DataContract.Request.Client;
using Order.Application.DataContract.Response.Client;
using Order.Domain.Validations.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Application.Interfacds
{
    public interface IClientApplication
    {
        Task<Response> CreateAsync(CreateClientRequest client);
        Task<Response<List<ClientResponse>>> ListByFilterAsync(string clientId, string name);
        Task<Response<ClientResponse>> GetByIdAsync(string clientId);
        Task<Response> UpdateAsync(UpdateClientRequest request);
        Task<Response> DeleteAsync(string clientId);
    }
}
