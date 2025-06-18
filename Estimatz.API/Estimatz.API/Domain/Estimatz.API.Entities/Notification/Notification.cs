namespace Estimatz.API.Entities.Notification
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
}
