using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Order.Application.DataContract.Request.Product;
using Order.Application.Interfacds;

namespace Order.Api.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductApplication _ProductApplication;

        public ProductController(IProductApplication ProductApplication)
        {
            _ProductApplication = ProductApplication;
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <param name="clientid"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] string productId, [FromQuery] string description)
        {
            var response = await _ProductApplication.ListByFilterAsync(productId, description);

            if (response.Report.Any())
                return UnprocessableEntity(response.Report);

            return Ok(response);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateProductRequest request)
        {
            var response = await _ProductApplication.CreateAsync(request);

            if (response.Report.Any())
                return UnprocessableEntity(response.Report);

            return Ok(response);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
