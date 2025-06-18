using Estimatz.API.Entities.Notification;

namespace Estimatz.API.Notifications
{
    public interface INotificator
    {
        bool HasNotification { get; }
        bool HasMessages { get; }
        bool IsSucess { get; }
        List<Message> Messages { get; }

        void Notify(Notification notification);
    }
}
