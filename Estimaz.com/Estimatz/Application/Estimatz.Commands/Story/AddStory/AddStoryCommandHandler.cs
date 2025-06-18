using Estimatz.Data.StoryRepository;
using Estimatz.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Estimatz.Commands.Story.AddStory
{
    public class AddStoryCommandHandler : IRequestHandler<AddStoryCommand>
    {
        private readonly IStoryRepository _storyRepository;
        private readonly INotificator _notificationService;
        private readonly ILogger<AddStoryCommandHandler> _logger;

        public AddStoryCommandHandler(IStoryRepository storyRepository, INotificator notificationService, ILogger<AddStoryCommandHandler> logger)
        {
            _storyRepository = storyRepository;
            _notificationService = notificationService;
            _logger = logger;
        }

        public async Task Handle(AddStoryCommand command, CancellationToken cancellationToken)
        {
            var response = await _storyRepository.AddStory(command.RoomId, command.Story);

            if(response.StatusCode == HttpStatusCode.OK)
            {
                _logger.LogInformation($"Nova história adicionada na sala {command.RoomId}");
                _notificationService.Notify(new(success: true));
                return;                
            }

            _logger.LogError($"Não foi possível adicionar uma nova história na sala {command.RoomId}");
            _notificationService.Notify(new(success: false));
            return;
        }
    }
}
