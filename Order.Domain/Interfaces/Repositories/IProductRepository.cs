using Order.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task CreateAsync(ProductModel product);
        Task UpdateAsync(ProductModel product);
        Task DeleteAsync(string productId);
        Task<ProductModel> GetByIdAsync(string productId);
        Task<List<ProductModel>> ListByFilterAsync(string productId = null, string description = null);
        Task<bool> ExistsByIdAsync(string productId);

    }
}
