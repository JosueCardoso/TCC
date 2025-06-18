using Estimatz.Entities.Notification;

namespace Estimatz.Notifications
{
    public class NotificationsService : INotificator
    {
        private Notification _notification;

        public List<Message> Messages => HasNotification ? _notification.Messages : new List<Message>();
        public bool HasNotification =>  _notification is not null;
        public bool HasMessages => HasNotification && _notification.Messages.Any();
        public bool IsSuccess => HasNotification && _notification.Success;

        public void Notify(Notification notification)
        {
            _notification = notification;
        }
    }
}
