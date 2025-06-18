using Estimatz.Commands.Account.RefreshToken;
using Estimatz.Entities.Token;
using Estimatz.Entities.User;
using Estimatz.Notifications;
using Estimatz.Services.Token;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Estimatz.Test.UnitTest.Estimatz.Commands.Account.RefreshToken
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
            notificatorService.IsSuccess.Should().BeFalse();
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
            notificatorService.IsSuccess.Should().BeTrue();
        }
    }
}
