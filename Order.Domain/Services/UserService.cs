using Order.Domain.Common;
using Order.Domain.Interfaces.Repositories;
using Order.Domain.Interfaces.Services;
using Order.Domain.Models;
using Order.Domain.Validations;
using Order.Domain.Validations.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Order.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _UserRepository;
        private readonly ITimeProvider _timeProvider;
        private readonly IGenerators _generators;
        public UserService(IUserRepository UserRepository,
                           ITimeProvider timeProvider,
                           IGenerators generators)
        {
            _UserRepository = UserRepository;
            _timeProvider = timeProvider;
            _generators = generators;
        }
        public Task<Response<bool>> AutheticationAsync(UserModel user)
        {
            throw new NotImplementedException();
        }

        public async Task<Response> CreateAsync(UserModel user)
        {
            var response = new Response();

            var validation = new UserValidation();
            var errors = validation.Validate(user).GetErrors();

            if (errors.Report.Count > 0)
                return errors;

            user.Id = _generators.Generate();
            user.CreatedAt = _timeProvider.utcDateTime();

            await _UserRepository.CreateAsync(user);

            return response;
        }

        public async Task<Response> DeleteAsync(string userId)
        {
            var response = new Response();

            var exists = await _UserRepository.ExistsByIdAsync(userId);

            if (!exists)
            {
                response.Report.Add(Report.Create($"User {userId} not exists!"));
                return response;
            }

            await _UserRepository.DeleteAsync(userId);

            return response;
        }

        public async Task<Response<UserModel>> GetByIdAsync(string userId)
        {
            var response = new Response<UserModel>();

            var exists = await _UserRepository.ExistsByIdAsync(userId);

            if (!exists)
            {
                response.Report.Add(Report.Create($"User {userId} not exists!"));
                return response;
            }

            var data = await _UserRepository.GetByIdAsync(userId);
            response.Data = data;
            return response;
        }

        public async Task<Response<List<UserModel>>> ListByFilterAsync(string userId = null, string name = null)
        {
            var response = new Response<List<UserModel>>();

            if (!string.IsNullOrWhiteSpace(userId))
            {
                var exists = await _UserRepository.ExistsByIdAsync(userId);

                if (!exists)
                {
                    response.Report.Add(Report.Create($"User {userId} not exists!"));
                    return response;
                }
            }

            var data = await _UserRepository.ListByFilterAsync(userId, name);
            response.Data = data;

            return response;
        }

        public async Task<Response> UpdateAsync(UserModel user)
        {
            var response = new Response();

            var validation = new UserValidation();
            var errors = validation.Validate(user).GetErrors();

            if (errors.Report.Count > 0)
                return errors;

            var exists = await _UserRepository.ExistsByIdAsync(user.Id);

            if (!exists)
            {
                response.Report.Add(Report.Create($"User {user.Id} not exists!"));
                return response;
            }

            await _UserRepository.UpdateAsync(user);

            return response;
        }
    }
}
