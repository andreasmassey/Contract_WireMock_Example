using ConsumerApplicationTests.Contracts;
using FluentAssertions;
using WireMockServer.Servers;
using Xunit;


namespace ConsumerApplicationTests.Tests
{
    public class DecisionContractTest
    {
        private readonly string host = "http:\\localhost:1000";
        
        [Fact]
        public void ShouldBeDeepCompare()
        {
            var response = new BeginDecision.Response
            {
                Message = "Message from response",
                NoticeType = "ACCEPTED",
                UserId = "06e4e0c9-f244-4526-b3fb-01301b9cc4bb",
                AccountId = 321,
                DecisionMade = "Declined"
            };

            var decision = BeginDecisionMock.WithHost(host);

            decision.WithDecision("Decision");
            decision.WithDecisionId(123);
            decision.StartServer();

            decision.ExpectedResponse.Should().BeEquivalentTo(response);
            decision.StopServer();
        }
    }
}
