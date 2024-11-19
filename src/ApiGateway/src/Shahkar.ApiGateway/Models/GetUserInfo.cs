namespace Shahkar.ApiGateway.Models
{
    public class GetUserInfoQuery
    {
        public Guid Analyser_Id { get; set; }
        public string Phone_Number { get; set; }
        public Request_Details Request_Details { get; set; }

    }
    public class Request_Details
    {
        public string User_Agent { get; set; }
        public string Source_Ip { get; set; }
        public Guid Request_Id { get; set; }
    }
}
