using Order.Domain.Interfaces.Repositories;
using Order.Domain.Interfaces.Services;
using Order.Domain.Models;
using Order.Domain.Validations;
using Order.Domain.Validations.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Domain.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        async Task<Response> IClientService.CreateAsync(ClientModel client)
        {
            var response = new Response();

            var validation = new ClientValidation();
            var errors = validation.Validate(client).GetErrors();

            if (errors.Report.Count > 0)
                return errors;

            await _clientRepository.CreateAsync(client);

            return response;
        }

        Task<Response> IClientService.DeleteAsync(string clientId)
        {
            throw new NotImplementedException();
        }

        Task<Response<ClientModel>> IClientService.GetByIdAsync(string clientId)
        {
            throw new NotImplementedException();
        }

        Task<Response<List<ClientModel>>> IClientService.ListByFiltersAsync(string clientId, string name)
        {
            throw new NotImplementedException();
        }

        Task<Response> IClientService.UpdateAsync(ClientModel client)
        {
            throw new NotImplementedException();
        }
    }
}
