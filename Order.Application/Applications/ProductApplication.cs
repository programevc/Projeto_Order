using AutoMapper;
using Order.Application.DataContract.Request.Product;
using Order.Application.DataContract.Response.Product;
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
            try
            {
                var ProductModel = _mapper.Map<ProductModel>(Product);

                return await _ProductService.CreateAsync(ProductModel);
            }
            catch (Exception ex)
            {
                var response = Report.Create(ex.Message);

                return Response.Unprocessable(response);
            }
        }

        public async Task<Response<List<ProductResponse>>> ListByFilterAsync(string productId = null, string description = null)
        {
            Response<List<ProductModel>> product = await _ProductService.ListByFilterAsync(productId, description);

            if (product.Report.Any())
                return Response.Unprocessable<List<ProductResponse>>(product.Report);

            var response = _mapper.Map<List<ProductResponse>>(product.Data);

            return Response.OK(response);
        }
    }
}
