using System;
using Contracts;
using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace WireMockTests.Servers
{
    public class GetUserMock
    {
        public FluentMockServer Server;
        private readonly string _host;
        private readonly Func<string> _getUrl;
        public GetUser.Response ExpectedResponse { get; private set; }
        private string _userId;

        public static GetUserMock WithHost(string host)
        {
            return new GetUserMock(host);
        }

        private GetUserMock(string host)
        {
            this._host = host;
            this._getUrl = () => $"/api/decision/v1";
        }

        public GetUserMock WithUserId(string userId)
        {
            _userId = userId;
            return this;
        }

        public GetUserMock StartServer()
        {
            Server = FluentMockServer.Start(new FluentMockServerSettings { Urls = new[] { this._host }, StartAdminInterface = true });
            Server.Reset();

            ExpectedResponse = new GetUser.Response
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

            Server.Given(Request.Create().UsingPost().WithPath(this._getUrl()).WithBody(new JsonMatcher(
                new
                {
                    UserId = _userId
                }))).RespondWith(Response.Create().WithBodyAsJson(ExpectedResponse));
            return this;
        }

        public void StopServer()
        {
            this.Server.Stop();
        }
    }
}
