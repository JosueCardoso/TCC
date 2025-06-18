namespace Estimatz.UI.ExternalServices.EstimatzLoginApi.Common
{
    public class CommonResponse
    {
        public bool Success { get; set; }
        public List<Message> Messages { get; set; }
    }

    public class Message
    {
        public string? Code { get; set; }
        public string Description { get; set; }
    }
}
