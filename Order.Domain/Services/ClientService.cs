using Order.Domain.Interfaces.Repositories;
using Order.Domain.Interfaces.Services;
using Order.Domain.Models;
using Order.Domain.Validations;
using Order.Domain.Validations.Base;
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

        async Task<Response> IClientService.DeleteAsync(string clientId)
        {
            var response = new Response();

            var exists = await _clientRepository.ExistsByIdAsync(clientId);

            if (!exists)
            {
                response.Report.Add(Report.Create($"Client {clientId} not exists!"));
                return response;
            }

            await _clientRepository.DeleteAsync(clientId);

            return response;
        }

        async Task<Response<ClientModel>> IClientService.GetByIdAsync(string clientId)
        {
            var response = new Response<ClientModel>();

            var exists = await _clientRepository.ExistsByIdAsync(clientId);

            if (!exists)
            {
                response.Report.Add(Report.Create($"Client {clientId} not exists!"));
                return response;
            }

            var data = await _clientRepository.GetByIdAsync(clientId);
            response.Data = data;
            return response;
        }

        async Task<Response<List<ClientModel>>> IClientService.ListByFiltersAsync(string clientId, string name)
        {
            var response = new Response<List<ClientModel>>();

            if (!string.IsNullOrWhiteSpace(clientId))
            {
                var exists = await _clientRepository.ExistsByIdAsync(clientId);

                if (!exists)
                {
                    response.Report.Add(Report.Create($"Client {clientId} not exists!"));
                    return response;
                }
            }

            var data = await _clientRepository.ListByFilterAsync(clientId, name);
            response.Data = data;

            return response;
        }

        async Task<Response> IClientService.UpdateAsync(ClientModel client)
        {
            var response = new Response();

            var validation = new ClientValidation();
            var errors = validation.Validate(client).GetErrors();

            if (errors.Report.Count > 0)
                return errors;

            var exists = await _clientRepository.ExistsByIdAsync(client.Id);

            if (!exists)
            {
                response.Report.Add(Report.Create($"Client {client.Id} not exists!"));
                return response;
            }

            await _clientRepository.UpdateAsync(client);

            return response;
        }
    }
}
