using Order.Application.DataContract.Response.User;
using Order.Domain.Models;
using System.Threading.Tasks;

namespace Order.Application.Interfacds.Security
{
    public interface ITokenManager
    {
        Task<AuthResponse> GenerateTokenAsync(UserModel user);
    }
}
