﻿using Order.Application.DataContract.Request.Client;
using Order.Domain.Validations.Base;
using System.Threading.Tasks;

namespace Order.Application.Interfacds
{
    public interface IClientApplication
    {
        Task<Response> CreateAsync(CreateClientRequest client);
    }
}