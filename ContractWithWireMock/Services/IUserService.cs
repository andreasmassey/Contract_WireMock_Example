using System.Threading.Tasks;
using Contracts;

namespace ContractWithWireMock.Services
{
    public interface IUserService
    {
        Task<GetUser.Response> GetUser(GetUser.Request request);
    }
}