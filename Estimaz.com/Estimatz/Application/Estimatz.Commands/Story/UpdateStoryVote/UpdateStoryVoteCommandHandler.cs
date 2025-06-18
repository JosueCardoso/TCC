using Estimatz.Data.RoomRepository;
using Estimatz.Data.StoryRepository;
using Estimatz.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Estimatz.Commands.Story.UpdateStoryVote
{
    public class UpdateStoryVoteCommandHandler : IRequestHandler<UpdateStoryVoteCommand>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IStoryRepository _storyRepository;
        private readonly ILogger<UpdateStoryVoteCommandHandler> _logger;
        private readonly INotificator _notificationService;

        public UpdateStoryVoteCommandHandler(IRoomRepository roomRepository, IStoryRepository storyRepository, INotificator notificationService, ILogger<UpdateStoryVoteCommandHandler> logger)
        {
            _roomRepository = roomRepository;
            _storyRepository = storyRepository;
            _logger = logger;
            _notificationService = notificationService;
        }

        public async Task Handle(UpdateStoryVoteCommand request, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.FindRoom(request.RoomId);

            if(room is not null)
            {
                var indexArray = room.UserStories.FindIndex(x => x.Id == request.StoryId);

                if (indexArray != -1)
                {
                    var result = await _storyRepository.UpdateStoryVote(request.RoomId, indexArray, request.VotingResult);

                    if(result.StatusCode == HttpStatusCode.OK)
                    {
                        _logger.LogInformation($"Atualizada a votação da história {request.StoryId} da sala {request.RoomId}");
                        _notificationService.Notify(new(success: true));
                        return;
                    }
                }
            }

            _logger.LogError($"Não foi possível atualizar a votação da história {request.StoryId} da sala {request.RoomId}");
            _notificationService.Notify(new(success: false));
        }
    }
}
