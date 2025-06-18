using Estimatz.Login.API.Entities.Notification;
using Estimatz.Login.API.Entities.User;
using Estimatz.Login.API.Notifications;
using Estimatz.Login.API.Services.Token;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Estimatz.Login.API.Queries.SignIn
{
    public class SignInQueryHandler : IRequestHandler<SignInQuery, SignInUser>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenManager _tokenManager;
        private readonly INotificator _notificationService;
        private readonly ILogger<SignInQueryHandler> _logger;

        public SignInQueryHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITokenManager tokenManager, INotificator notificationService, ILogger<SignInQueryHandler> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenManager = tokenManager;
            _notificationService = notificationService; 
            _logger = logger;
        }

        public async Task<SignInUser> Handle(SignInQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)            
                return InvalidSignIn("Usuário ou senha incorretos", $"Tentativa de login com e-mail {request.Email} usuário ou senha incorretos.", request.Email);
            
            var emailsIsConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            if (!emailsIsConfirmed)            
                return InvalidSignIn("E-mail não confirmado. Verifique sua caixa de entrada e confirme seu e-mail", $"E-mail {request.Email} não confirmado", request.Email);            

            var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, true);

            if (result.IsLockedOut)
                return InvalidSignIn("Usuário temporariamente bloqueado por muitas tentativas inválidas", $"E-mail {request.Email} temporariamente bloqueado por muitas tentativas inválidas.", request.Email);

            if (!result.Succeeded)            
                return InvalidSignIn("Usuário ou senha incorretos", $"Tentativa de login com e-mail {request.Email} usuário ou senha incorretos.", request.Email);

            _logger.LogInformation($"Usuário {request.Email} logado com sucesso!");
            _notificationService.Notify(new Notification(success: true));

            return await _tokenManager.GenerateToken(request.Email);
        }

        private SignInUser InvalidSignIn(string messageNotifiaction, string messageLog, string email)
        {
            var notification = new Notification(success: false, new Message(messageNotifiaction));
            _notificationService.Notify(notification);
            _logger.LogInformation(messageLog);

            return new SignInUser();
        }
    }
}
