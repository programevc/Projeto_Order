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
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _OrderRepository;
        public OrderService(IOrderRepository OrderRepository)
        {
            _OrderRepository = OrderRepository;

        }

        public async Task<Response> CreateAsync(OrderModel order)
        {
            var response = new Response();

            var validation = new OrderValidation();
            var errors = validation.Validate(order).GetErrors();

            if (errors.Report.Count > 0)
                return errors;

            await _OrderRepository.CreateAsync(order);

            return response;
        }

        public async Task<Response> DeleteAsync(string orderId)
        {
            var response = new Response();

            var exists = await _OrderRepository.ExistsByIdAsync(orderId);

            if (!exists)
            {
                response.Report.Add(Report.Create($"Order {orderId} not exists!"));
                return response;
            }

            await _OrderRepository.DeleteAsync(orderId);

            return response;
        }

        public async Task<Response<OrderModel>> GetByIdAsync(string orderId)
        {
            var response = new Response<OrderModel>();

            var exists = await _OrderRepository.ExistsByIdAsync(orderId);

            if (!exists)
            {
                response.Report.Add(Report.Create($"Order {orderId} not exists!"));
                return response;
            }

            var data = await _OrderRepository.GetByIdAsync(orderId);
            response.Data = data;
            return response;
        }

        public async Task<Response<List<OrderModel>>> ListByFiltersAsync(string orderId = null, string clientId = null, string userId = null)
        {
            var response = new Response<List<OrderModel>>();

            if (!string.IsNullOrWhiteSpace(orderId))
            {
                var exists = await _OrderRepository.ExistsByIdAsync(orderId);

                if (!exists)
                {
                    response.Report.Add(Report.Create($"Order {orderId} not exists!"));
                    return response;
                }
            }

            var data = await _OrderRepository.ListByFilterAsync(orderId,clientId,userId);
            response.Data = data;

            return response;
        }

        public async Task<Response> UpdateAsync(OrderModel order)
        {
            var response = new Response();

            var validation = new OrderValidation();
            var errors = validation.Validate(order).GetErrors();

            if (errors.Report.Count > 0)
                return errors;

            var exists = await _OrderRepository.ExistsByIdAsync(order.Id);

            if (!exists)
            {
                response.Report.Add(Report.Create($"Order {order.Id} not exists!"));
                return response;
            }

            await _OrderRepository.UpdateAsync(order);

            return response;
        }
    }
}
