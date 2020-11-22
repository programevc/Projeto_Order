using Order.Application.DataContract.Request.Order;
using Order.Domain.Validations.Base;
using System.Threading.Tasks;

namespace Order.Application.Interfacds
{
    public interface IOrderApplication
    {
        Task<Response> CreateAsync(CreateOrderRequest Order);
    }
}
