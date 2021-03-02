using AutoMapper;
using Order.Application.DataContract.Request.User;
using Order.Application.DataContract.Response.Client;
using Order.Application.DataContract.Response.User;
using Order.Application.Interfacds;
using Order.Application.Interfacds.Security;
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
        private readonly ITokenManager _tokenManager;

        public UserApplication(IUserService UserService, IMapper mapper, ISecurityService securityService, ITokenManager tokenManager)
        {
            _UserService = UserService;
            _mapper = mapper;
            _securityService = securityService;
            _tokenManager = tokenManager;
        }

        public async Task<Response<AuthResponse>> AuthAsync(AuthRequest auth)
        {
            var user = await _UserService.GetByLoginAsync(auth.Login);

            if (user.Report.Any())
                return Response.Unprocessable<AuthResponse>(user.Report);

            var isAuthenticated = await _UserService.AutheticationAsync(auth.Password, user.Data);

            if (!isAuthenticated.Data)
                return Response.Unprocessable<AuthResponse>(new List<Report>() { Report.Create("Invalid password or login") });

            var token = await _tokenManager.GenerateTokenAsync(user.Data);

            return new Response<AuthResponse>(token);
        }

        public async Task<Response> CreateAsync(CreateUserRequest User)
        {
            try
            {
                var isEquals = await _securityService.ComparePassword(User.Password, User.ConfirmPassword);

                if (!isEquals.Data)
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
