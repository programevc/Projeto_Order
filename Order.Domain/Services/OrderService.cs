using Order.Domain.Common;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITimeProvider _timeProvider;
        private readonly IGenerators _generators;

        public OrderService(ITimeProvider timeProvider,
                            IGenerators generators, IUnitOfWork unitOfWork)
        {
            _timeProvider = timeProvider;
            _generators = generators;
            _unitOfWork = unitOfWork;
        }

        public async Task<Response> CreateAsync(OrderModel order)
        {
            var response = new Response();

            var validation = new OrderValidation();
            var errors = validation.Validate(order).GetErrors();

            if (errors.Report.Count > 0)
                return errors;

            order.Id = _generators.Generate();
            order.CreatedAt = _timeProvider.utcDateTime();

            foreach (var item in order.Items)
            {
                item.Order = order;
                item.Id = _generators.Generate();
                item.CreatedAt = _timeProvider.utcDateTime();
            }

            await _unitOfWork.OrderRepository.CreateAsync(order);

            return response;
        }

        public async Task<Response> DeleteAsync(string orderId)
        {
            var response = new Response();

            var exists = await _unitOfWork.OrderRepository.ExistsByIdAsync(orderId);

            if (!exists)
            {
                response.Report.Add(Report.Create($"Order {orderId} not exists!"));
                return response;
            }

            await _unitOfWork.OrderRepository.DeleteAsync(orderId);

            return response;
        }

        public async Task<Response<OrderModel>> GetByIdAsync(string orderId)
        {
            var response = new Response<OrderModel>();
            _unitOfWork.BeginTransaction();

            try
            {
                var exists = await _unitOfWork.OrderRepository.ExistsByIdAsync(orderId);

                if (!exists)
                {
                    response.Report.Add(Report.Create($"Order {orderId} not exists!"));
                    return response;
                }

                var data = await _unitOfWork.OrderRepository.GetByIdAsync(orderId);
                data.Items = await _unitOfWork.OrderRepository.ListItemByOrderIdAsync(orderId);

                _unitOfWork.CommitTransaction();

                response.Data = data;
                return response;
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                return response;
            }
        }

        public async Task<Response<List<OrderModel>>> ListByFiltersAsync(string orderId = null, string clientId = null, string userId = null)
        {
            var response = new Response<List<OrderModel>>();

            if (!string.IsNullOrWhiteSpace(orderId))
            {
                var exists = await _unitOfWork.OrderRepository.ExistsByIdAsync(orderId);

                if (!exists)
                {
                    response.Report.Add(Report.Create($"Order {orderId} not exists!"));
                    return response;
                }
            }

            var data = await _unitOfWork.OrderRepository.ListByFilterAsync(orderId, clientId, userId);
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

            var exists = await _unitOfWork.OrderRepository.ExistsByIdAsync(order.Id);

            if (!exists)
            {
                response.Report.Add(Report.Create($"Order {order.Id} not exists!"));
                return response;
            }

            await _unitOfWork.OrderRepository.UpdateAsync(order);

            return response;
        }
    }
}
