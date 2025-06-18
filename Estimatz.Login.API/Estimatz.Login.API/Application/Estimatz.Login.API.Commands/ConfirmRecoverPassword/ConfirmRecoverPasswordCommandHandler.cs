using Estimatz.Login.API.Entities.Notification;
using Estimatz.Login.API.Entities.User;
using Estimatz.Login.API.Notifications;
using Estimatz.Login.API.Services.Token;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Estimatz.Login.API.Commands.ConfirmRecoverPassword
{
    public class ConfirmRecoverPasswordCommandHandler : IRequestHandler<ConfirmRecoverPasswordCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenManager _tokenManager;
        private readonly INotificator _notificationService;
        private readonly ILogger<ConfirmRecoverPasswordCommandHandler> _logger;

        public ConfirmRecoverPasswordCommandHandler(UserManager<ApplicationUser> userManager, ITokenManager tokenManager, INotificator notificationService, ILogger<ConfirmRecoverPasswordCommandHandler> logger)
        {
            _userManager = userManager;
            _tokenManager = tokenManager;
            _notificationService = notificationService;
            _logger = logger;
        }

        public async Task Handle(ConfirmRecoverPasswordCommand request, CancellationToken cancellationToken)
        {
            Notification notification;

            if (!_tokenManager.IsValidToken(request.Token))
            {
                _notificationService.Notify(new(success: false, new("Token para recuperação de senha está invalido")));
                _logger.LogError("Erro: Token para recuperação de senha está invalido!");
                return;
            }
                        
            if (request.Password != request.ConfirmPassword)
            {
                _notificationService.Notify(new(success: false, new("Senhas não conferem")));
                _logger.LogError($"Erro: Usuário informou senhas que não conferem.");
                return;
            }

            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                notification = new Notification(success: false, new("Usuário não encontrado"));
                _logger.LogError($"Erro: Usuário não foi encontrado.");
            }
            else
            {
                var result = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);

                if (result.Succeeded)
                {
                    _tokenManager.InvalidToken(request.Token);
                    notification = new Notification(success: true);
                    _logger.LogInformation("Token para recuperação de senha validado com sucesso!");
                }
                else
                {
                    notification = new Notification(success: false);
                    _logger.LogError($"Erro: Usuário do {user.Email} não foi encontrado.");

                    foreach (var error in result.Errors)
                    {
                        notification.AddMessage(new(error.Code, error.Description));
                        _logger.LogError($"Code: {error.Code} - Description: {error.Description}");
                    }                    
                }
            }

            _notificationService.Notify(notification);
        }
    }
}
