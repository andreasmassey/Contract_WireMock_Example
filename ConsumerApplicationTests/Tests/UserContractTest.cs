using ConsumerApplicationTests.Contracts;
using FluentAssertions;
using WireMockServer.Servers;
using Xunit;

namespace ConsumerApplicationTests.Tests
{
    public class UserContractTest
    {
        private readonly string host = "http:\\localhost:1001";

        [Fact]
        public void ShouldBeDeepCompare()
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

            var user = GetUserMock.WithHost(host);

            user.WithUserId("06e4e0c9-f244-4526-b3fb-01301b9cc4bb");
            user.WithAccountId(1234);
            user.StartServer();

            user.ExpectedResponse.Should().BeEquivalentTo(response);
            user.StopServer();
        }
    }
}
