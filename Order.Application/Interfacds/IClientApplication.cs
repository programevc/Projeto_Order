﻿using Order.Application.DataContract.Request.Client;
using Order.Application.DataContract.Response.Client;
using Order.Domain.Validations.Base;
using System.Threading.Tasks;

namespace Order.Application.Interfacds
{
    public interface IClientApplication
    {
        Task<Response> CreateAsync(CreateClientRequest client);
        Task<Response<ClientResponse>> ListByFilterAsync(string clientId, string name);
    }
}
