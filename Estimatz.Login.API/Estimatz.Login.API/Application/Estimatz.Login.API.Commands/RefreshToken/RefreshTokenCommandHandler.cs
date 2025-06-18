using Estimatz.Login.API.Entities.Notification;
using Estimatz.Login.API.Entities.Token;
using Estimatz.Login.API.Entities.User;
using Estimatz.Login.API.Notifications;
using Estimatz.Login.API.Services.Token;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Estimatz.Login.API.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, SignInUser>
    {
        private readonly ITokenManager _tokenManager;
        private readonly INotificator _notificationService;
        private readonly ILogger<RefreshTokenCommandHandler> _logger;

        public RefreshTokenCommandHandler(ITokenManager tokenManager, INotificator notificator, ILogger<RefreshTokenCommandHandler> logger)
        {
            _tokenManager = tokenManager;
            _notificationService = notificator;
            _logger = logger;
        }

        public async Task<SignInUser> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            if (!_tokenManager.IsValidToken(request.TokenString))
            {
                var notification = new Notification(success: false, new Message("Não será possível resetar o token. Token informado está inválido"));
                _notificationService.Notify(notification);
                _logger.LogError($"Erro: Não será possível resetar o token. Token informado pelo email {request.Email} está inválido!");

                return new SignInUser();
            }

            var newToken = await _tokenManager.RefreshToken(new RefreshTokenRequestModel
            {
                Email = request.Email,
                TokenString = request.TokenString
            });

            _notificationService.Notify(new Notification(success: true));
            _logger.LogInformation($"Refresh de token para o e-mail {request.Email} realizado com sucesso.");

            return newToken;
        }
    }
}
