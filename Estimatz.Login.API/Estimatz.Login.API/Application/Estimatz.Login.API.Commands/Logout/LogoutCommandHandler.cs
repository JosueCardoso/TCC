using Estimatz.Login.API.Entities.Notification;
using Estimatz.Login.API.Entities.User;
using Estimatz.Login.API.Notifications;
using Estimatz.Login.API.Services.Token;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Estimatz.Login.API.Commands.Logout
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenManager _tokenManager;
        private readonly ILogger<LogoutCommandHandler> _logger;
        private readonly INotificator _notificationService;

        public LogoutCommandHandler(UserManager<ApplicationUser> userManager, ITokenManager tokenManager, INotificator notificationService, ILogger<LogoutCommandHandler> logger)
        {
            _userManager = userManager;
            _tokenManager = tokenManager;
            _logger = logger;
            _notificationService = notificationService;
        }

        public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            Notification notification;

            if (!_tokenManager.IsValidToken(request.Token))
            {                
                notification = new(success: false, new("Token informado para logout está invalido"));
                _logger.LogWarning("Token informado para logout está invalido!");
            }
            else
            {
                var simpleToken = _tokenManager.GetSimpleToken(request.Token);
                var user = await _userManager.FindByIdAsync(simpleToken.UserId);

                notification = new(success: true);
                _tokenManager.InvalidToken(request.Token);
                _logger.LogInformation($"Usuário com e-mail {user?.Email} realizou logout!");
            }

            _notificationService.Notify(notification);
        }
    }
}
