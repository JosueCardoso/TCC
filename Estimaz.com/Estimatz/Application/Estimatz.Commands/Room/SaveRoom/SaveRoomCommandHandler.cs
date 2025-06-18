using AutoMapper;
using Estimatz.Commands.Story.AddStory;
using Estimatz.Data.RoomRepository;
using Estimatz.Entities.Notification;
using Entity = Estimatz.Entities.Room;
using Estimatz.Entities.UserStory;
using Estimatz.Notifications;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Estimatz.Commands.Room.SaveRoom
{
    public class SaveRoomCommandHandler : IRequestHandler<SaveRoomCommand, Guid>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;
        private readonly INotificator _notificationService;
        private readonly ILogger<SaveRoomCommandHandler> _logger;
        private readonly IMediator _mediatr;

        public SaveRoomCommandHandler(IRoomRepository roomRepository, IMapper mapper, INotificator notificationService, ILogger<SaveRoomCommandHandler> logger, IMediator mediatr)
        {
            _roomRepository = roomRepository;
            _mapper = mapper;
            _notificationService = notificationService;
            _logger = logger;
            _mediatr = mediatr;
        }

        public async Task<Guid> Handle(SaveRoomCommand request, CancellationToken cancellationToken)
        {
            var room = _mapper.Map<Entity.Room>(request);
            room.Id = Guid.NewGuid();

            var response = await _roomRepository.CreateRoom(room);

            if (response?.StatusCode == System.Net.HttpStatusCode.Created)
            {
                _logger.LogInformation($"Nova sala com ID {room.Id} criada com sucesso");

                if (room.RoomConfig.VotingType == Entity.VotingType.FreeVoting) //Caso a votação seja livre (sem tasks ou story) deve ser criado ao menos uma por baixo dos panos para que seja possível armazenar as informações das votações
                    CreateStory(room.Id);

                _notificationService.Notify(new Notification(success: true));

                return room.Id;
            }

            _notificationService.Notify(new Notification(success: false, new("Não foi possível criar a nova sala")));
            _logger.LogError($"Não foi possível criar a nova sala");

            return Guid.Empty;
        }

        private async void CreateStory(Guid roomId)
        {
            var story = new UserStory 
            { 
                Id = Guid.NewGuid(),
                Name = "",
                Status = StoryStatus.Unfinished
            };

            await _mediatr.Send(new AddStoryCommand 
            { 
                RoomId = roomId, 
                Story = story 
            });

            _logger.LogInformation($"Nova história com ID {story.Id} criada com sucesso");
        }
    }
}
