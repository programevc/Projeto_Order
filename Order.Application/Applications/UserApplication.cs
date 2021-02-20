using AutoMapper;
using Order.Application.DataContract.Request.User;
using Order.Application.DataContract.Response.Client;
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
    public class UserApplication : IUserApplication
    {
        private readonly IUserService _UserService;
        private readonly IMapper _mapper;
        private readonly ISecurityService _securityService;

        public UserApplication(IUserService UserService, IMapper mapper, ISecurityService securityService)
        {
            _UserService = UserService;
            _mapper = mapper;
            _securityService = securityService;
        }

        public async Task<Response> CreateAsync(CreateUserRequest User)
        {
            try
            {
                var isEquals = await _securityService.ComparePassword(User.Password, User.ConfirmPassword);

                if(!isEquals.Data)
                    return Response.Unprocessable(Report.Create("Passwords do not match"));

                var passwordEncripted = await _securityService.EncryptPassword(User.Password);

                User.Password = passwordEncripted.Data;

                var UserModel = _mapper.Map<UserModel>(User);

                return await _UserService.CreateAsync(UserModel);
            }
            catch (Exception ex)
            {
                var response = Report.Create(ex.Message);

                return Response.Unprocessable(response);
            }
        }

        public async Task<Response<List<UserResponse>>> ListByFilterAsync(string userId = null, string name = null)
        {
            try
            {
                Response<List<UserModel>> user = await _UserService.ListByFilterAsync(userId, name);

                if (user.Report.Any())
                    return Response.Unprocessable<List<UserResponse>>(user.Report);

                var response = _mapper.Map<List<UserResponse>>(user.Data);

                return Response.OK(response);
            }
            catch (Exception ex)
            {
                var response = Report.Create(ex.Message);

                return Response.Unprocessable<List<UserResponse>>(new List<Report>() { response });
            }
        }
    }
}
