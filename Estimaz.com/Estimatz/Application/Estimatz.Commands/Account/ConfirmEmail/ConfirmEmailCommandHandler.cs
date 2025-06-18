using Estimatz.Entities.Notification;
using Estimatz.Entities.User;
using Estimatz.Notifications;
using Estimatz.Services.Token;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Estimatz.Commands.Account.ConfirmEmail
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenManager _tokenManager;
        private readonly INotificator _notificationService;
        private readonly ILogger<ConfirmEmailCommandHandler> _logger;

        public ConfirmEmailCommandHandler(UserManager<ApplicationUser> userManager, ITokenManager tokenManager, INotificator notificator, ILogger<ConfirmEmailCommandHandler> logger)
        {
            _userManager = userManager;
            _tokenManager = tokenManager;
            _notificationService = notificator;
            _logger = logger;
        }

        public async Task Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            Notification notification;
            IdentityResult? result = null;

            if (!_tokenManager.IsValidToken(request.Token))
            {
                _notificationService.Notify(new(success: false, new("Token Invalido")));
                _logger.LogError("Erro: Token para confirmação de e-mail inválido!");
                return;
            }

            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user is not null)
                result = await _userManager.ConfirmEmailAsync(user, request.Token);

            if (result is not null && result.Succeeded)
            {
                _tokenManager.InvalidToken(request.Token);
                notification = new Notification(success: true, new("E-mail confirmado com sucesso"));
                _logger.LogInformation($"E-mail {user?.Email} confirmado com sucesso.");
            } 
            else if (result is null)
            {
                notification = new Notification(success: false, new("Não foi possível confirmar o e-mail"));
                _logger.LogWarning($"Erro: Não foi possível confirmar o e-mail. E-mail não encontrado.");
            }
            else
            {
                notification = new Notification(success: false);
                _logger.LogWarning($"Erro: Não foi possível confirmar o e-mail {user?.Email}.");

                foreach(var error in result.Errors)
                {
                    notification.AddMessage(new Message(error.Code, error.Description));
                    _logger.LogWarning($"Code: {error.Code} - Description: {error.Description}");
                }
            }

            _notificationService.Notify(notification);
        }
    }
}
