using Estimatz.API.Data.RoomRepository;
using Estimatz.API.Data.StoryRepository;
using Estimatz.API.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Estimatz.API.Commands.RemoveStory
{
    public class RemoveStoryCommandHandler : IRequestHandler<RemoveStoryCommand>
    {
        private readonly IStoryRepository _storyRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly INotificator _notificationService;
        private readonly ILogger<RemoveStoryCommandHandler> _logger;

        public RemoveStoryCommandHandler(IStoryRepository storyRepository, IRoomRepository roomRepository, INotificator notificationService, ILogger<RemoveStoryCommandHandler> logger)
        {
            _storyRepository = storyRepository;
            _roomRepository = roomRepository;
            _notificationService = notificationService;
            _logger = logger;
        }

        public async Task Handle(RemoveStoryCommand request, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.FindRoom(request.RoomId);

            if(room != null)
            {
                var indexArray = room.UserStories.FindIndex(x => x.Id == request.StoryId);   

                if(indexArray != -1)
                {
                    var response = await _storyRepository.RemoveStory(indexArray, request.RoomId);

                    if(response.StatusCode == HttpStatusCode.OK)
                    {
                        _logger.LogInformation($"História {request.StoryId} removida da sala {request.RoomId}");
                        _notificationService.Notify(new(success: true));
                        return;
                    }
                }
            }

            _logger.LogInformation($"Não foi possível remover a história {request.StoryId} da sala {request.RoomId}");
            _notificationService.Notify(new(success: false));
        }
    }
}
