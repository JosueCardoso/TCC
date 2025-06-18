using Estimatz.Entities.Notification;
using Estimatz.Entities.User;
using Estimatz.Events.RecoverPasswordEmail;
using Estimatz.Notifications;
using Estimatz.Services.Token;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Estimatz.Commands.Account.RecoverPassword
{
    public class RecoverPasswordCommandHandler : IRequestHandler<RecoverPasswordCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenManager _tokenManager;
        private readonly INotificator _notificationService;
        private readonly ILogger<RecoverPasswordCommandHandler> _logger;
        private readonly IMediator _mediator;

        public RecoverPasswordCommandHandler(UserManager<ApplicationUser> userManager, ITokenManager tokenManager, INotificator notificationService, ILogger<RecoverPasswordCommandHandler> logger, IMediator mediator)
        {
            _userManager = userManager;
            _tokenManager = tokenManager;
            _notificationService = notificationService;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Handle(RecoverPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                var notifiation = new Notification(success: false, new("Usuário não encontrado"));
                _notificationService.Notify(notifiation);

                _logger.LogError($"Erro: Usuário com email {request.Email} não encontrado.");
                return;
            }            
            
            var token = await _tokenManager.GenerateRecoverPasswordToken(user);
            await _mediator.Publish(new RecoverPasswordEmailEvent
            {
                ApplicationURL = request.ApplicationURL,
                Email = request.Email,
                Token = token,
                UserId = user.Id,
                Username = user.Name
            });

            _notificationService.Notify(new(success: true));
        }
    }
}
