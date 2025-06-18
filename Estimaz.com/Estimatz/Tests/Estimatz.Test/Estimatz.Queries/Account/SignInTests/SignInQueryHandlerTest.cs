using Estimatz.Entities.User;
using Estimatz.Notifications;
using Estimatz.Queries.Account.SignIn;
using Estimatz.Services.Token;
using Estimatz.Test.Fakes;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Estimatz.Test.UnitTest.Estimatz.Queries.Account.SignInTests
{
    public class SignInQueryHandlerTest
    {
        [Fact]
        public async Task NaoDeveLogarDadoUsuarioNaoEncontrado()
        {
            //arrange
            var query = new SignInQuery
            {
                Email = "Email",
                Password = "Password"
            };

            var mockUserStore = Substitute.For<IUserStore<ApplicationUser>>();
            var loggerMock = Substitute.For<ILogger<SignInQueryHandler>>();

            var userManagerMock = Substitute.For<UserManager<ApplicationUser>>(mockUserStore, null, null, null, null, null, null, null, null);
            userManagerMock.FindByEmailAsync(Arg.Any<string>())
               .Returns(Task.FromResult<ApplicationUser>(null).Result);
            userManagerMock.IsEmailConfirmedAsync(Arg.Any<ApplicationUser>())
                .Returns(false);

            var signInManagerMock = Substitute.For<FakeSignInManager>(userManagerMock, false);

            var tokenManagerMock = Substitute.For<ITokenManager>();
            tokenManagerMock.GenerateConfirmEmailTokenAsync(Arg.Any<ApplicationUser>()).Returns("");

            var notificationService = new NotificationsService();
            var queryHandler = new SignInQueryHandler(userManagerMock, signInManagerMock, tokenManagerMock, notificationService, loggerMock);

            //act
            var result = await queryHandler.Handle(query, CancellationToken.None);

            //assert
            result.Should().BeEquivalentTo(new SignInUser());
            await userManagerMock.Received(1).FindByEmailAsync(Arg.Any<string>());
            await userManagerMock.Received(0).IsEmailConfirmedAsync(Arg.Any<ApplicationUser>());
            await signInManagerMock.Received(0).PasswordSignInAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<bool>());
            notificationService.IsSuccess.Should().BeFalse();
            notificationService.Messages.First().Description.Should().Be("Usuário ou senha incorretos");
        }

        [Fact]
        public async Task NaoDeveLogarDadoEmailNaoConfirmadoAsync()
        {
            //arrange
            var query = new SignInQuery
            {
                Email = "Email",
                Password = "Password"
            };

            var mockUserStore = Substitute.For<IUserStore<ApplicationUser>>();
            var loggerMock = Substitute.For<ILogger<SignInQueryHandler>>();

            var userManagerMock = Substitute.For<UserManager<ApplicationUser>>(mockUserStore, null, null, null, null, null, null, null, null);
            userManagerMock.FindByEmailAsync(Arg.Any<string>())
               .Returns(Task.FromResult(new ApplicationUser()).Result);
            userManagerMock.IsEmailConfirmedAsync(Arg.Any<ApplicationUser>())
                .Returns(false);

            var signInManagerMock = Substitute.For<FakeSignInManager>(userManagerMock, false);

            var tokenManagerMock = Substitute.For<ITokenManager>();
            tokenManagerMock.GenerateConfirmEmailTokenAsync(Arg.Any<ApplicationUser>()).Returns("");

            var notificationService = new NotificationsService();
            var queryHandler = new SignInQueryHandler(userManagerMock, signInManagerMock, tokenManagerMock, notificationService, loggerMock);

            //act
            var result = await queryHandler.Handle(query, CancellationToken.None);

            //assert
            result.Should().BeEquivalentTo(new SignInUser());
            await userManagerMock.Received(1).FindByEmailAsync(Arg.Any<string>());
            await userManagerMock.Received(1).IsEmailConfirmedAsync(Arg.Any<ApplicationUser>());
            await signInManagerMock.Received(0).PasswordSignInAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<bool>());
            notificationService.IsSuccess.Should().BeFalse();
            notificationService.Messages.First().Description.Should().Be("E-mail não confirmado. Verifique sua caixa de entrada e confirme seu e-mail");
        }

        [Fact]
        public async Task NaoDeveLogarDadoEmailInvalido()
        {
            //arrange
            var query = new SignInQuery
            {
                Email = "Email",
                Password = "Password"
            };

            var mockUserStore = Substitute.For<IUserStore<ApplicationUser>>();
            var loggerMock = Substitute.For<ILogger<SignInQueryHandler>>();

            var userManagerMock = Substitute.For<UserManager<ApplicationUser>>(mockUserStore, null, null, null, null, null, null, null, null);
            userManagerMock.FindByEmailAsync(Arg.Any<string>())
               .Returns(Task.FromResult(new ApplicationUser()).Result);
            userManagerMock.IsEmailConfirmedAsync(Arg.Any<ApplicationUser>())
                .Returns(true);

            var signInManagerMock = Substitute.For<FakeSignInManager>(userManagerMock, false);
            signInManagerMock.PasswordSignInAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<bool>())
                .Returns(Task.FromResult(SignInResult.Failed).Result);

            var tokenManagerMock = Substitute.For<ITokenManager>();
            tokenManagerMock.GenerateConfirmEmailTokenAsync(Arg.Any<ApplicationUser>()).Returns("");

            var notificationService = new NotificationsService();
            var queryHandler = new SignInQueryHandler(userManagerMock, signInManagerMock, tokenManagerMock, notificationService, loggerMock);

            //act
            var result = await queryHandler.Handle(query, CancellationToken.None);

            //assert
            result.Should().BeEquivalentTo(new SignInUser());
            await userManagerMock.Received(1).FindByEmailAsync(Arg.Any<string>());
            await userManagerMock.Received(1).IsEmailConfirmedAsync(Arg.Any<ApplicationUser>());
            await signInManagerMock.Received(1).PasswordSignInAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<bool>());
            notificationService.IsSuccess.Should().BeFalse();
            notificationService.Messages.First().Description.Should().Be("Usuário ou senha incorretos");
        }

        [Fact]
        public async Task NaoDeveLogarDadoMuitasTentativas()
        {
            //arrange
            var query = new SignInQuery
            {
                Email = "Email",
                Password = "Password"
            };

            var mockUserStore = Substitute.For<IUserStore<ApplicationUser>>();
            var loggerMock = Substitute.For<ILogger<SignInQueryHandler>>();

            var userManagerMock = Substitute.For<UserManager<ApplicationUser>>(mockUserStore, null, null, null, null, null, null, null, null);
            userManagerMock.FindByEmailAsync(Arg.Any<string>())
               .Returns(Task.FromResult(new ApplicationUser()).Result);
            userManagerMock.IsEmailConfirmedAsync(Arg.Any<ApplicationUser>())
                .Returns(true);

            var signInManagerMock = Substitute.For<FakeSignInManager>(userManagerMock, false);
            signInManagerMock.PasswordSignInAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<bool>())
                .Returns(Task.FromResult(SignInResult.LockedOut).Result);

            var tokenManagerMock = Substitute.For<ITokenManager>();
            tokenManagerMock.GenerateConfirmEmailTokenAsync(Arg.Any<ApplicationUser>()).Returns("");

            var notificationService = new NotificationsService();
            var queryHandler = new SignInQueryHandler(userManagerMock, signInManagerMock, tokenManagerMock, notificationService, loggerMock);

            //act
            var result = await queryHandler.Handle(query, CancellationToken.None);

            //assert
            result.Should().BeEquivalentTo(new SignInUser());
            await userManagerMock.Received(1).FindByEmailAsync(Arg.Any<string>());
            await userManagerMock.Received(1).IsEmailConfirmedAsync(Arg.Any<ApplicationUser>());
            await signInManagerMock.Received(1).PasswordSignInAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<bool>());
            notificationService.IsSuccess.Should().BeFalse();
            notificationService.Messages.First().Description.Should().Be("Usuário temporariamente bloqueado por muitas tentativas inválidas");
        }

        [Fact]
        public async Task DeveLogar()
        {
            //arrange
            var query = new SignInQuery
            {
                Email = "Email",
                Password = "Password"
            };

            var user = new SignInUser
            {
                AccessToken = "token",
                ExpiresIn = TimeSpan.FromMinutes(1).Minutes,
                UserToken = new UserToken
                {
                    Claims = new List<Claim>(),
                    Email = "Email",
                    Id = "123"
                }
            };

            var mockUserStore = Substitute.For<IUserStore<ApplicationUser>>();
            var loggerMock = Substitute.For<ILogger<SignInQueryHandler>>();

            var userManagerMock = Substitute.For<UserManager<ApplicationUser>>(mockUserStore, null, null, null, null, null, null, null, null);
            userManagerMock.FindByEmailAsync(Arg.Any<string>())
               .Returns(Task.FromResult(new ApplicationUser()).Result);
            userManagerMock.IsEmailConfirmedAsync(Arg.Any<ApplicationUser>())
                .Returns(true);

            var signInManagerMock = Substitute.For<FakeSignInManager>(userManagerMock, false);
            signInManagerMock.PasswordSignInAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<bool>())
                .Returns(Task.FromResult(SignInResult.Success).Result);

            var tokenManagerMock = Substitute.For<ITokenManager>();
            tokenManagerMock.GenerateToken(Arg.Any<string>())
                .Returns(Task.FromResult(user).Result);

            var notificationService = new NotificationsService();
            var queryHandler = new SignInQueryHandler(userManagerMock, signInManagerMock, tokenManagerMock, notificationService, loggerMock);

            //act
            var result = await queryHandler.Handle(query, CancellationToken.None);

            //assert
            result.Should().BeEquivalentTo(user);
            await userManagerMock.Received(1).FindByEmailAsync(Arg.Any<string>());
            await userManagerMock.Received(1).IsEmailConfirmedAsync(Arg.Any<ApplicationUser>());
            await signInManagerMock.Received(1).PasswordSignInAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<bool>());
            await tokenManagerMock.Received(1).GenerateToken(Arg.Any<string>());
            notificationService.IsSuccess.Should().BeTrue();
        }
    }
}
