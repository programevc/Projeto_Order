using Order.Domain.Models;
using Order.Domain.Validations.Base;
using System.Threading.Tasks;

namespace Order.Domain.Interfaces.Services
{
    public interface ISecurityService
    {
        Task<Response<bool>> ComparePassword(string password, string confirmPassword);
        Task<Response<bool>> VerifyPassword(string password, UserModel user);
        Task<Response<string>> EncryptPassword(string password);
    }
}
