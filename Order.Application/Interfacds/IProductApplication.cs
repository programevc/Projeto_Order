using Order.Application.DataContract.Request.Product;
using Order.Domain.Validations.Base;
using System.Threading.Tasks;

namespace Order.Application.Interfacds
{
    public interface IProductApplication
    {
        Task<Response> CreateAsync(CreateProductRequest Product);
    }
}
