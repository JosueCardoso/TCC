namespace Estimatz.API.Entities.Notification
{
    public class Message
    {
        public Message(string description)
        {
            Description = description;
        }

        public Message(string code, string description)
        {
            Code = code;
            Description = description;
        }

        public string? Code { get; set; }
        public string Description { get; set; }
    }
}
