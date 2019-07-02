using Contracts;
using System;
using System.Threading.Tasks;

namespace ContractWithWireMock.Services
{
    public class DecisionService : IDecisionService
    {
        public async Task<BeginDecision.Response> GetDecision(BeginDecision.Request request)
        {
            var response = new BeginDecision.Response
            {
                Message = "Word to the wise... don't eat yellow snow!!!",
                NoticeType = "Advice",
                UserId = Guid.NewGuid().ToString(),
                AccountId = 421,
                DecisionMade = "Approved"
            };

            return await Task.Run(() => response);
        }
    }
}
