using AutoMapper;
using Order.Application.DataContract.Request.Client;
using Order.Application.DataContract.Response.Client;
using Order.Application.Interfacds;
using Order.Domain.Interfaces.Services;
using Order.Domain.Models;
using Order.Domain.Validations.Base;
using System.Threading.Tasks;

namespace Order.Application.Applications
{
    public class ClientApplication : IClientApplication
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper; 

        public ClientApplication(IClientService clientService, IMapper mapper)
        {
            _clientService = clientService;
            _mapper = mapper;
        }

        public async Task<Response> CreateAsync(CreateClientRequest client)
        {
            var clientModel = _mapper.Map<ClientModel>(client);

            return await _clientService.CreateAsync(clientModel);
        }

        public async Task<Response<ClientResponse>> ListByFilterAsync(string clientId, string name)
        {
            var client = await _clientService.ListByFiltersAsync(clientId, name);

            var response = _mapper.Map<ClientResponse>(client.Data);

            return Response.OK(response);
        }
    }
}
