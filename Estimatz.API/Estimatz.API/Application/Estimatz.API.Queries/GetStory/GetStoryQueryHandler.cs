using Estimatz.API.Data.RoomRepository;
using Estimatz.API.Entities.UserStory;
using Estimatz.API.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Estimatz.API.Queries.GetStory
{
    public class GetStoryQueryHandler : IRequestHandler<GetStoryQuery, Story>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly INotificator _notificationService;
        private readonly ILogger<GetStoryQueryHandler> _logger;

        public GetStoryQueryHandler(IRoomRepository roomRepository, INotificator notificator, ILogger<GetStoryQueryHandler> logger)
        {
            _roomRepository = roomRepository;
            _notificationService = notificator;
            _logger = logger;
        }

        public async Task<Story> Handle(GetStoryQuery request, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.FindRoom(request.RoomId);

            if (room != null)
            {
                var story = room.UserStories.Find(x => x.Id == request.StoryId);

                if(story != null)
                {
                    _notificationService.Notify(new(success: true));
                    _logger.LogInformation($"História {story.Id} encontrada com sucesso!");
                    return story;
                }
            }

            _notificationService.Notify(new(success: false, new("Não foi possível encontrar a sala")));
            _logger.LogError($"Não foi possível encontrar a história. Sala {request.RoomId} - História {request.StoryId}");
            return new Story();
        }
    }
}
