using Order.Application.DataContract.Request.Product;
using Order.Application.DataContract.Response.Product;
using Order.Domain.Validations.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Application.Interfacds
{
    public interface IProductApplication
    {
        Task<Response> CreateAsync(CreateProductRequest Product); 
        Task<Response<List<ProductResponse>>> ListByFilterAsync(string productId = null, string description = null);

    }
}
