using Contracts;
using System.Threading.Tasks;

namespace ContractWithWireMock.Services
{
    public interface IDecisionService
    {
        Task<BeginDecision.Response> GetDecision(BeginDecision.Request request);
    }
}
