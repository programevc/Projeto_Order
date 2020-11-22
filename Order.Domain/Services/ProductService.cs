﻿using Order.Domain.Interfaces.Repositories;
using Order.Domain.Interfaces.Services;
using Order.Domain.Models;
using Order.Domain.Validations;
using Order.Domain.Validations.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Domain.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;

        }
        public async Task<Response> CreateAsync(ProductModel product)
        {
            var response = new Response();

            var validation = new ProductValidation();
            var errors = validation.Validate(product).GetErrors();

            if (errors.Report.Count > 0)
                return errors;

            await _productRepository.CreateAsync(product);

            return response;
        }

        public async Task<Response> DeleteAsync(string productId)
        {
            var response = new Response();

            var exists = await _productRepository.ExistsByIdAsync(productId);

            if (!exists)
            {
                response.Report.Add(Report.Create($"product {productId} not exists!"));
                return response;
            }

            await _productRepository.DeleteAsync(productId);

            return response;
        }

        public async Task<Response<ProductModel>> GetByIdAsync(string productId)
        {
            var response = new Response<ProductModel>();

            var exists = await _productRepository.ExistsByIdAsync(productId);

            if (!exists)
            {
                response.Report.Add(Report.Create($"product {productId} not exists!"));
                return response;
            }

            var data = await _productRepository.GetByIdAsync(productId);
            response.Data = data;
            return response;
        }

        public async  Task<Response<List<ProductModel>>> ListByFilterAsync(string productId = null, string description = null)
        {
            var response = new Response<List<ProductModel>>();

            if (!string.IsNullOrWhiteSpace(productId))
            {
                var exists = await _productRepository.ExistsByIdAsync(productId);

                if (!exists)
                {
                    response.Report.Add(Report.Create($"product {productId} not exists!"));
                    return response;
                }
            }

            var data = await _productRepository.ListByFilterAsync(productId, description);
            response.Data = data;

            return response;
        }

        public async Task<Response> UpdateAsync(ProductModel product)
        {
            var response = new Response();

            var validation = new ProductValidation();
            var errors = validation.Validate(product).GetErrors();

            if (errors.Report.Count > 0)
                return errors;

            var exists = await _productRepository.ExistsByIdAsync(product.Id);

            if (!exists)
            {
                response.Report.Add(Report.Create($"product {product.Id} not exists!"));
                return response;
            }

            await _productRepository.UpdateAsync(product);

            return response;
        }
    }
}
