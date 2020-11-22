using AutoMapper;
using Order.Application.DataContract.Request.Product;
using Order.Application.Interfacds;
using Order.Domain.Interfaces.Services;
using Order.Domain.Models;
using Order.Domain.Validations.Base;
using System.Threading.Tasks;

namespace Order.Application.Applications
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductService _ProductService;
        private readonly IMapper _mapper; 

        public ProductApplication(IProductService ProductService, IMapper mapper)
        {
            _ProductService = ProductService;
            _mapper = mapper;
        }

        public async Task<Response> CreateAsync(CreateProductRequest Product)
        {
            var ProductModel = _mapper.Map<ProductModel>(Product);

            return await _ProductService.CreateAsync(ProductModel);
        }
    }
}
