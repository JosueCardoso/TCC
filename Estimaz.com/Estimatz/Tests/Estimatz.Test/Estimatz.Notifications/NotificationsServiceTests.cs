using Estimatz.Entities.Notification;
using Estimatz.Notifications;
using FluentAssertions;

namespace Estimatz.Test.UnitTest.Estimatz.Notifications
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
            notificationService.IsSuccess.Should().BeTrue();
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
            notificationService.IsSuccess.Should().BeFalse();
            notificationService.HasMessages.Should().BeFalse();
        }
    }
}
