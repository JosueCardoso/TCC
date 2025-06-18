namespace Estimatz.Login.API.Entities.Notification
{
    public class Notification
    {
        public Notification(bool success, Message message)
        {
            Success = success;
            Messages = new List<Message> { message };
        }

        public Notification(bool success)
        {
            Success = success;
            Messages = new List<Message>();
        }

        public bool Success { get; }
        public List<Message> Messages { get; }

        public void AddMessage(Message message)
        {
            Messages.Add(message);
        }
    }

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
