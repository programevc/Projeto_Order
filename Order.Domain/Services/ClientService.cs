using Order.Domain.Common;
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
        private readonly ITimeProvider _timeProvider;
        private readonly IGenerators _generators;
        public ClientService(IClientRepository clientRepository, 
                             ITimeProvider timeProvider, 
                             IGenerators generators)
        {
            _clientRepository = clientRepository;
            _timeProvider = timeProvider;
            _generators = generators;
        }

        public async Task<Response> CreateAsync(ClientModel client)
        {
            var response = new Response();

            var validation = new ClientValidation();
            var errors = validation.Validate(client).GetErrors();

            if (errors.Report.Count > 0)
                return errors;

            client.Id = _generators.Generate();
            client.CreatedAt = _timeProvider.utcDateTime();

            await _clientRepository.CreateAsync(client);

            return response;
        }

        public async Task<Response> DeleteAsync(string clientId)
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

        public async Task<Response<ClientModel>> GetByIdAsync(string clientId)
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

        public async Task<Response<List<ClientModel>>> ListByFiltersAsync(string clientId = null, string name = null)
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

        public async Task<Response> UpdateAsync(ClientModel client)
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
