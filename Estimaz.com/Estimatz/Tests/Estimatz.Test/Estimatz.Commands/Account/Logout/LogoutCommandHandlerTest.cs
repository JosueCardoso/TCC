using Estimatz.Commands.Account.Logout;
using Estimatz.Entities.Token;
using Estimatz.Entities.User;
using Estimatz.Notifications;
using Estimatz.Services.Token;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Estimatz.Test.UnitTest.Estimatz.Commands.Account.Logout
{
    public class LogoutCommandHandlerTest
    {
        [Fact]
        public async Task DeveRealizarLogoutDadoUsuarioValidoAsync()
        {
            //arrange
            var command = new LogoutCommand
            {
                Token = "token"
            };

            var simpleToken = new SimpleToken
            {
                TokenString = "token",
                ExpireAt = DateTime.UtcNow,
                UserId = "userId"
            };

            var mockUserStore = Substitute.For<IUserStore<ApplicationUser>>();
            var loggerMock = Substitute.For<ILogger<LogoutCommandHandler>>();

            var userManagerMock = Substitute.For<UserManager<ApplicationUser>>(mockUserStore, null, null, null, null, null, null, null, null);
            userManagerMock.FindByIdAsync(Arg.Any<string>())
                .Returns(Task.FromResult(new ApplicationUser { Email = "Email" }));

            var tokenManagerMock = Substitute.For<ITokenManager>();
            tokenManagerMock.InvalidToken(Arg.Any<string>());
            tokenManagerMock.IsValidToken(Arg.Any<string>())
                .Returns(true);
            tokenManagerMock.GetSimpleToken(Arg.Any<string>())
                .Returns(simpleToken);

            var notificationService = new NotificationsService();
            var commandHandler = new LogoutCommandHandler(userManagerMock, tokenManagerMock, notificationService, loggerMock);

            //act
            await commandHandler.Handle(command, CancellationToken.None);

            //assert
            tokenManagerMock.Received(1).GetSimpleToken(Arg.Any<string>());
            await userManagerMock.Received(1).FindByIdAsync(Arg.Any<string>());
            notificationService.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task DeveRealizarLogoutDadoUsuarioNuloAsync()
        {
            //arrange
            var command = new LogoutCommand
            {
                Token = "token"
            };

            var simpleToken = new SimpleToken
            {
                TokenString = "token",
                ExpireAt = DateTime.UtcNow,
                UserId = "userId"
            };

            var mockUserStore = Substitute.For<IUserStore<ApplicationUser>>();
            var loggerMock = Substitute.For<ILogger<LogoutCommandHandler>>();

            var userManagerMock = Substitute.For<UserManager<ApplicationUser>>(mockUserStore, null, null, null, null, null, null, null, null);
            userManagerMock.FindByIdAsync(Arg.Any<string>())
                .Returns(Task.FromResult<ApplicationUser>(null));

            var tokenManagerMock = Substitute.For<ITokenManager>();
            tokenManagerMock.InvalidToken(Arg.Any<string>());
            tokenManagerMock.IsValidToken(Arg.Any<string>())
                .Returns(true);
            tokenManagerMock.GetSimpleToken(Arg.Any<string>())
                .Returns(simpleToken);

            var notificationService = new NotificationsService();
            var commandHandler = new LogoutCommandHandler(userManagerMock, tokenManagerMock, notificationService, loggerMock);

            //act
            await commandHandler.Handle(command, CancellationToken.None);

            //assert
            tokenManagerMock.Received(1).GetSimpleToken(Arg.Any<string>());
            await userManagerMock.Received(1).FindByIdAsync(Arg.Any<string>());
            notificationService.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task NaoDeveRealizarLogoutDadoTokenInvalidoAsync()
        {
            //arrange
            var command = new LogoutCommand
            {
                Token = "token"
            };

            var simpleToken = new SimpleToken
            {
                TokenString = "token",
                ExpireAt = DateTime.UtcNow,
                UserId = "userId"
            };

            var mockUserStore = Substitute.For<IUserStore<ApplicationUser>>();
            var loggerMock = Substitute.For<ILogger<LogoutCommandHandler>>();

            var userManagerMock = Substitute.For<UserManager<ApplicationUser>>(mockUserStore, null, null, null, null, null, null, null, null);
            userManagerMock.FindByIdAsync(Arg.Any<string>())
                .Returns(Task.FromResult<ApplicationUser>(null));

            var tokenManagerMock = Substitute.For<ITokenManager>();
            tokenManagerMock.InvalidToken(Arg.Any<string>());
            tokenManagerMock.IsValidToken(Arg.Any<string>())
                .Returns(false);
            tokenManagerMock.GetSimpleToken(Arg.Any<string>())
                .Returns(simpleToken);

            var notificationService = new NotificationsService();
            var commandHandler = new LogoutCommandHandler(userManagerMock, tokenManagerMock, notificationService, loggerMock);

            //act
            await commandHandler.Handle(command, CancellationToken.None);

            //assert
            notificationService.IsSuccess.Should().BeFalse();
            notificationService.Messages.First().Description.Should().Be("Token informado para logout está invalido");
        }
    }
}
