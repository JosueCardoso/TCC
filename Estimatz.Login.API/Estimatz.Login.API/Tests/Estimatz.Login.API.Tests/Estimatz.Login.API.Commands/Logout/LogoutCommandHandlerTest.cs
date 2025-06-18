using Estimatz.Login.API.Commands.Logout;
using Estimatz.Login.API.Entities.Token;
using Estimatz.Login.API.Entities.User;
using Estimatz.Login.API.Notifications;
using Estimatz.Login.API.Services.Token;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Estimatz.Login.API.Tests.Estimatz.Login.API.Commands.Logout
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
                .Returns(Task.FromResult<ApplicationUser>(new ApplicationUser { Email = "Email"}));

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
            notificationService.IsSucess.Should().BeTrue();
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
            notificationService.IsSucess.Should().BeTrue();
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
            notificationService.IsSucess.Should().BeFalse();
            notificationService.Messages.First().Description.Should().Be("Token informado para logout está invalido");
        }
    }
}
