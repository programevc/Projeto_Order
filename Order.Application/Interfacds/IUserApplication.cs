using Order.Application.DataContract.Request.User;
using Order.Domain.Validations.Base;
using System.Threading.Tasks;

namespace Order.Application.Interfacds
{
    public interface IUserApplication
    {
        Task<Response> CreateAsync(CreateUserRequest User);
    }
}
