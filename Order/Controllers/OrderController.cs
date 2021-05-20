using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Application.DataContract.Request.Order;
using Order.Application.Interfacds;

namespace Order.Api.Controllers
{
    [Route("api/order")]
    [ApiController]
    //[Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderApplication _OrderApplication;

        public OrderController(IOrderApplication OrderApplication)
        {
            _OrderApplication = OrderApplication;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] string orderId, [FromQuery] string clientid, [FromQuery] string userId)
        {
            var response = await _OrderApplication.ListByFilterAsync(orderId, clientid, userId);

            if (response.Report.Any())
                return UnprocessableEntity(response.Report);

            return Ok(response);
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            var response = await _OrderApplication.GetByIdAsync(id);

            if (response.Report.Any())
                return UnprocessableEntity(response.Report);

            return Ok(response);
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateOrderRequest request)
        {
            var response = await _OrderApplication.CreateAsync(request);

            if (response.Report.Any())
                return UnprocessableEntity(response.Report);

            return Ok(response);
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
