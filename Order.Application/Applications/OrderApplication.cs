using AutoMapper;
using Order.Application.DataContract.Request.Order;
using Order.Application.DataContract.Response.Client;
using Order.Application.Interfacds;
using Order.Domain.Interfaces.Services;
using Order.Domain.Models;
using Order.Domain.Validations.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Application.Applications
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IOrderService _OrderService;
        private readonly IMapper _mapper;

        public OrderApplication(IOrderService OrderService, IMapper mapper)
        {
            _OrderService = OrderService;
            _mapper = mapper;
        }

        public async Task<Response> CreateAsync(CreateOrderRequest Order)
        {
            try
            {
                var OrderModel = _mapper.Map<OrderModel>(Order);

                return await _OrderService.CreateAsync(OrderModel);
            }
            catch (Exception ex)
            {
                var response = Report.Create(ex.Message);

                return Response.Unprocessable(response);
            }
        }

        public async Task<Response<OrderResponse>> GetByIdAsync(string orderId)
        {
            Response<OrderModel> user = await _OrderService.GetByIdAsync(orderId);

            if (user.Report.Any())
                return Response.Unprocessable<OrderResponse>(user.Report);

            var response = _mapper.Map<OrderResponse>(user.Data);

            return Response.OK(response);
        }

        public async Task<Response<List<OrderResponse>>> ListByFilterAsync(string orderId = null, string clientId = null, string userId = null)
        {
            Response<List<OrderModel>> user = await _OrderService.ListByFiltersAsync(orderId, clientId, userId);

            if (user.Report.Any())
                return Response.Unprocessable<List<OrderResponse>>(user.Report);

            var response = _mapper.Map<List<OrderResponse>>(user.Data);

            return Response.OK(response);
        }
    }
}
