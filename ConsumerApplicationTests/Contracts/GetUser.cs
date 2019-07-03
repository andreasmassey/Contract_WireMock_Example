namespace ConsumerApplicationTests.Contracts
{
    public class GetUser
    {
        public class Request
        {
            public string UserId { get; set; }
        }

        public class Response
        {
            public string UserId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public int SSN { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public int StateId { get; set; }
            public int Zip { get; set; }
        }
    }
}
