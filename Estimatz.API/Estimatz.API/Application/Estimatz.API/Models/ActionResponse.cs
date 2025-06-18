namespace Estimatz.API.Models
{
    public class ActionResponse
    {
        public ActionResponse(bool success, object data, object messages)
        {
            Success = success;
            Data = data;
            Messages = messages;
        }

        public ActionResponse(bool success, object messages)
        {
            Success = success;
            Messages = messages;
        }

        public bool Success { get; set; }
        public object? Data { get; set; }
        public object? Messages { get; set; }
    }
}
