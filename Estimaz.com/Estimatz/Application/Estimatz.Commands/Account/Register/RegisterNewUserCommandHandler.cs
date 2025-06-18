using Estimatz.Entities.Notification;
using Estimatz.Entities.User;
using Estimatz.Events.NewUserConfirmationEmail;
using Estimatz.Notifications;
using Estimatz.Services.Token;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Estimatz.Commands.Account.Register
{
    public class RegisterNewUserCommandHandler : IRequestHandler<RegisterNewUserCommand>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenManager _tokenManager;
        private readonly IMediator _mediator;
        private readonly INotificator _notificationService;
        private readonly ILogger<RegisterNewUserCommandHandler> _logger;

        public RegisterNewUserCommandHandler(UserManager<ApplicationUser> userManager, ITokenManager tokenManager, IMediator mediator, INotificator notificationService, ILogger<RegisterNewUserCommandHandler> logger)
        {
            _userManager = userManager;
            _tokenManager = tokenManager;
            _mediator = mediator;
            _notificationService = notificationService;
            _logger = logger;   
        }

        public async Task Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
        {
            Notification notification;

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                EmailConfirmed = false,   
                Name = request.Username
            };

            if(request.Password != request.ConfirmPassword)
            {
                _notificationService.Notify(new Notification(success: false, new Message("Senhas não conferem")));
                _logger.LogInformation($"Usuário {user.Email} informou senhas que não conferem");
                return;
            }            

            var result = await _userManager.CreateAsync(user, request.Password);     

            if (result.Succeeded)
            {                
                var confirmationToken = await _tokenManager.GenerateConfirmEmailTokenAsync(user);
                await _mediator.Publish(new NewUserConfirmationEmailEvent
                {
                    UserId = user.Id,
                    Email = user.Email,
                    Username = user.Name,
                    Token = confirmationToken,
                    ApplicationURL = request.ApplicationUrl
                });

                notification = new Notification(success: true, new Message("Usuário registrado com sucesso"));
                _logger.LogInformation($"Usuário {user.Email} registrado com sucesso.");
            }
            else
            {
                notification = new Notification(success: false);
                _logger.LogWarning($"Usuário {user.Email} não foi registrado.");

                foreach (var error in result.Errors)
                {
                    notification.AddMessage(new Message(error.Code, error.Description));
                    _logger.LogWarning($"Code: {error.Code} - Description: {error.Description}");
                }                
            } 

            _notificationService.Notify(notification);
        }        
    }
}
