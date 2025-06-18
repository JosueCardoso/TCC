using Estimatz.Commands.Account.Register;
using Estimatz.Entities.User;
using Estimatz.Events.NewUserConfirmationEmail;
using Estimatz.Notifications;
using Estimatz.Services.Token;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Estimatz.Test.UnitTest.Estimatz.Commands.Account.Register
{
    public class RegisterNewUserCommandHandlerTests
    {
        [Fact]
        public async Task DeveRegistrarNovoUsuarioComSucessoAsync()
        {
            //arrange
            var command = new RegisterNewUserCommand
            {
                Username = "Teste",
                Email = "Teste@teste.com",
                Password = "1234",
                ConfirmPassword = "1234"
            };

            var mockUserStore = Substitute.For<IUserStore<ApplicationUser>>();
            var loggerMock = Substitute.For<ILogger<RegisterNewUserCommandHandler>>();

            var userManagerMock = Substitute.For<UserManager<ApplicationUser>>(mockUserStore, null, null, null, null, null, null, null, null);
            userManagerMock.CreateAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>())
                .Returns(Task.FromResult(IdentityResult.Success).Result);

            var tokenManagerMock = Substitute.For<ITokenManager>();
            tokenManagerMock.GenerateConfirmEmailTokenAsync(Arg.Any<ApplicationUser>()).Returns("");

            var mediatrMock = Substitute.For<IMediator>();
            await mediatrMock.Publish(Arg.Any<NewUserConfirmationEmailEvent>());

            var notificationService = new NotificationsService();
            var commandHandler = new RegisterNewUserCommandHandler(userManagerMock, tokenManagerMock, mediatrMock, notificationService, loggerMock);

            //act
            await commandHandler.Handle(command, CancellationToken.None);

            //assert
            await tokenManagerMock.Received(1).GenerateConfirmEmailTokenAsync(Arg.Any<ApplicationUser>());
            await mediatrMock.Received(1).Publish(Arg.Any<NewUserConfirmationEmailEvent>());
            await userManagerMock.Received(1).CreateAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>());
            notificationService.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task NaoDeveRegistrarNovoUsuario()
        {
            //arrange
            var command = new RegisterNewUserCommand
            {
                Username = "Teste",
                Email = "Teste@teste.com",
                Password = "1234",
                ConfirmPassword = "1234"
            };

            var mockUserStore = Substitute.For<IUserStore<ApplicationUser>>();
            var loggerMock = Substitute.For<ILogger<RegisterNewUserCommandHandler>>();

            var userManagerMock = Substitute.For<UserManager<ApplicationUser>>(mockUserStore, null, null, null, null, null, null, null, null);
            userManagerMock.CreateAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>())
                .Returns(Task.FromResult(IdentityResult.Failed()).Result);

            var tokenManagerMock = Substitute.For<ITokenManager>();
            tokenManagerMock.GenerateConfirmEmailTokenAsync(Arg.Any<ApplicationUser>()).Returns("");

            var mediatrMock = Substitute.For<IMediator>();
            await mediatrMock.Publish(Arg.Any<NewUserConfirmationEmailEvent>());

            var notificationService = new NotificationsService();
            var commandHandler = new RegisterNewUserCommandHandler(userManagerMock, tokenManagerMock, mediatrMock, notificationService, loggerMock);

            //act
            var result = commandHandler.Handle(command, CancellationToken.None);

            //assert
            await tokenManagerMock.Received(0).GenerateConfirmEmailTokenAsync(Arg.Any<ApplicationUser>());
            await mediatrMock.Received(0).Publish(Arg.Any<NewUserConfirmationEmailEvent>());
            await userManagerMock.Received(1).CreateAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>());
            notificationService.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public async Task NaoDeveRegistrarNovoUsuarioDadoSenhasDiferentes()
        {
            //arrange
            var command = new RegisterNewUserCommand
            {
                Username = "Teste",
                Email = "Teste@teste.com",
                Password = "1234",
                ConfirmPassword = "12345"
            };

            var mockUserStore = Substitute.For<IUserStore<ApplicationUser>>();
            var loggerMock = Substitute.For<ILogger<RegisterNewUserCommandHandler>>();

            var userManagerMock = Substitute.For<UserManager<ApplicationUser>>(mockUserStore, null, null, null, null, null, null, null, null);
            userManagerMock.CreateAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>())
                .Returns(Task.FromResult(IdentityResult.Failed()).Result);

            var tokenManagerMock = Substitute.For<ITokenManager>();
            tokenManagerMock.GenerateConfirmEmailTokenAsync(Arg.Any<ApplicationUser>()).Returns("");

            var mediatrMock = Substitute.For<IMediator>();
            await mediatrMock.Publish(Arg.Any<NewUserConfirmationEmailEvent>());

            var notificationService = new NotificationsService();
            var commandHandler = new RegisterNewUserCommandHandler(userManagerMock, tokenManagerMock, mediatrMock, notificationService, loggerMock);

            //act
            var result = commandHandler.Handle(command, CancellationToken.None);

            //assert
            await tokenManagerMock.Received(0).GenerateConfirmEmailTokenAsync(Arg.Any<ApplicationUser>());
            await mediatrMock.Received(0).Publish(Arg.Any<NewUserConfirmationEmailEvent>());
            await userManagerMock.Received(0).CreateAsync(Arg.Any<ApplicationUser>(), Arg.Any<string>());
            notificationService.IsSuccess.Should().BeFalse();
        }
    }
}
