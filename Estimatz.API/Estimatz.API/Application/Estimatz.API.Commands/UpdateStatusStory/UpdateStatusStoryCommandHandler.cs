using Estimatz.API.Data.RoomRepository;
using Estimatz.API.Data.StoryRepository;
using Estimatz.API.Entities.Room;
using Estimatz.API.Entities.UserStory;
using Estimatz.API.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Estimatz.API.Commands.UpdateStatusStory
{
    public class UpdateStatusStoryCommandHandler : IRequestHandler<UpdateStatusStoryCommand>
    {
        private readonly IStoryRepository _storyRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly ILogger<UpdateStatusStoryCommandHandler> _logger;
        private readonly INotificator _notificationService;

        public UpdateStatusStoryCommandHandler(IStoryRepository storyRepository, IRoomRepository roomStory, ILogger<UpdateStatusStoryCommandHandler> logger, INotificator notificationService)
        {
            _storyRepository = storyRepository;
            _roomRepository = roomStory;
            _logger = logger;
            _notificationService = notificationService;
        }

        public async Task Handle(UpdateStatusStoryCommand request, CancellationToken cancellationToken)
        {
            var room = await _roomRepository.FindRoom(request.RoomId);

            if(room is not null)
            {
                var indexArray = room.UserStories.FindIndex(x => x.Id == request.StoryId);

                if (indexArray != -1)
                {
                    RoomStatus newRoomStatus = RoomStatus.NotStarted;
                    bool updateRoomStatus = false;

                    if(request.NewStoryStatus == StoryStatus.InProgress && room.Status == RoomStatus.NotStarted)
                    {
                        newRoomStatus = RoomStatus.Unfinished;
                        updateRoomStatus = true;
                    }                    
                        
                    if(request.NewStoryStatus == StoryStatus.Finished && room.Status == RoomStatus.Unfinished && CanFinishRoom(room, request.StoryId))
                    {
                        newRoomStatus = RoomStatus.Finished;
                        updateRoomStatus = true;
                    }

                    var responseStory = await _storyRepository.UpdateStatusStory(indexArray, request.NewStoryStatus, request.RoomId);

                    if (updateRoomStatus)
                    {
                        var responseRoom = await _roomRepository.UpdateStatusRoom(room.Id, newRoomStatus);

                        if(responseRoom.StatusCode == HttpStatusCode.OK)                        
                            _logger.LogInformation($"Atualizado o status da sala {request.RoomId}");
                        else                        
                            _logger.LogError($"Ocorreu erro ao atualizar o status da sala {request.RoomId}");                       
                    }

                    if (responseStory.StatusCode == HttpStatusCode.OK)
                    {
                        _logger.LogInformation($"Atualizado o status da história {request.StoryId} da sala {request.RoomId}");
                        _notificationService.Notify(new(success: true));
                        return;
                    }
                }
            }

            _logger.LogError($"Não foi possível atualizar o status da história {request.StoryId} da sala {request.RoomId}");
            _notificationService.Notify(new(success: false));
        }

        private bool CanFinishRoom(Room room, Guid storyId)
        {
            bool finishRoom = true;

            foreach(var story in room.UserStories)
            {
                if (story.Id == storyId)
                    continue;

                if(story.Status != StoryStatus.Finished)
                    finishRoom = false;
            }

            return finishRoom;
        }
    }
}
