using Estimatz.Login.API.Commands.RefreshToken;
using Estimatz.Login.API.Entities.Token;
using Estimatz.Login.API.Entities.User;
using Estimatz.Login.API.Notifications;
using Estimatz.Login.API.Services.Token;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Estimatz.Login.API.Tests.Estimatz.Login.API.Commands.RefreshToken
{
    public class RefreshTokenCommandHandlerTest
    {
        [Fact]
        public async void DeveRealizarRefreshDoToken()
        {
            //arrange
            var command = new RefreshTokenCommand
            {
                Email = "email",
                TokenString = "tokenString"
            };

            var loggerMock = Substitute.For<ILogger<RefreshTokenCommandHandler>>();

            var tokenManagerMock = Substitute.For<ITokenManager>();
            tokenManagerMock.IsValidToken(Arg.Any<string>())
                .Returns(false);
            tokenManagerMock.RefreshToken(Arg.Any<RefreshTokenRequestModel>())
                .Returns(new SignInUser());

            var notificatorService = new NotificationsService();
            var commandHandler = new RefreshTokenCommandHandler(tokenManagerMock, notificatorService, loggerMock);

            //act
            await commandHandler.Handle(command, CancellationToken.None);

            //assert
            tokenManagerMock.Received(1).IsValidToken(Arg.Any<string>());
            await tokenManagerMock.Received(0).RefreshToken(Arg.Any<RefreshTokenRequestModel>());
            notificatorService.IsSucess.Should().BeFalse();
        }

        [Fact]
        public async void NaoDeveRealizarRefreshDoTokenAsync()
        {
            // arrange
            var command = new RefreshTokenCommand
            {
                Email = "email",
                TokenString = "tokenString"
            };

            var loggerMock = Substitute.For<ILogger<RefreshTokenCommandHandler>>();

            var tokenManagerMock = Substitute.For<ITokenManager>();
            tokenManagerMock.IsValidToken(Arg.Any<string>())
                .Returns(true);
            tokenManagerMock.RefreshToken(Arg.Any<RefreshTokenRequestModel>())
                .Returns(new SignInUser());

            var notificatorService = new NotificationsService();
            var commandHandler = new RefreshTokenCommandHandler(tokenManagerMock, notificatorService, loggerMock);

            //act
            await commandHandler.Handle(command, CancellationToken.None);

            //assert
            tokenManagerMock.Received(1).IsValidToken(Arg.Any<string>());
            await tokenManagerMock.Received(1).RefreshToken(Arg.Any<RefreshTokenRequestModel>());
            notificatorService.IsSucess.Should().BeTrue();
        }
    }
}
