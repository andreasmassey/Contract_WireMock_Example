using System.Threading.Tasks;
using Contracts;

namespace ContractWithWireMock.Services
{
    public class UserService : IUserService
    {
        public async Task<GetUser.Response> GetUser(GetUser.Request request)
        {
            var response = new GetUser.Response
            {
                UserId = "06e4e0c9-f244-4526-b3fb-01301b9cc4bb",
                FirstName = "John",
                LastName = "Smith",
                Email = "John@Smith.com",
                SSN = 123121234,
                Address = "123 Main Street",
                City = "Atlanta",
                StateId = 10,
                Zip = 90024
            };

            return await Task.Run(() => response);
        }
    }
}