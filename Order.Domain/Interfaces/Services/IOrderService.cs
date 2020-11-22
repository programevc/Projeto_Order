using Order.Domain.Models;
using Order.Domain.Validations.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Domain.Interfaces.Services
{
    public interface IOrderService
    {
        Task<Response> CreateAsync(OrderModel order);
        Task<Response> UpdateAsync(OrderModel order);
        Task<Response> DeleteAsync(string orderId);
        Task<Response<OrderModel>> GetByIdAsync(string orderId);
        Task<Response<List<OrderModel>>> ListByFiltersAsync(string orderId = null, string clientId = null, string userId = null);
    }
}
