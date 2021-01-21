using AutoMapper;
using Order.Application.DataContract.Request.Client;
using Order.Application.DataContract.Response.Client;
using Order.Application.Interfacds;
using Order.Domain.Interfaces.Services;
using Order.Domain.Models;
using Order.Domain.Validations.Base;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<Response> DeleteAsync(string clientId)
        {
            return await _clientService.DeleteAsync(clientId);
        }

        public async Task<Response<ClientResponse>> GetByIdAsync(string clientId)
        {
            Response<ClientModel> client = await _clientService.GetByIdAsync(clientId);

            if (client.Report.Any())
                return Response.Unprocessable<ClientResponse>(client.Report);

            var response = _mapper.Map<ClientResponse>(client.Data);

            return Response.OK(response);
        }

        public async Task<Response<List<ClientResponse>>> ListByFilterAsync(string clientId, string name)
        {
            Response<List<ClientModel>> client = await _clientService.ListByFiltersAsync(clientId, name);

            if (client.Report.Any())
                return Response.Unprocessable<List<ClientResponse>>(client.Report);

            var response = _mapper.Map<List<ClientResponse>>(client.Data);

            return Response.OK(response);
        }

        public async Task<Response> UpdateAsync(UpdateClientRequest request)
        {
            var clientModel = _mapper.Map<ClientModel>(request);

            return await _clientService.UpdateAsync(clientModel);
        }
    }
}
