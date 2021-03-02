using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Application.DataContract.Request.User;
using Order.Application.Interfacds;

namespace Order.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserApplication _UserApplication;

        public UserController(IUserApplication UserApplication)
        {
            _UserApplication = UserApplication;
        }

        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] string userId, [FromQuery] string name)
        {
            var response = await _UserApplication.ListByFilterAsync(userId, name);

            if (response.Report.Any())
                return UnprocessableEntity(response.Report);

            return Ok(response);
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<UserController>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post([FromBody] CreateUserRequest request)
        {
            var response = await _UserApplication.CreateAsync(request);

            if (response.Report.Any())
                return UnprocessableEntity(response.Report);

            return Ok(response);
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // DELETE api/<UserController>/5
        [HttpPost("auth")]
        [AllowAnonymous]
        public async Task<ActionResult> Auth([FromBody] AuthRequest request)
        {
            var response = await _UserApplication.AuthAsync(request);

            if (response.Report.Any())
                return UnprocessableEntity(response.Report);

            return Ok(response);
        }
    }
}
