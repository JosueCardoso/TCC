using Estimatz.Entities.Notification;

namespace Estimatz.Notifications
{
    public interface INotificator
    {
        bool HasNotification { get; }
        bool HasMessages { get; }
        bool IsSuccess { get; }        
        List<Message> Messages { get; }

        void Notify(Notification notification);
    }
}
