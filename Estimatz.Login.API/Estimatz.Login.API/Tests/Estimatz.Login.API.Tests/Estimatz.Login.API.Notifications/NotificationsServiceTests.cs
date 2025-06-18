using Estimatz.Login.API.Entities.Notification;
using Estimatz.Login.API.Notifications;
using FluentAssertions;

namespace Estimatz.Login.API.Tests.Estimatz.Login.API.Notifications
{
    public class NotificationsServiceTests
    {
        [Fact]
        public void DeveNotificar()
        {
            //arrange
            var notificationService = new NotificationsService();
            var notification = new Notification(success: true);

            //act
            notificationService.Notify(notification);

            //assert
            notificationService.HasNotification.Should().BeTrue();
            notificationService.IsSucess.Should().BeTrue();
            notificationService.HasMessages.Should().BeFalse();
        }

        [Fact]
        public void NaoDeveNotificar()
        {
            //arrange
            var notificationService = new NotificationsService();

            //act

            //assert
            notificationService.HasNotification.Should().BeFalse();
            notificationService.IsSucess.Should().BeFalse();
            notificationService.HasMessages.Should().BeFalse();
        }
    }
}
