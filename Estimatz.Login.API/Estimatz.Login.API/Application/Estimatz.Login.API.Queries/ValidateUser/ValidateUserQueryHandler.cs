using Estimatz.Login.API.Notifications;
using Estimatz.Login.API.Services.Token;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Estimatz.Login.API.Queries.ValidateUser
{
    public class ValidateUserQueryHandler : IRequestHandler<ValidateUserQuery>
    {
        private readonly ITokenManager _tokenManager;
        private readonly INotificator _notificationService;
        private readonly ILogger<ValidateUserQueryHandler> _logger;

        public ValidateUserQueryHandler(ITokenManager tokenManager, INotificator notificationService, ILogger<ValidateUserQueryHandler> logger)
        {
            _tokenManager = tokenManager;
            _notificationService = notificationService;
            _logger = logger;
        }

        public Task Handle(ValidateUserQuery request, CancellationToken cancellationToken)
        {
            if (!_tokenManager.IsValidToken(request.Token))
            {
                _logger.LogError($"Token inválido! Token: {request.Token} - UserId: {request.UserId}");
                _notificationService.Notify(new(success: false));
                return Task.CompletedTask;
            }

            var simpleToken = _tokenManager.GetSimpleToken(request.Token);

            if(simpleToken.UserId != request.UserId)
            {
                _logger.LogError($"Usuário inválido! Token: {request.Token} - UserId: {request.UserId}");
                _notificationService.Notify(new(success: false));
                return Task.CompletedTask;
            }

            _logger.LogInformation($"Token validado! Token: {request.Token} - UserId: {request.UserId}");
            _notificationService.Notify(new(success: true));
            return Task.CompletedTask;
        }
    }
}
