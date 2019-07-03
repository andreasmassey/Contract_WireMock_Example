namespace ConsumerApplicationTests.Contracts
{
    //This class MUST contain POCO objects.
    public class BeginDecision
    {
        public class Response
        {
            public string Message { get; set; }
            public string NoticeType { get; set; }
            public string UserId { get; set; }
            public int AccountId { get; set; }
            public string DecisionMade { get; set; }
        }

        public class Request
        {
            public int DecisionId { get; set; }
            public string Decision { get; set; }
        }
    }
}
