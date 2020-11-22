using AutoMapper;
using Order.Application.DataContract.Request.User;
using Order.Application.Interfacds;
using Order.Domain.Interfaces.Services;
using Order.Domain.Models;
using Order.Domain.Validations.Base;
using System.Threading.Tasks;

namespace Order.Application.Applications
{
    public class UserApplication : IUserApplication
    {
        private readonly IUserService _UserService;
        private readonly IMapper _mapper; 

        public UserApplication(IUserService UserService, IMapper mapper)
        {
            _UserService = UserService;
            _mapper = mapper;
        }

        public async Task<Response> CreateAsync(CreateUserRequest User)
        {
            var UserModel = _mapper.Map<UserModel>(User);

            return await _UserService.CreateAsync(UserModel);
        }
    }
}
