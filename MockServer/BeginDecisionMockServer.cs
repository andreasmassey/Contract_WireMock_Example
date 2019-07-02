using System;
using Contracts;
using WireMock.Matchers;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace MockServer
{
    public class BeginDecisionMockServer
    {
        public FluentMockServer Server;
        private readonly string _host;
        private readonly Func<string> _getUrl;
        public BeginDecision.Response ExpectedResponse { get; private set; }
        private int _decisionId;
        private string _decision;

        public static BeginDecisionMockServer WithHost(string host)
        {
            return new BeginDecisionMockServer(host);
        }

        private BeginDecisionMockServer(string host)
        {
            this._host = host;
            this._getUrl = () => $"/api/decision/v1";
        }

        public BeginDecisionMockServer WithDecisionId(int decisionId)
        {
            _decisionId = decisionId;
            return this;
        }

        public BeginDecisionMockServer WithDecision(string decision)
        {
            _decision = decision;
            return this;
        }

        public BeginDecisionMockServer StartServer()
        {
            Server = FluentMockServer.Start(new FluentMockServerSettings {Urls = new[] {this._host}, StartAdminInterface = true});
            Server.Reset();

            ExpectedResponse = new BeginDecision.Response
            {
                Message = "Message from response",
                NoticeType = "ACCEPTED",
                UserId = Guid.NewGuid().ToString(),
                AccountId = 321,
                DecisionMade = "Declined"
            };

            Server.Given(Request.Create().UsingPost().WithPath(this._getUrl()).WithBody(new JsonMatcher(
            new
            {
                DecisionId = _decisionId,
                Decision = _decision
            }))).RespondWith(Response.Create().WithBodyAsJson(ExpectedResponse));
            return this;
        }

        public void StopServer()
        {
            this.Server.Stop();
        }
    }
}
