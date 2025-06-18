using Estimatz.Commands.Account.ConfirmEmail;
using Estimatz.Entities.User;
using Estimatz.Notifications;
using Estimatz.Services.Token;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Estimatz.Test.UnitTest.Estimatz.Commands.Account.ConfirmEmail
{
    public class ConfirmEmailCommandHandlerTest
    {
        [Fact]
        public async Task NaoDeveConfirmarEmailDadoUsuarioNaoEncontradoAsync()
        {
            //arrange
            var command = new ConfirmEmailCommand
            {
                Token = "Token",
                UserId = "UserId"
            };

            var mockUserStore = Substitute.For<IUserStore<ApplicationUser>>();
            var loggerMock = Substitute.For<ILogger<ConfirmEmailCommandHandler>>();

            var userManagerMock = Substitute.For<UserManager<ApplicationUser>>(mockUserStore, null, null, null, null, null, null, null, null);
            userManagerMock.FindByIdAsync(Arg.Any<string>())
                .Returns(Task.FromResult<ApplicationUser>(null));

            var tokenManagerMock = Substitute.For<ITokenManager>();
            tokenManagerMock.InvalidToken(Arg.Any<string>());
            tokenManagerMock.IsValidToken(Arg.Any<string>())
                .Returns(true);

            var notificationService = new NotificationsService();
            var commandHandler = new ConfirmEmailCommandHandler(userManagerMock, tokenManagerMock, notificationService, loggerMock);

            //act
            await commandHandler.Handle(command, CancellationToken.None);

            //assert
            await userManagerMock.Received(1).FindByIdAsync(Arg.Any<string>());
            notificationService.IsSuccess.Should().BeFalse();
            notificationService.Messages.First().Description.Should().Be("Não foi possível confirmar o e-mail");
        }

        [Fact]
        public async Task NaoDeveConfirmarEmailDadoResultadoSemSucessoDoIdentity()
        {
            //arrange
            var command = new ConfirmEmailCommand
            {
                Token = "Token",
                UserId = "UserId"
            };

            var mockUserStore = Substitute.For<IUserStore<ApplicationUser>>();
            var loggerMock = Substitute.For<ILogger<ConfirmEmailCommandHandler>>();

            var userManagerMock = Substitute.For<UserManager<ApplicationUser>>(mockUserStore, null, null, null, null, null, null, null, null);
            userManagerMock.FindByIdAsync(Arg.Any<string>())
                .Returns(Task.FromResult(new ApplicationUser { Email = "teste@teste.com" }));
            userManagerMock.ConfirmEmailAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>())
                .Returns(Task.FromResult(IdentityResult.Failed()).Result);

            var tokenManagerMock = Substitute.For<ITokenManager>();
            tokenManagerMock.InvalidToken(Arg.Any<string>());
            tokenManagerMock.IsValidToken(Arg.Any<string>())
                .Returns(true);

            var notificationService = new NotificationsService();
            var commandHandler = new ConfirmEmailCommandHandler(userManagerMock, tokenManagerMock, notificationService, loggerMock);

            //act
            await commandHandler.Handle(command, CancellationToken.None);

            //assert           
            await userManagerMock.Received(1).FindByIdAsync(Arg.Any<string>());
            await userManagerMock.Received(1).ConfirmEmailAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>());
            notificationService.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task DeveConfirmarEmail()
        {
            //arrange
            var command = new ConfirmEmailCommand
            {
                Token = "Token",
                UserId = "UserId"
            };

            var mockUserStore = Substitute.For<IUserStore<ApplicationUser>>();
            var loggerMock = Substitute.For<ILogger<ConfirmEmailCommandHandler>>();

            var userManagerMock = Substitute.For<UserManager<ApplicationUser>>(mockUserStore, null, null, null, null, null, null, null, null);
            userManagerMock.FindByIdAsync(Arg.Any<string>())
                .Returns(Task.FromResult(new ApplicationUser { Email = "teste@teste.com" }));
            userManagerMock.ConfirmEmailAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>())
                .Returns(Task.FromResult(IdentityResult.Success).Result);

            var tokenManagerMock = Substitute.For<ITokenManager>();
            tokenManagerMock.InvalidToken(Arg.Any<string>());
            tokenManagerMock.IsValidToken(Arg.Any<string>())
                .Returns(true);

            var notificationService = new NotificationsService();
            var commandHandler = new ConfirmEmailCommandHandler(userManagerMock, tokenManagerMock, notificationService, loggerMock);

            //act
            await commandHandler.Handle(command, CancellationToken.None);

            //assert
            tokenManagerMock.Received(1).InvalidToken(Arg.Any<string>());
            await userManagerMock.Received(1).FindByIdAsync(Arg.Any<string>());
            notificationService.IsSuccess.Should().BeTrue();
            notificationService.Messages.First().Description.Should().Be("E-mail confirmado com sucesso");
        }
    }
}
