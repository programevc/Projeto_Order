using AutoMapper;
using Order.Application.DataContract.Request.Order;
using Order.Application.Interfacds;
using Order.Domain.Interfaces.Services;
using Order.Domain.Models;
using Order.Domain.Validations.Base;
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
            var OrderModel = _mapper.Map<OrderModel>(Order);

            return await _OrderService.CreateAsync(OrderModel);
        }
    }
}
